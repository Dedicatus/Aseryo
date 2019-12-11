using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject skillSelect, PlayerStatus, gameOver, Timer, KillCount;
    bool timerActive;
    float timeCount;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timeCount -= Time.deltaTime;
            Timer.GetComponent<Text>().text = Mathf.RoundToInt(timeCount).ToString();

            if (timeCount <= 0)
            {
                hideTimer();
                Timer.SetActive(false);
                timerActive = false;
            }
        }

        if (player == null) return;
        KillCount.GetComponent<Text>().text = Mathf.RoundToInt(player.GetComponent<Player>().killCount).ToString();

    }

    public void showGameOver()
    {
        gameOver.SetActive(true);
    }

    public void showTimer(float time)
    {
        Timer.SetActive(true);
        timerActive = true;
        timeCount = time;
    }

    void hideTimer()
    {
        Timer.SetActive(true);
        timerActive = true;
    }
}
