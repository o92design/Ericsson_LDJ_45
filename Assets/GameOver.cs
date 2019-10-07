using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{
    public static GameOver Instance;
    public Canvas canvas;
    public Image image;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            Instance = this;
        }
        canvas = GetComponent<Canvas>();
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TriggerGameOver()
    {
        canvas.enabled = true;
        StartCoroutine(fade());
        Invoke("LoadMenu", 20f);
    }

    IEnumerator fade()
    {
        while (text.alpha < 1)
        {
            text.alpha = text.alpha + 0.001f;
            if (image.color.a < 0.3)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.001f);
            }
            yield return null;
        }
    }

    public void LoadMenu()
    {
        Destroy(Camera.main.gameObject);
        SceneManager.LoadScene(0);
    }

}
