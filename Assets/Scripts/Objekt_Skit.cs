using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objekt_Skit : MonoBehaviour
{
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
        foreach (ContactPoint contact in hit.contacts)
        {

            // Check for combinations??

            FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.anchor = contact.point;
            fixedJoint.connectedBody = hit.rigidbody;
        }
        this.enabled = false;
    }
}
