using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetJointHandler : MonoBehaviour
{
    public static PlanetJointHandler Instance;

    public List<GameObject> m_connected;
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

        m_connected = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // please fiz
        for (int i = 0;i < m_connected.Count; i++)
        {
            if (m_connected[i] == null)
            {
                m_connected.Remove(m_connected[i]);
            }
        }
    }

    
}
