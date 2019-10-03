using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

namespace editor
{
  public class Build
  {
    private static ICommandLineInterface m_commandLineInstance = new CommandLineInterface();
#if UNITY_EDITOR
    public static ICommandLineInterface m_commandLine { get { return m_commandLineInstance; } set { m_commandLineInstance = value; } }
#endif

    [MenuItem("Build/Windows64")]
    public static BuildSummary Windows64Build()
    {
      BuildPlayerOptions options = new BuildPlayerOptions();

      options.scenes = GetLevelsFromBuildSettings();
      options.target = BuildTarget.StandaloneWindows64;
      options.options = BuildOptions.None;
      options.locationPathName = Path.Combine("Build/Release/",
                                              Path.Combine(options.target.ToString(),
                                                           Application.productName + "_" + options.target.ToString() + ".exe"));

      return PerformBuild(options);
    }

    public static BuildSummary CommandLineBuild()
    {
      string[] commandLineArguments = m_commandLine.GetCommandLineArgs();
      List<string> providedScenes = new List<string>();
      string providedLocationPath = "";

      BuildTarget providedTarget = BuildTarget.Tizen;
      BuildOptions buildOptions = BuildOptions.None;

      Debug.Log("Got CommandLine Args:");
      foreach (string argument in commandLineArguments)
      {
        Debug.Log(argument);
        if (argument.StartsWith("--Scenes"))
        {
          foreach (string scene in argument.Substring(2).Split(' '))
          {
            if (scene.EndsWith(".unity"))
            {
              providedScenes.Add(scene);
              Debug.Log("Added Scene: " + scene);
            }
          }

          continue;
        }
        else if (argument.StartsWith("--Path"))
        {
          providedLocationPath = argument.Substring(1).Split(' ')[1];
          continue;
        }
        else if (argument.StartsWith("--Target"))
        {
          providedTarget = StringToBuildTarget(argument.Substring(1).Split(' ')[1]);
          continue;
        }
        else if (argument.StartsWith("--DevBuild"))
        {
          buildOptions = BuildOptions.Development | BuildOptions.AllowDebugging;
        }
      }

      BuildPlayerOptions options = new BuildPlayerOptions();
      options.options = buildOptions;
      options.scenes = providedScenes.Count > 0 ? providedScenes.ToArray() : GetLevelsFromBuildSettings();
      options.locationPathName = providedLocationPath;
      options.target = providedTarget;

      return PerformBuild(options);
    }

    public static BuildSummary PerformBuild(BuildPlayerOptions p_buildOptions)
    {
      BuildSummary summary = new BuildSummary();

      if (!IsSceneStringsCorrect(p_buildOptions.scenes))
      {
        return summary;
      }

      bool buildSuccess = false;

      Debug.Log("Buildplayer Options:");
      Debug.Log(p_buildOptions.options);

      BuildReport report = BuildPipeline.BuildPlayer(p_buildOptions);
      summary = report.summary;

      buildSuccess = summary.result == BuildResult.Succeeded;

      if (buildSuccess)
      {
        Debug.Log("Build Succeeded: " + summary.totalSize + " bytes");
        Debug.Log("Build Time: " + summary.totalTime);
        Debug.Log("Build Path: " + summary.outputPath);
      }
      else
      {
        Debug.LogError("Build result did not yield in success");
        Debug.LogWarning("Result:" + summary.result);
        Debug.LogWarning("GUID:" + summary.guid);
        Debug.LogWarning("Options:" + summary.options);
        Debug.LogWarning("Outpath:" + summary.outputPath);
        Debug.LogWarning("Platform:" + summary.platform);
        Debug.LogWarning("Errors:" + summary.totalErrors);
        Debug.LogWarning("Warnings:" + summary.totalWarnings);
        Debug.LogWarning("Time:" + summary.totalTime);

        Debug.Log("Steps taking place during build:");
        foreach (BuildStep step in report.steps)
        {
          Debug.Log(step.name);
          foreach (BuildStepMessage message in step.messages)
          {
            Debug.Log(string.Format("Type {0} - content:{1}", message.type, message.content));
          }
        }
      }

      return summary;
    }

    private static bool IsSceneStringsCorrect(string[] p_scenes)
    {
      if (p_scenes.Length == 0)
        return false;

      // Check if all scene strings are correct else return false
      foreach (var scene in p_scenes)
      {
        if (!scene.EndsWith(".unity") || !File.Exists(scene))
        {
          // Create string of scenes that was sentin
          string sceneStrings = "";
          foreach (var sceneString in p_scenes)
          {
            sceneStrings += sceneString + "\n";
          }

          Debug.LogError(string.Format("Scenes array contains a non-existing scene string\n{0}", sceneStrings));
          return false;
        }
      }
      return true;
    }

    public static string[] GetLevelsFromBuildSettings()
    {
      List<string> enabledBuildSettingsScenes = new List<string>();

      for (int sceneIndex = 0; sceneIndex < EditorBuildSettings.scenes.Length; ++sceneIndex)
      {
        EditorBuildSettingsScene scene = EditorBuildSettings.scenes[sceneIndex];
        if (scene.enabled)
        {
          enabledBuildSettingsScenes.Add(scene.path);
        }
      }

      return enabledBuildSettingsScenes.ToArray();
    }

    private static BuildTarget StringToBuildTarget(string p_buildstring)
    {
      BuildTarget target = BuildTarget.Tizen;

      if ("windows32".Equals(p_buildstring.ToLower()))
      {
        target = BuildTarget.StandaloneWindows;
      }
      else if ("windows64".Contains(p_buildstring.ToLower()))
      {
        target = BuildTarget.StandaloneWindows64;
      }
      else if ("linux".Equals(p_buildstring.ToLower()))
      {
        target = BuildTarget.StandaloneLinux64;
      }

      if (target == BuildTarget.Tizen)
      {
        Debug.Log(BuildTarget.StandaloneWindows64.ToString());
        Debug.LogError("Non valid buildtarget was provided, " + p_buildstring);
      }

      return target;
    }
  }
}
