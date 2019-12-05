using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    Player player;
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
                animator.Play("Idle");
                animator.SetBool("isMoving", false);
                animator.SetBool("isDashing", false);
                break;
            case Player.PlayerStates.MOVING:
                animator.Play("Move");
                animator.SetBool("isMoving", true);
                animator.SetBool("isDashing", false);
                break;
            case Player.PlayerStates.DASHING:
                animator.Play("Attack");
                animator.SetBool("isDashing", true);
                break;
        }
    }
}
