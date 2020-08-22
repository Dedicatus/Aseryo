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
        if (player == null) return;
        float Ult = player.GetComponent<Player>().getUltCharge();
        float width = 0.9f*Ult;
        if (Ult >= 180f)
            width += 30f;
        else if (Ult < 180f && Ult >= 90f)
            width += 15f;
        rect.sizeDelta = new Vector2(width, 24f);
    }
}
