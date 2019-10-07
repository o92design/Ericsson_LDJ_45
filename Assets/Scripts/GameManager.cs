using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;

  //Awake is always called before any Start functions
  void Awake()
  {
        //Check if instance already exists
        if (instance == null)

      //if not, set instance to this
      instance = this;

    //If instance already exists and it's not this:
    else if (instance != this)

      //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
      Destroy(gameObject);

    //Sets this to not be destroyed when reloading scene
    DontDestroyOnLoad(gameObject);
  }

  void OnGUI()
  {
    if (Input.GetKey(KeyCode.LeftControl) &&
        Input.GetKey(KeyCode.LeftShift))
    {
      Event keyEvent = Event.current;

      if (keyEvent.isKey && keyEvent.type == EventType.KeyDown)
      {
        if (keyEvent.keyCode >= KeyCode.Alpha0 && keyEvent.keyCode <= KeyCode.Alpha9)
        {
          int sceneIndex = keyEvent.keyCode - KeyCode.Alpha0;
          // 0 wasn't recorded as 0 :(
          if (keyEvent.keyCode == KeyCode.Alpha9)
            sceneIndex = 0;

          if (sceneIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(sceneIndex);
        }
      }
    }
  }

  public List<string> GetAllScenes()
  {
    List<string> projectScenes = new List<string>();
    var scenesDirPath = Application.dataPath + "/Scenes";
    var scenesDirInfo = new DirectoryInfo(scenesDirPath);
    var allScenesInfo = scenesDirInfo.GetFiles("*.unity", SearchOption.AllDirectories);

    foreach (var sceneInfo in allScenesInfo)
    {
      projectScenes.Add(sceneInfo.FullName);
    }

    Debug.Log("Chose to add all scenes: " + projectScenes.Count);

    string scenesAdded = "";
    foreach (string sceneName in projectScenes)
    {
      scenesAdded += sceneName + "\n";
    }
    Debug.Log(scenesAdded);

    return projectScenes;
  }
}
