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
            player.GetComponent<Player>().addUltCharge();

            //Debug.Log(player.GetComponent<Player>().ultCharge);
            player.GetComponent<Player>().addSingleKill();
            if (coll.gameObject.GetComponent<Enemy>().getDestoried())
            { 
                Destroy(coll.gameObject);

                if (enemy.transform.parent != null)  enemy.transform.parent.parent.GetComponent<EnemyTrigger>().enemyCount--;
                faintEffect = enemy.transform.GetComponent<FaintEffect>();
                faintEffect.startFaintEffect();
                faintEffect.startGroundBlood();

                enemyDrop = enemy.transform.GetComponent<EnemyDrop>();
                enemyDrop.dropLoot();

                player.GetComponent<Player>().addExp(coll.gameObject.GetComponent<Enemy>().getExp());
                //Debug.Log(player.GetComponent<Player>().exp);

            }
                
        }

    }
}
