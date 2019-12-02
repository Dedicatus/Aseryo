﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineArea : MonoBehaviour
{

    private void Start()
    {
        
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider coll)
    {
        transform.parent.GetComponent<Shrine>().ShrineEntered();
    }

    private void OnTriggerExit(Collider coll)
    {
        transform.parent.GetComponent<Shrine>().ShrineExited();
    }
}
