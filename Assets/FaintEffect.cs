using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaintEffect : MonoBehaviour
{
    public GameObject faintEffectObj;
    public float effectRotationY = 0f;
    public float effectKillTime = 1.5f;
    bool effectAlive;

    public void startFaintParticle()
    {
        GameObject temp = Instantiate(faintEffectObj, transform.position, transform.rotation * Quaternion.Euler(0, effectRotationY + 180f, 0), GameObject.Find("BloodHolder").transform);
        Destroy(temp, effectKillTime);
    }
}
