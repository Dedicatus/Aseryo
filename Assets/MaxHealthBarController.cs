using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthBarController : MonoBehaviour
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
        if (player.GetComponent<Player>().isAddMaxHealth && !isAddHealth)
        {
            isAddHealth = true;
            rect.sizeDelta = new Vector2(500f, 7.35f);
            //GetComponent<RectTransform>().anchoredPosition = new Vector2(312.0f, -55.0f);
        }
    }
}
