﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnJointBreak(float breakForce)
    {
        //Debug.Log("A joint has just been broken!, force: " + breakForce);
        this.transform.parent = null;
        Invoke("SelfDestruct",10);
    }

    void SelfDestruct()
    {

      Destroy(this.gameObject);
    }
}