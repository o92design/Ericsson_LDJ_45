using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class InitiateWorld : MonoBehaviour
{

    public List<GameObject> m_world;
    public GameObject m_startGlow;
    public bool m_playingmusic = false; 
    public bool m_isWorldInitiated = false;



    private void Update()
    {
        Application.targetFrameRate = 60;
        if (Input.GetKeyDown(KeyCode.Space) && !m_isWorldInitiated)
        {
            m_startGlow.SetActive(true);
            GetComponent<ParticleSystem>().Play();
            AudioManager.Instance.PlayBang();

            // somewhere during initializing
            m_isWorldInitiated = true;
        }
    }

    public void BigBang()
    {
        if (!m_isWorldInitiated)
        {
          m_startGlow.SetActive(true);
          GetComponent<ParticleSystem>().Play();
          AudioManager.Instance.PlayBang();

          // somewhere during initializing
          m_isWorldInitiated = true;
        }
  }

    public void OnParticleSystemStopped()
    {
        foreach(GameObject game in m_world)
        {
            game.SetActive(true);
        }
        PostProcessVolume volume = Camera.main.GetComponent<PostProcessVolume>();
        volume.weight = 0.8f;

        m_startGlow.SetActive(false);
        Camera.main.backgroundColor = new Color(0.2f, 0.2f, 0.2f);
        AudioManager.Instance.PlayBoom();
        ScoreHandler.Instance.gameObject.GetComponent<Canvas>().enabled = true;
        ScoreHandler.Instance.m_scoretext.alpha = 0;
        ScoreHandler.Instance.m_score.alpha = 0;
        ScoreHandler.Instance.m_modifier.alpha = 0;
        StartCoroutine("Light");
    }

    IEnumerator Light()
    {
        PostProcessVolume volume = Camera.main.GetComponent<PostProcessVolume>();
        while (volume.weight > 0)
        {
            volume.weight -= 0.001f;
            if (volume.weight < 0.2 && !m_playingmusic)
            {
                m_playingmusic = true;
                AudioManager.Instance.PlayBackgroundMusic();
                StartCoroutine("Ui");
            }
            yield return null;
        }
        Debug.Log("end");
    }

    IEnumerator Ui()
    {
        while (ScoreHandler.Instance.m_scoretext.alpha < 1)
        {
            ScoreHandler.Instance.m_scoretext.alpha = ScoreHandler.Instance.m_scoretext.alpha + 0.001f;
            ScoreHandler.Instance.m_modifier.alpha = ScoreHandler.Instance.m_modifier.alpha + 0.001f;
            ScoreHandler.Instance.m_score.alpha = ScoreHandler.Instance.m_score.alpha + 0.001f;

            
            yield return null;
        }
        Debug.Log("fini"); 
    }

}
