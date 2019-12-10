using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    GameObject playerObj;
    Player player;
    public Image dash, dashShining, explose;
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
        }

        if (player.canExploseThisDash)
        {
            explose.color = new Color(0.76f, 0, 0, 1.0f);
        }
        else
        {
            explose.color = new Color(0.76f, 0.76f, 0.76f, 1.0f);
        }
    }

    private void dashHUDHandler()
    {
        if (player.singleDashCount >= 3)
        {
            //dash.color = new Color(0.8f, 0.05f, 0);
            //targetColor = new Color(0.8f, 0.05f, 0);
            dashShining.color = new Color(0.8f, 0.05f, 0, 0.39f);
            return;
        }
        if (player.dashCDcount <= 0)
        {
            //targetColor = new Color(0.8f, 0.05f, 0);
            shiningTargetColor = new Color(0.8f, 0.05f, 0, 0.39f);
        }

        if (player.state == Player.PlayerStates.DASHING)
        {
            //dash.color = new Color(0.8f, 0.8f, 0.8f);
            //targetColor = new Color(0.8f, 0.8f, 0.8f);
            shiningTargetColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        dash.color = Color.Lerp(dash.color, targetColor, 0.2f);
        dashShining.color = Color.Lerp(dashShining.color, shiningTargetColor, 0.2f);
    }
}
