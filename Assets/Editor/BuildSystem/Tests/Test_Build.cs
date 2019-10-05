using NUnit.Framework;
using NUnit.Framework.Interfaces;
using editor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.Build.Reporting;
using System.Text.RegularExpressions;
using UnityEditor;
using NSubstitute;
using System.IO;

namespace Tests
{
  public class Test_Build
  {
    [SetUp]
    public void Build_SetupTest()
    {
      Debug.Log(" ==== Performing Unit test of " + TestContext.CurrentContext.Test.Name + " ====");
      BuildSystem.m_commandLine = Substitute.For<ICommandLineInterface>();
      LogAssert.ignoreFailingMessages = false;
    }

    [TearDown]
    public void Build_TeardownTest()
    {
      Debug.Log(" ==== Teardown for Unit test of " + TestContext.CurrentContext.Test.Name + " ====");
      TestStatus result = TestContext.CurrentContext.Result.Outcome.Status;
      if (result == TestStatus.Passed)
      {
        Debug.Log("Result:" + result);
      }
      else
      {
        Debug.LogError("Result:" + result);
      }
      Debug.Log("=======================");
    }

    [Test]
    public void Build_Negative_SentInWrongSceneStringsTest()
    {
      BuildPlayerOptions options = new BuildPlayerOptions();
      options.scenes = new[] { "Assets/Assets/Editor/Build/Tests/Scenes/BuildScene.unity", "NON-existing" };

      Regex scenesRegex = new Regex(@"non-existing", RegexOptions.Multiline | RegexOptions.IgnoreCase);
      LogAssert.Expect(LogType.Error, scenesRegex);
      BuildSystem.PerformBuild(options);
    }

    [Test]
    public void Build_Positive_PerformBuildWithExistingSceneTest()
    {
      BuildPlayerOptions options = new BuildPlayerOptions();
      options.scenes = new[] { "Assets/Editor/Build/Tests/Scenes/BuildScene.unity" };
      options.target = BuildTarget.StandaloneWindows64;
      options.options = BuildOptions.Development;
      options.locationPathName = Path.Combine("Build/EditorTest/Test", Path.Combine(options.target.ToString(), "EditorTest.exe"));

      LogAssert.Expect(LogType.Log, new Regex(@"Succeeded", RegexOptions.IgnoreCase));

      BuildSummary summary = BuildSystem.PerformBuild(options);
      Assert.AreEqual(summary.result, BuildResult.Succeeded);
    }

    [Test]
    public void Build_Negative_PerformBuildWithNonExistingSceneTest()
    {
      BuildPlayerOptions options = new BuildPlayerOptions();
      options.scenes = new[] { "Assets/Editor/Build/Tests/Scenes/Build.unity" };
      options.target = BuildTarget.StandaloneWindows64;
      options.options = BuildOptions.BuildScriptsOnly;
      options.locationPathName = Path.Combine("Build/EditorTest/Test", Path.Combine(options.target.ToString(), "EditorTest.exe"));

      LogAssert.Expect(LogType.Error, new Regex(@"non-existing", RegexOptions.IgnoreCase));

      BuildSummary summary = BuildSystem.PerformBuild(options);
      Assert.AreEqual(summary.result, BuildResult.Unknown, "Build method did not return a uninitialized BuildSummary");
    }

    [Test]
    public void Build_Negative_NonEnabledBuildSettingsLevelsTest()
    {
      EditorBuildSettingsScene[] scenes = { new EditorBuildSettingsScene("Assets/Editor/Build/Tests/Scenes/BuildScene.unity", false) };
      EditorBuildSettings.scenes = scenes;

      Assert.IsEmpty(BuildSystem.GetLevelsFromBuildSettings(), "Non-enabled scene was added to the buildsettings");
    }

    [Test]
    public void Build_Positive_PerformWindows64BuildTest()
    {
      EditorBuildSettingsScene[] scenes = { new EditorBuildSettingsScene("Assets/Editor/Build/Tests/Scenes/BuildScene.unity", true) };
      EditorBuildSettings.scenes = scenes;

      BuildSummary summary = BuildSystem.Windows64Build();
      Assert.AreEqual(summary.result, BuildResult.Succeeded, "Did not succeed in building for Windows 64");
    }

    [Test]
    public void Build_Positive_PerformCommandLineDevelopmentBuildTest()
    {
      string scenes = "Assets/Editor/Build/Tests/Scenes/BuildScene.unity Assets/Editor/Build/Tests/Scenes/BuildScene2.unity";
      string locationPath = Path.Combine("Build/BuildEditorTest", Path.Combine(BuildTarget.StandaloneWindows64.ToString(), Application.productName + ".exe"));
      string[] expectedArguments = new[] { "--DevBuild", "--Target Windows64", "--Path " + locationPath, "--Scenes " + scenes };
      BuildOptions options = BuildOptions.Development | BuildOptions.AllowDebugging;

      BuildSystem.m_commandLine.GetCommandLineArgs().Returns(expectedArguments);

      BuildSummary summary = BuildSystem.CommandLineBuild();
      BuildSystem.m_commandLine.Received().GetCommandLineArgs();

      Assert.AreEqual(BuildResult.Succeeded, summary.result, "Commandline build did not yield in succcess");
      Assert.AreEqual(options, summary.options, "Did not return the correct BuildOptions");
    }

    [Test]
    public void Build_Positive_PerformWithNoScenesSelected()
    {
      string locationPath = Path.Combine("Build/BuildEditorTest", Path.Combine(BuildTarget.StandaloneWindows64.ToString(), Application.productName + ".exe"));
      string[] expectedArguments = new[] { "--DevBuild", "--Target Windows64", "--Path " + locationPath};
      BuildOptions options = BuildOptions.Development | BuildOptions.AllowDebugging;

      // This is for temporary remove any selected scenes in Editor build settings
      EditorBuildSettingsScene[] tempSceneSettings = EditorBuildSettings.scenes;
      EditorBuildSettings.scenes = null;

      BuildSystem.m_commandLine.GetCommandLineArgs().Returns(expectedArguments);

      BuildSummary summary = BuildSystem.CommandLineBuild();
      BuildSystem.m_commandLine.Received().GetCommandLineArgs();

      EditorBuildSettings.scenes = tempSceneSettings;

      Assert.AreEqual(BuildResult.Succeeded, summary.result, "Commandline build did not yield in succcess");
      Assert.AreEqual(options, summary.options, "Did not return the correct BuildOptions");
    }
  }
}
