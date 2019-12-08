using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public GameObject swipeEffectObj;
    public float swipeEffectRotationY = 0f;
    public float swipeEffectKillTime = 1.5f;

    public GameObject dashEffectObj;
    public float dashEffectRotationY = 0f;
    public float dashEffectKillTime = 1.5f;

    //bool effectAlive;

    public void startDashEffect()
    {
        startSwipeParticle();
        startDashParticle();
    }

    public void startUltEffect()
    { 

    }

    private void startSwipeParticle()
    {
        GameObject temp = Instantiate(swipeEffectObj, transform.position, transform.rotation * Quaternion.Euler(0, swipeEffectRotationY + 110f, 0), transform);
        Destroy(temp, swipeEffectKillTime);
    }

    private void startDashParticle()
    {
        GameObject temp = Instantiate(dashEffectObj, transform.position, transform.rotation * Quaternion.Euler(0, dashEffectRotationY, 0), GameObject.Find("Environment").transform.Find("Effect"));
        Destroy(temp, dashEffectKillTime);
    }
}
