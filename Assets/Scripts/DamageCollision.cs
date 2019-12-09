using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class DamageCollision : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    FaintEffect faintEffect;
    EnemyDrop enemyDrop;

    private void Start()
    {
        player = transform.parent.parent.gameObject;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            enemy = coll.gameObject;
            player.GetComponent<Player>().addSingleKill();
            checkUltBuff(enemy);
            coll.gameObject.GetComponent<Enemy>().getHurt();
        }

    }

    void checkUltBuff(GameObject ee)
    {
        if (player.GetComponent<Player>().Utype == UltType.FIRE)
        {
            if (!ee.GetComponent<Enemy>().isFired)
            {
                ee.GetComponent<Enemy>().catchOnFire();
            }
        }
    }
}
