using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public float m_rotationalSpeed = 10f;
    public float m_accelerationMultiplier = 5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Acceleration",5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Random.value, Random.value, Random.value) * (m_rotationalSpeed *  Time.deltaTime));

        transform.RotateAround(Vector3.zero, Vector3.forward, 1.0f);

    }


    public void Acceleration()
    {
        m_rotationalSpeed += m_accelerationMultiplier;
    }
}
