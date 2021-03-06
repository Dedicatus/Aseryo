﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBarController : MonoBehaviour
{
    GameObject player;
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float Exp = 4 * player.GetComponent<Player>().getExp();
        rect.sizeDelta = new Vector2(Exp, 20f);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-200f + Exp/ 2f, 0);
    }
}
