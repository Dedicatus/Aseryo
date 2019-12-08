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
    public EnemyStates state;
    public float UltCharge = 1f;
    public float Exp = 1f;
    public float Attack = 1f;
    public float Health = 2f;
    public float AttackTime = 1f;
    bool isHit;
    public bool isAttackEnd;
    EnemyAttackCollision enemyAttackCollision;
    float AttackTimeCount;

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
        AttackTimeCount = AttackTime;
        isHit = false;
        isAttackEnd = true;
        enemyAttackCollision = transform.Find("Colliders").Find("AttackCollider").GetComponent<EnemyAttackCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyHandler();
    }

    void EnemyHandler()
    {
        colliderCheck();
        switch (state)
        {
            case EnemyStates.IDLING:
                break;
            
            case EnemyStates.MOVING:
                followPlayer();
                break;

            case EnemyStates.ATTACKING:
                attackPlayer();
                break;
        }
    }

    void colliderCheck()
    { 
        if (!player.GetComponent<Player>().isCollision())
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
        else
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
    }

    void attackPlayer()
    {
        AttackTimeCount -= Time.deltaTime;
        
        if (AttackTimeCount <= 0)
        {
            isAttackEnd = true;
            if (enemyAttackCollision.inRange)
                player.GetComponent<Player>().getAttacked(Attack);
            state = EnemyStates.MOVING;
        }

    }

    void followPlayer()
    {
        
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
                isAttackEnd = false;
                AttackTimeCount = AttackTime;
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
