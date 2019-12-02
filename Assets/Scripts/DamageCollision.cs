using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class DamageCollision : MonoBehaviour
{
    GameObject player;
    
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            if(coll.gameObject.GetComponent<Enemy>().getDestoried())
            //Debug.Log(coll.gameObject.GetComponent<Enemy>().Health);
            //Debug.Log(player.GetComponent<Player>().Attack);
            //if (coll.gameObject.GetComponent<Enemy>().Health <= 0f)
                Destroy(coll.gameObject);
        }
    }
}
