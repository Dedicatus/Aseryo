using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputHandler();
    }

    private void inputHandler()
    {
        if (Input.GetKey(KeyCode.JoystickButton3) && GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().state == GameManager.GameStates.Playing)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            Time.timeScale = 1;
        }
    }

}
