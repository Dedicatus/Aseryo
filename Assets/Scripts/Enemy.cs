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
    public float onFireTime = 2.5f;
    public float FireCD = 0.2f;
    public float FireDamage = 1f;
    public float iceTime = 2f;
    public float Exp = 1f;
    public float Attack = 1f;
    public float Health = 2f;
    public float AttackTime = 1f;
    public float hitByWindCD = 1.5f;
    bool isHit;
    public bool isFired = false;
    public bool isIced = false;
    public bool isHitbyWind;
    float FireCDCount;
    float onFireCount;
    float iceCount;
    float hitByWindCount;
    public bool isAttackEnd;
    EnemyAttackCollision enemyAttackCollision;
    EnemyAlarmCollision enemyAlarmCollision;
    float AttackTimeCount;

    FaintEffect faintEffect;
    EnemyDrop enemyDrop;

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
        isFired = false;
        isHitbyWind = false;
        isAttackEnd = true;
        FireCDCount = 0f;
        hitByWindCount = 0f;
        enemyAttackCollision = transform.Find("Colliders").Find("AttackCollider").GetComponent<EnemyAttackCollision>();
        enemyAlarmCollision = transform.Find("Colliders").Find("AlarmCollider").GetComponent<EnemyAlarmCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyHandler();
    }



    void EnemyHandler()
    {
        checkDeath();
        colliderCheck();
        checkWindHit();
        if (onFireCount > 0)
            burn();
        if (iceCount > 0)
            iced();
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

    void checkDeath()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            if (transform.parent != null) transform.parent.parent.GetComponent<EnemyTrigger>().enemyCount--;
            faintEffect = transform.GetComponent<FaintEffect>();
            faintEffect.startFaintEffect();
            faintEffect.startGroundBlood();

            enemyDrop = transform.GetComponent<EnemyDrop>();
            enemyDrop.dropLoot();
        }
    }

    public void windHit()
    {
        isHitbyWind = true;
        hitByWindCount = hitByWindCD;
    }


    void checkWindHit()
    {
        if (hitByWindCount > 0)
        {
            hitByWindCount -= Time.deltaTime;
            if (hitByWindCount <= 0)
                isHitbyWind = false;
        }
    }        

    void colliderCheck()
    {
        if (player == null) return;
        if (!player.GetComponent<Player>().isCollision())
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
        else
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
    }

    public void catchOnFire()
    {
        isFired = true;
        onFireCount = onFireTime;
        FireCDCount = FireCD;
    }

    void burn()
    {
        onFireCount -= Time.deltaTime;
        FireCDCount -= Time.deltaTime;
        if (onFireCount >= 0)
        {
            if (FireCDCount <= 0)
            {
                Health -= FireDamage;
                FireCDCount = FireCD;
            }
        }
        
    }
    
    public void getIced()
    {
        isIced = true;
        iceCount = iceTime;
    }

    void iced()
    {
        iceCount -= Time.deltaTime;
        if (iceCount > 0)
        {
            state = EnemyStates.IDLING;
            enemyAlarmCollision.enabled = false;
        }
        else
        { 
            enemyAlarmCollision.enabled = true;
            state = EnemyStates.MOVING;
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
        if (target == null) return;
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

    public void getHurt()
    {
        Health -= playerAttack;
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
