using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
  private void OnEnable()
  {
    Debug.Log("Exit game");
    Application.Quit();
  }
}
