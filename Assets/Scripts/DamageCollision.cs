using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class DamageCollision : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    enum DamageType { Dash, Explose };
    [SerializeField] DamageType dmgType;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void OnTriggerEnter(Collider coll)
    {
        if (player == null) return;
        if (coll.gameObject.tag == "Enemy")
        {   
            enemy = coll.gameObject;
            if(dmgType == DamageType.Dash)
                player.GetComponent<Player>().addSingleKill();
            checkUltBuff(enemy);
            coll.gameObject.GetComponent<Enemy>().getHurt();
        }

    }

    void checkUltBuff(GameObject ee)
    {
        if (player == null) return;
        if (player.GetComponent<Player>().Utype == UltType.FIRE)
        {
            if (!ee.GetComponent<Enemy>().isFired)
            {
                ee.GetComponent<Enemy>().catchOnFire();
            }
        }
        if (player.GetComponent<Player>().Utype == UltType.ICE)
        {
            if (!ee.GetComponent<Enemy>().isIced)
            {
                ee.GetComponent<Enemy>().getIced();
            }
        }
    }
}
