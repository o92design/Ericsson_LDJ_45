using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToGame : MonoBehaviour
{
  public string m_nextSceneName;
  private void OnEnable()
  {
    SceneManager.LoadScene(m_nextSceneName);
  }
}
