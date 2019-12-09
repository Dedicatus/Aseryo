using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    GameObject player;
    RectTransform rect;
    bool isAddHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rect = GetComponent<RectTransform>();
        isAddHealth = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        if (player.GetComponent<Player>().isAddMaxHealth&&!isAddHealth)
        {
            isAddHealth = true;
            rect.sizeDelta = new Vector2(400f, 15f);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(35.0f, -29.0f);
        }
        if (isAddHealth)
        {
            float health = 3.4f * player.GetComponent<Player>().getHealth();
            rect.sizeDelta = new Vector2(health, 15f);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-165f + health / 2f, -29.0f);
        }
        else
        {
            float health = 3.5f * player.GetComponent<Player>().getHealth();
            rect.sizeDelta = new Vector2(health, 15f);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-155f + health / 2f, -29.0f);
        }
    }
}
