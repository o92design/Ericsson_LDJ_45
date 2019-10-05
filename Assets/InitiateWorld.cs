using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class InitiateWorld : MonoBehaviour
{

    public List<GameObject> m_world;
    public GameObject m_startGlow;




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_startGlow.SetActive(true);
            GetComponent<ParticleSystem>().Play();
            Camera.main.backgroundColor = new Color(0.2f, 0.2f, 0.2f);
        }
    }



    public void OnParticleSystemStopped()
    {
        foreach(GameObject game in m_world)
        {
            game.SetActive(true);
        }


        Bloom bloomLayer = null;

        // somewhere during initializing
        PostProcessVolume volume = m_startGlow.GetComponentInChildren<PostProcessVolume>();
        volume.profile.TryGetSettings<Bloom>(out bloomLayer);

        bloomLayer.intensity.value = 20f;
        bloomLayer.threshold.value = 20f;


        m_startGlow.SetActive(false);
    }
}
