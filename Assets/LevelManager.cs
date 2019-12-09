using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level;
    public Transform[] levelStartPoints;
    GameObject player;
    Rigidbody playerRB;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody>();
        level = 0;
    }

    public void nextLevel()
    {
        if (level < levelStartPoints.Length)
        {
            playerRB.MovePosition(levelStartPoints[level].position);
            ++level;
        }
    }
}
