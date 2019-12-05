﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticle : MonoBehaviour
{
    public GameObject dashEffectObj;
    public float effectRotationY = 90f;
    public float effectKillTime = 1.5f;
    bool effectAlive;
    


    public void startDashParticle()
    {
        GameObject temp = Instantiate(dashEffectObj, transform.position, transform.rotation * Quaternion.Euler(0, effectRotationY, 0), transform);
        Destroy(temp, effectKillTime);
    }
}
