using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objekt_Skit : MonoBehaviour
{
    // 0 rock
    // 1 water
    // 2 tree
    // 3 human

    public int objtype = 0;
    public bool m_en = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }




    void OnCollisionEnter(Collision hit)
    {
        if (m_en)
        {
            if (hit.gameObject.CompareTag("Planet"))
            {
                foreach (ContactPoint contact in hit.contacts)
                {

                    // Check for combinations??

                    FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
                    fixedJoint.anchor = contact.point;
                    fixedJoint.connectedBody = hit.rigidbody;
                    fixedJoint.breakForce = 1500;
                    if (!PlanetJointHandler.Instance.m_connected.Contains(this.gameObject))
                    {
                        PlanetJointHandler.Instance.m_connected.Add(this.gameObject);
                    }
                }
                this.GetComponent<Rigidbody>().useGravity = false;
                this.transform.parent = hit.transform;
                // Disable
                m_en = false;
            }
            else if (hit.gameObject.GetComponent<FixedJoint>())
            {
                foreach (ContactPoint contact in hit.contacts)
                {

                    // Check for combinations??

                    FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
                    fixedJoint.anchor = contact.point;
                    fixedJoint.connectedBody = hit.rigidbody;
                    fixedJoint.breakForce = 1000;
                    if (!PlanetJointHandler.Instance.m_connected.Contains(this.gameObject))
                    {
                        PlanetJointHandler.Instance.m_connected.Add(this.gameObject);
                    }
                }
                this.transform.parent = hit.transform;
                this.GetComponent<Rigidbody>().useGravity = false;
                // Disable
                m_en = false;
            }
        }

    }
}
