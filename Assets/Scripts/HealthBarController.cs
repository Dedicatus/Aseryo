using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
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
        float health = 4*player.GetComponent<Player>().getHealth();
        rect.sizeDelta = new Vector2(health, 20f);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-200f + health / 2f, 0);
    }
}
