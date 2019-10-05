﻿using System.Collections;
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
        if (hit.gameObject.CompareTag("Planet"))
        {
            foreach (ContactPoint contact in hit.contacts)
            {

                // Check for combinations??

                FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.anchor = contact.point;
                fixedJoint.connectedBody = hit.rigidbody;
            }
            this.GetComponent<Rigidbody>().useGravity = false;
            // Disable
            this.enabled = false;
        }else if (hit.gameObject.GetComponent<FixedJoint>())
        {
            foreach (ContactPoint contact in hit.contacts)
            {

                // Check for combinations??

                FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.anchor = contact.point;
                fixedJoint.connectedBody = hit.rigidbody;
            }
            this.GetComponent<Rigidbody>().useGravity = false;
            // Disable
            this.enabled = false;
        }

    }
}
