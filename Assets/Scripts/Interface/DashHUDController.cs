using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashHUDController : MonoBehaviour
{
    GameObject playerObj;
    Player player;
    Image img;
    Color targetColor;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        img = GetComponent<Image>();
        targetColor = img.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        if (player.singleDashCount >= 3)
        {
            img.color = new Color(0.8f, 0.05f, 0);
            targetColor = new Color(0.8f, 0.05f, 0);
            return;
        }
        if (player.dashCDcount <= 0)
        {
            targetColor = new Color(0.8f, 0.05f, 0);
        }

        if (player.state == Player.PlayerStates.DASHING)
        {
            img.color = new Color(0.8f, 0.8f, 0.8f);
            targetColor = new Color(0.8f, 0.8f, 0.8f);
        }

        img.color = Color.Lerp(img.color, targetColor, 0.2f);

    }
}
