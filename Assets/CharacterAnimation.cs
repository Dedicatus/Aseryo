using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    Player player;
    private float dashTimer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = transform.GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (player.state)
        {
            case Player.PlayerStates.IDLING:
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) { animator.Play("Idle"); }
                animator.SetBool("isMoving", false);
                break;
            case Player.PlayerStates.MOVING:
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) { animator.Play("Move"); }
                animator.SetBool("isMoving", true);
                break;
            case Player.PlayerStates.DASHING:
                animator.Play("Attack");
                break;
        }
    }
}
