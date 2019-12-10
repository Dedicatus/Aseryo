﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    GameObject playerObject;
    Player player;
    public enum Trait { NONE, FIRST, SECOND, THIRD };
    private bool isAutoCharge = false;

    [Header("Lv1Trait")]
    [SerializeField]
    private float DashAreaMultipler = 2.0f;
    [SerializeField]
    private float MaxHealthMultipler = 1.5f;
    [SerializeField]
    private float UltTimeMultipler = 1.5f;

    [Header("Lv2Trait")]
    [SerializeField]
    private float DashCDMultipler = 0.5f;
    private float ChargeRecover = 0.01f;

    [Header("Lv3Trait")]
    [SerializeField]
    private float HealthRecover = 0.01f;
    [SerializeField]
    private float UltTimeMultiplerLV3 = 1.5f;


    [Header("Debug")]
    public Trait lv1Trait;
    public Trait lv2Trait;
    public Trait lv3Trait;
    public Trait lv4Trait;

    // Start is called before the first frame update
    void Start()
    {
        lv1Trait = Trait.NONE; 
        lv3Trait = Trait.NONE;
        lv4Trait = Trait.NONE;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
    }

    void Update()
    {
        if (isAutoCharge)
        {
            autoCharge();
        }
    }

    public void updateTrait(int traitNum)
    {
        if (lv1Trait == Trait.NONE)
        {
            //Debug.Log("die");
            lv1Trait = setTrait(traitNum);
            lv1TraitHandler();
            return;
        }

        if (lv2Trait == Trait.NONE)
        {
            lv2Trait = setTrait(traitNum);
            lv2TraitHandler();
            return;
        }

        if (lv3Trait == Trait.NONE)
        {
            lv3Trait = setTrait(traitNum);
            lv3TraitHandler();
            return;
        }

        if (lv4Trait == Trait.NONE)
        {
            lv4Trait = setTrait(traitNum);
            lv4TraitHandler();
            return;
        }

    }

    private Trait setTrait(int traitNum)
    {
        Trait trait = Trait.NONE;
        switch (traitNum)
        {
            case 0:
                trait = Trait.FIRST;
                break;
            case 1:
                trait = Trait.SECOND;
                break;
            case 2:
                trait = Trait.THIRD;
                break;
            default:
                trait = Trait.NONE;
                break;
        }
        return trait;
    }

    private void lv1TraitHandler()
    {
        switch (lv1Trait)
        {
            case Trait.FIRST:
                playerObject.transform.Find("Colliders").Find("DashCollider").GetComponent<BoxCollider>().size = new Vector3(playerObject.transform.Find("Colliders").Find("DashCollider").GetComponent<BoxCollider>().size.x * DashAreaMultipler, 1, 1);
                playerObject.transform.Find("Colliders").Find("UltCollider").GetComponent<BoxCollider>().size = new Vector3(playerObject.transform.Find("Colliders").Find("UltCollider").GetComponent<BoxCollider>().size.x * DashAreaMultipler, 1, 1);
                break;
            case Trait.SECOND:
                player.changeMaxHealth();
                break;
            case Trait.THIRD:
                player.ultTime *= UltTimeMultipler;
                break;
            default:
                break;
        }
    }

    private void lv2TraitHandler()
    {
        switch (lv2Trait)
        {
            case Trait.FIRST:
                player.dashCD *= DashCDMultipler;
                break;
            case Trait.SECOND:
                player.reviveTimes = 1;
                break;
            case Trait.THIRD:
                isAutoCharge = true;    
                break;
            default:
                break;
        }
    }

    private void lv3TraitHandler()
    {
        switch (lv3Trait)
        {
            case Trait.FIRST:
                player.isExploseOpen = true;
                break;
            case Trait.SECOND:
                break;
            case Trait.THIRD:
                /*if(player.state == DASHING)
                {
                    if (player.CanDashTwice == true)
                    {
                        player.state = Moving;
                        player.CanDashTwice == false;
                    }
                }
                else
                {
                    player.CanDashTwice == ture;
                }*/
                break;
            default:
                break;
        }
    }

    private void lv4TraitHandler()
    {
        switch (lv4Trait)
        {
            case Trait.FIRST:
                //player.DashHpRecover = true;
                break;
            case Trait.SECOND:

                break;
            case Trait.THIRD:
                break;
            default:
                break;
        }
    }

    private void autoCharge()
    {
        player.addUltCharge(ChargeRecover);
    }
}
