using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameStates { StartScreen, Playing, EndScreen};
    public GameStates state;
    public GameObject UIManager;
    public float waitTime = 2f;
    float waitTimeCount;
    // Start is called before the first frame update
    void Start()
    {
        state = GameStates.StartScreen;
        waitTimeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        countGameOver();
        inputHandler();
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

    void inputHandler()
    {
        switch (state)
        {
            case GameStates.StartScreen:
                Time.timeScale = 0.0f;
                if (Input.GetKey(KeyCode.JoystickButton2))
                {
                    state = GameStates.Playing;
                }
                break;

            case GameStates.Playing:
                Time.timeScale = 1.0f;
                break;

            case GameStates.EndScreen:
                Time.timeScale = 0.0f;
                if (Input.GetKey(KeyCode.JoystickButton2))
                {
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                    state = GameStates.StartScreen;
                }
                break;
        }

    }
}
