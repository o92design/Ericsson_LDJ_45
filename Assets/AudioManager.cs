using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    // Audio Clips Here
    //AudioClip m_sound; <--- example
    public AudioClip m_blip;


    // Components
    AudioListener m_audioListener;
    AudioSource m_audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //m_sound = Resources.Load<AudioClip>("Sound/m_sound"); <-- example
        m_blip = Resources.Load<AudioClip>("Sound/Blip");
        m_audioListener = GetComponent<AudioListener>();
        m_audioSource = GetComponent<AudioSource>();




        // Init
        m_audioSource.PlayOneShot(m_blip);
        
    }


    public void Keyevent()
    {
        m_audioSource.PlayOneShot(m_blip);
    }



}
