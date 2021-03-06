﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    GameObject playerObj;
    Player player;
    public Image dash, dashShining, explose, exploseShining;
    Color targetColor;
    Color shiningTargetColor;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        targetColor = dash.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        dashHUDHandler();

        if (player.isExploseOpen)
        {
            transform.Find("Explose").gameObject.SetActive(true);
            transform.Find("ExploseShining").gameObject.SetActive(true);
        }

        if (player.canExploseThisDash)
        {
            //explose.color = new Color(0.76f, 0, 0, 1.0f);
            exploseShining.color = new Color(0.8f, 0.05f, 0, 0.39f);
        }
        else
        {
            //explose.color = new Color(0.76f, 0.76f, 0.76f, 1.0f);
            exploseShining.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        if (player.checkUlt()&&!player.isUltra)
        {
            transform.Find("UltimateShining").gameObject.SetActive(true);
            transform.Find("Ultimate").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Ultimate").gameObject.SetActive(true);
            transform.Find("UltimateShining").gameObject.SetActive(false);
        }

    }

    private void dashHUDHandler()
    {
        if (player.singleDashCount >= 3)
        {
            //dash.color = new Color(0.8f, 0.05f, 0);
            //targetColor = new Color(0.8f, 0.05f, 0);
            dashShining.color = new Color(0.8f, 0.05f, 0, 0.39f);
            transform.Find("DashGuide").gameObject.SetActive(true);
            return;
        }
        if (player.dashCDcount <= 0)
        {
            //targetColor = new Color(0.8f, 0.05f, 0);
            shiningTargetColor = new Color(0.8f, 0.05f, 0, 0.39f);
            transform.Find("DashGuide").gameObject.SetActive(true);
        }

        if (player.state == Player.PlayerStates.DASHING)
        {
            //dash.color = new Color(0.8f, 0.8f, 0.8f);
            //targetColor = new Color(0.8f, 0.8f, 0.8f);
            transform.Find("DashGuide").gameObject.SetActive(false);
            shiningTargetColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        dash.color = Color.Lerp(dash.color, targetColor, 0.2f);
        dashShining.color = Color.Lerp(dashShining.color, shiningTargetColor, 0.2f);
    }
}
