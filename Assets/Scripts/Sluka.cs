using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sluka : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Destroying " + collision.gameObject.name);
        if (PlanetJointHandler.Instance.m_connected.Contains(collision.gameObject))
        {
            PlanetJointHandler.Instance.m_connected.Remove(collision.gameObject);
        }
        Destroy(collision.gameObject);
    }
}
