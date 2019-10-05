using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


  void Update()
  {
    if(Input.GetKey(KeyCode.LeftControl) && 
       Input.GetKey(KeyCode.LeftShift))
    {
      Debug.Log("Tries to switch scene");
     
      Event keyEvent = Event.current;
      if(keyEvent != null)
      {
        Debug.Log(keyEvent.keyCode);
        if (keyEvent.keyCode >= KeyCode.Alpha0 && keyEvent.keyCode <= KeyCode.Alpha9)
        {
          int sceneIndex = keyEvent.keyCode - KeyCode.Alpha0;
          SceneManager.LoadScene(sceneIndex);
        }
      }
    }
  }
}
