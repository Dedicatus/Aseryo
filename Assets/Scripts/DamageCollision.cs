using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class DamageCollision : MonoBehaviour
{
    GameObject player;
    GameObject Enemy;

    private void Start()
    {
        player = transform.parent.parent.gameObject;
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Enemy = coll.gameObject;
            player.GetComponent<Player>().addUltCharge();
            Debug.Log(player.GetComponent<Player>().ultCharge);
            if (coll.gameObject.GetComponent<Enemy>().getDestoried())
            { 
                Destroy(coll.gameObject);
                player.GetComponent<Player>().addExp(coll.gameObject.GetComponent<Enemy>().getExp());
                Debug.Log(player.GetComponent<Player>().exp);
                
                
            }
                
        }
    }
}
