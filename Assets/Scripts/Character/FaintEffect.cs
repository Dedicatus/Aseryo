﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaintEffect : MonoBehaviour
{
    public GameObject faintEffectObj;
    public float effectRotationY = 0f;
    public float effectKillTime = 1.5f;

    public GameObject hurtEffectObj;
    public float hurtRotationY = 0f;
    public float hurtKillTime = 1.5f;

    public GameObject groundBloodObj;
    public float bloodRotationY = 0f;
    public float bloodKillTime = 500.0f;

    public void startFaintEffect()
    {
        GameObject temp = Instantiate(faintEffectObj, transform.position + new Vector3(0, 1.0f, 0), transform.rotation * Quaternion.Euler(0, effectRotationY + 180f, 0), GameObject.Find("Environment").transform.Find("Effect"));
        Destroy(temp, effectKillTime);
    }

    public void startGroundBlood()
    {
        GameObject temp = Instantiate(groundBloodObj, transform.position + new Vector3(0, 1.0f, 0), transform.rotation * Quaternion.Euler(0, effectRotationY, 0), GameObject.Find("Environment").transform.Find("Effect"));
        Destroy(temp, bloodKillTime);
    }

    public void startHurtEffect()
    {
        GameObject temp = Instantiate(hurtEffectObj, transform.position + new Vector3(0, 1.0f, 0), transform.rotation * Quaternion.Euler(0, hurtRotationY, 0), GameObject.Find("Environment").transform.Find("Effect"));
        Destroy(temp, hurtKillTime);
    }
}
