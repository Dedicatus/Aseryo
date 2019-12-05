using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform target;
    public GameObject player;
    float playerAttack;
    public enum EnemyStates { IDLING, MOVING, ATTACKING };
    EnemyStates state;
    public float UltCharge = 1f;
    public float Exp = 1f;
    public float Attack = 1f;
    public float Health = 2f;
    
    public Enemy()
    {
        

    }
    // Start is called before the first frame update
    void Start()
    {
        state = EnemyStates.IDLING;
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        playerAttack = player.GetComponent<Player>().attack;
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
    }

    void followPlayer()
    {
        if (!player.GetComponent<Player>().isCollision())
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
        else
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void changeState(int flag)
    {
        switch (flag)
        {
            case 1:
                state = EnemyStates.IDLING;
                break;

            case 2:
                state= EnemyStates.MOVING;
                break;

            case 3:
                state = EnemyStates.ATTACKING;
                break;

        }
        
    }

    public bool getDestoried()
    {
        Health -= playerAttack;
        if (Health <= 0)
            return true;
        return false;
    }

    public float getUltCharge()
    {
        return UltCharge;
    }

    public float getExp()
    {
        return Exp;
    }

}
