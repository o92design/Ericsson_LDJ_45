using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public float m_rotationalSpeed = 10f;
    public float m_accelerationMultiplier = 5f;
    Vector3 m_rotation = new Vector3();
    // Start is called before the first frame update
    void Start()
        {
            InvokeRepeating("Acceleration",5f, 5f);
        }

    // Update is called once per frame
    void Update()
        {
            m_rotation += new Vector3(Random.value/10, Random.value/10, Random.value/10);


            if(Input.GetKey(InputManager.Instance.right))
            {
                m_rotation += new Vector3(0.1f, 0, 0);
            }
            if(Input.GetKey(InputManager.Instance.left))
            {
                m_rotation += new Vector3(-0.1f, 0, 0);
            }
            if(Input.GetKey(InputManager.Instance.up))
            {
                m_rotation += new Vector3(0, 0.1f, 0);
            }
            if(Input.GetKey(InputManager.Instance.down))
            {
                m_rotation += new Vector3(0, -0.1f, 0);
            }

            m_rotation.Normalize();
            transform.Rotate(m_rotation * (m_rotationalSpeed * Time.deltaTime), Space.Self);
        }


    public void Acceleration()
        {
            m_rotationalSpeed += m_accelerationMultiplier;
        }
}
