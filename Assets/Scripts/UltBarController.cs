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
        float Ult = player.GetComponent<Player>().getUltCharge();
        float width = 0.6f*Ult;
        if (Ult >= 200f)
            width += 20f;
        else if (Ult < 200f && Ult >= 100f)
            width += 10f;
        rect.sizeDelta = new Vector2(width, 22f);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-86f + width / 2f, -24f);
    }
}
