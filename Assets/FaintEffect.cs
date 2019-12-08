using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaintEffect : MonoBehaviour
{
    public GameObject faintEffectObj;
    public float effectRotationY = 0f;
    public float effectKillTime = 1.5f;
    //bool effectAlive;

    public GameObject groundBloodObj;
    public float bloodRotationY = 0f;
    public float bloodKillTime = 1.5f;

    public void startFaintEffect()
    {
        GameObject temp = Instantiate(faintEffectObj, transform.position, transform.rotation * Quaternion.Euler(0, effectRotationY + 180f, 0), GameObject.Find("Environment").transform.Find("Effect"));
        Destroy(temp, effectKillTime);
    }

    public void startGroundBlood()
    {
        GameObject temp = Instantiate(groundBloodObj, transform.position, transform.rotation * Quaternion.Euler(0, effectRotationY, 0), GameObject.Find("Environment").transform.Find("Effect"));
        //Destroy(temp, effectKillTime);
    }
}
