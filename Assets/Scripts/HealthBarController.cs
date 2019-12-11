using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    GameObject player;
    RectTransform rect;
    bool isAddHealth;
    float healthRate;
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
            rect.sizeDelta = new Vector2(438f, 5f);
            //GetComponent<RectTransform>().anchoredPosition = new Vector2(312.0f, -55.0f);
        }
        if (isAddHealth)
        {
            float health = 2.19f * player.GetComponent<Player>().getHealth();
            rect.sizeDelta = new Vector2(health, 5f);
            //GetComponent<RectTransform>().anchoredPosition = new Vector2(-165f + health / 2f, -29.0f);
        }
        else
        {
            float health = 3.47f * player.GetComponent<Player>().getHealth();
            rect.sizeDelta = new Vector2(health, 5f);
            //GetComponent<RectTransform>().anchoredPosition = new Vector2(142f + health / 2f, -55.0f);
        }
    }
}
