using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetJointHandler : MonoBehaviour
{
    public static PlanetJointHandler Instance;

    public List<GameObject> m_connected;
    public GameObject m_missile;
    // Start is called before the first frame update
    void Start()
    {
        m_missile = Resources.Load<GameObject>("Prefabs/Missile");
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
        InvokeRepeating("Humanwar", 60, 5);
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

    public void Humanwar()
    {
        Debug.Log("War");
        int numofhumans = 0;
        GameObject firsthuman = null;
        GameObject thatnastyone = null;
        foreach (GameObject game in PlanetJointHandler.Instance.m_connected)
        {
            if (game.GetComponent<Objekt_Skit>().objtype == 3)
            {
                if (numofhumans == 0)
                {
                    firsthuman = game;
                }
                else
                {
                    thatnastyone = game;
                }
                numofhumans++;
            }
        }
        if (numofhumans > 20)
        {
            Debug.Log("fire");
            GameObject missile = Instantiate(m_missile,thatnastyone.transform.position, Quaternion.identity);
            missile.GetComponent<missile>().m_target = firsthuman;
            missile.GetComponent<SphereCollider>().enabled = false;
        }
    }



}
