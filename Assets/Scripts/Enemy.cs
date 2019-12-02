﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform target;
    public GameObject player;
    public float playerAttack;
    public float Attack = 1f;
    public float Health = 2f;
    public Enemy()
    {
        

    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        playerAttack = player.GetComponent<Player>().Attack;
    }

    // Update is called once per frame
    void Update()
    {
        findPlayer();
    }

    void findPlayer()
    {
        if(!player.GetComponent<Player>().isCollision())
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public bool getDestoried()
    {
        Health -= playerAttack;
        if (Health <= 0)
            return true;
        return false;
    }

}
