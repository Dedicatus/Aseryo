using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlarmCollision : MonoBehaviour
{
    GameObject player;
    GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = transform.parent.parent.gameObject;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Enemy.GetComponent<Enemy>().changeState(2);
        }
    }
}
