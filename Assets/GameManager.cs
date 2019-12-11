using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject UIManager;
    public float waitTime = 2f;
    float waitTimeCount;
    // Start is called before the first frame update
    void Start()
    {
        waitTimeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        countGameOver();
    }

    void countGameOver()
    {
        if (waitTimeCount > 0)
        {
            waitTimeCount -= Time.deltaTime;
            if (waitTimeCount <= 0)
            {
                Time.timeScale = 0;
                UIManager.GetComponent<UIManager>().showGameOver();
            }
        }

    }

    public void gameOver()
    {
        waitTimeCount = waitTime;
    }
}
