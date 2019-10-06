using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IngameUiHandler : MonoBehaviour
{



    public static bool m_paused = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;
            m_paused = !m_paused;
            if (m_paused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }

        }
    }




    public void ExitApplication()
    {
        Application.Quit();
    }

    public void ExitToMainMenu()
    {
        Destroy(Camera.main.gameObject);
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

}
