using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject skillSelect, PlayerStatus, gameOver, Timer;
    bool timerActive;
    float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        
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
