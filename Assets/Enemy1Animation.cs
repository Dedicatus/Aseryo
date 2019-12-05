using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Animation : MonoBehaviour
{
    private Animator animator;
    Enemy enemy;
    public float attackCD=0.5f;
    float attackCDCount;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = transform.GetComponentInParent<Enemy>();
        //attackCDCount = attackCD;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackGap")) { attackCDCount -= Time.deltaTime; Debug.Log(attackCDCount);}
        if (attackCDCount <= 0)
        {
            
            animator.SetTrigger("startAttack");
            attackCDCount = attackCD;
        }
        */
        switch (enemy.state)
        {
            case Enemy.EnemyStates.IDLING:
                animator.Play("Idle");
                break;
            case Enemy.EnemyStates.MOVING:
                animator.Play("Move");
                break;
            case Enemy.EnemyStates.ATTACKING:
                animator.Play("Attack");
                break;
        }
    }
}
