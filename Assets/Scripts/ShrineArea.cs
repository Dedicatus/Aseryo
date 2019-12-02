using System.Collections;
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
        Shrine shrine = transform.parent.GetComponent<Shrine>();
        shrine.ShrineEntered();
    }

    private void OnTriggerExit(Collider coll)
    {
        Shrine shrine = transform.parent.GetComponent<Shrine>();
        shrine.ShrineExited();
    }
}
