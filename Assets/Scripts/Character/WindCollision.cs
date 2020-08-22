using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCollision : MonoBehaviour
{
    GameObject player;
    GameObject enemy;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider coll)
    {
        if (player == null) return;
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log(111);
            enemy = coll.gameObject;
            if (!enemy.GetComponent<Enemy>().isHitbyWind)
            {
                enemy.GetComponent<Enemy>().getHurt();
                enemy.GetComponent<Enemy>().windHit();
            }
        }

    }
}
