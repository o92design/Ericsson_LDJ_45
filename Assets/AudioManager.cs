using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    // Audio Clips Here
    //AudioClip m_sound; <--- example
    public AudioClip m_blip;
    public AudioClip m_boom;
    public AudioClip m_mellow;
    // Components
    AudioListener m_audioListener;
    AudioSource m_audioSource;
    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(gameObject);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            Instance = this;
        }

        //m_sound = Resources.Load<AudioClip>("Sound/m_sound"); <-- example
        m_blip = Resources.Load<AudioClip>("Sound/Blip");
        m_boom = Resources.Load<AudioClip>("Sound/boom");
        m_mellow = Resources.Load<AudioClip>("Sound/mellow");
        m_audioListener = GetComponent<AudioListener>();
        m_audioSource = GetComponent<AudioSource>();




        // Init
        m_audioSource.PlayOneShot(m_blip);
        
    }


    public void Keyevent()
    {
        m_audioSource.PlayOneShot(m_blip);
    }

    public void PlayBoom()
    {
        m_audioSource.PlayOneShot(m_boom);
    }

    public void PlayBackgroundMellow()
    {
        m_audioSource.clip = m_mellow;
        m_audioSource.loop = true;
        m_audioSource.Play();
    }

}
