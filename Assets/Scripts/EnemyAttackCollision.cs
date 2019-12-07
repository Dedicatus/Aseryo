using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollision : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    GameObject Enemy;
    public bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = transform.parent.parent.gameObject;
        inRange = false;
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if(Enemy.GetComponent<Enemy>().isAttackEnd)
                Enemy.GetComponent<Enemy>().changeState(3);
            inRange = true;
            Debug.Log("inRange, Yes");
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            inRange = false;
            Debug.Log("inRange, No! ");
        }
    }
}
