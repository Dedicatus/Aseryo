using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class DamageCollision : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = transform.parent.parent.gameObject;
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            player.GetComponent<Player>().addUltCharge(coll.gameObject.GetComponent<Enemy>().getUltCharge());
            Debug.Log(player.GetComponent<Player>().UltCharge);
            if (coll.gameObject.GetComponent<Enemy>().getDestoried())
            { 
                Destroy(coll.gameObject);
                player.GetComponent<Player>().addExp(coll.gameObject.GetComponent<Enemy>().getExp());
                Debug.Log(player.GetComponent<Player>().Exp);
                
                
            }
                
        }
    }
}
