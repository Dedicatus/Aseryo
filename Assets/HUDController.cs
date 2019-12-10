using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    GameObject playerObj;
    Player player;
    public Image dash,Shining;
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
        if (player.singleDashCount >= 3)
        {
            //dash.color = new Color(0.8f, 0.05f, 0);
            //targetColor = new Color(0.8f, 0.05f, 0);
            Shining.color = new Color(0.8f, 0.05f, 0, 0.39f);
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
        Shining.color = Color.Lerp(Shining.color, shiningTargetColor, 0.2f);

    }
}
