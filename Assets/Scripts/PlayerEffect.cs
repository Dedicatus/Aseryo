using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    Player player;
    public GameObject swipeEffectObj;
    public float swipeEffectRotationY = 0f;
    public float swipeEffectKillTime = 1.5f;

    public GameObject fireSwipeObj;
    public float fireSwipeRotationY = 0f;
    public float fireSwipeKillTime = 1.5f;

    public GameObject iceSwipeObj;
    public float iceSwipeRotationY = 0f;
    public float iceSwipeKillTime = 1.5f;

    public GameObject windSwipeObj;
    public float windSwipeRotationY = 0f;
    public float windSwipeKillTime = 1.5f;
    public float windSwipeDelay = 0.5f;

    public GameObject dashEffectObj;
    public float dashEffectRotationY = 0f;
    public float dashEffectKillTime = 1.5f;

    public GameObject explosionEffectObj;
    public float explosionEffectRotationY = 0f;
    public float explosionEffectKillTime = 1.5f;

    public GameObject ultEffectObj;
    public float ultEffectRotationY = 0f;
    public float ultEffectKillTime = 1.5f;

    private void Start()
    {
        player = transform.GetComponent<Player>();
    }

    public void startDashEffect()
    {
        startSwipeParticle();
        startDashParticle();
    }

    public void startUltEffect()
    {
        GameObject temp = Instantiate(ultEffectObj, transform.position, transform.rotation * Quaternion.Euler(0, ultEffectRotationY, 0), transform);
        Destroy(temp, ultEffectKillTime);
    }

    public void startExplosionEffect()
    {
        GameObject temp = Instantiate(explosionEffectObj, transform.position, transform.rotation * Quaternion.Euler(0, explosionEffectRotationY, 0), transform);
        Destroy(temp, explosionEffectKillTime);
    }

    private void startSwipeParticle()
    {
        GameObject temp;
        switch (player.Utype)
        {
            case Player.UltType.NONE:
                temp = Instantiate(swipeEffectObj, transform.position, transform.rotation * Quaternion.Euler(0, swipeEffectRotationY + 110f, 0), transform);
                Destroy(temp, swipeEffectKillTime);
                break;

            case Player.UltType.FIRE:
                temp = Instantiate(fireSwipeObj, transform.position, transform.rotation * Quaternion.Euler(0, fireSwipeRotationY + 110f, 0), transform);
                Destroy(temp, fireSwipeKillTime);
                break;

            case Player.UltType.ICE:
                temp = Instantiate(iceSwipeObj, transform.position, transform.rotation * Quaternion.Euler(0, iceSwipeRotationY, 0), transform);
                Destroy(temp, iceSwipeKillTime);
                break;

            case Player.UltType.WIND:
                Invoke("startWindSwipe", windSwipeDelay);
                break;
        }  
    }

    private void startWindSwipe()
    {
        GameObject temp = Instantiate(windSwipeObj, transform.position + new Vector3(0, 1.0f, 0), transform.rotation * Quaternion.Euler(0, 0, 0), GameObject.Find("Environment").transform.Find("Effect"));
        Destroy(temp, windSwipeKillTime);
    }
    private void startDashParticle()
    {
        GameObject temp = Instantiate(dashEffectObj, transform.position, transform.rotation * Quaternion.Euler(0, dashEffectRotationY, 0), GameObject.Find("Environment").transform.Find("Effect"));
        Destroy(temp, dashEffectKillTime);
    }
}
