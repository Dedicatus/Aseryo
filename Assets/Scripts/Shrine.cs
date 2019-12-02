using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    private bool activated;

    void Start()
    {
        activated = false;
    }

    void Update()
    {
        Debug.Log(activated);
    }

    public void ShrineEntered()
    {
        activated = true;
    }

    public void ShrineExited()
    {
        activated = false;
    }
}
