using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltBarController : MonoBehaviour
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
        float Ult = 4 * player.GetComponent<Player>().getUltCharge();
        rect.sizeDelta = new Vector2(Ult, 20f);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-200f + Ult / 2f, 0);
    }
}
