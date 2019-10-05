﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class InitiateWorld : MonoBehaviour
{

    public List<GameObject> m_world;
    public GameObject m_startGlow;




    private void Update()
    {
        Application.targetFrameRate = 60;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_startGlow.SetActive(true);
            GetComponent<ParticleSystem>().Play();
            Camera.main.backgroundColor = new Color(0.2f, 0.2f, 0.2f);
            Bloom bloomLayer = null;

            // somewhere during initializing
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
        StartCoroutine("Light");
    }

    IEnumerator Light()
    {
        PostProcessVolume volume = Camera.main.GetComponent<PostProcessVolume>();
        while (volume.weight > 0)
        {
            volume.weight -= 0.001f;
            yield return null;
        }
        Debug.Log("end");
    }

}