using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level;
    public Transform[] levelStartPoints;

    public float xMin, xMax, zMin, zMax;

    [Header("Level0")]
    public float lv0_XMin;
    public float lv0_XMax;
    public float lv0_ZMin; 
    public float lv0_ZMax;
    [Header("Level1")]
    public float lv1_XMin;
    public float lv1_XMax;
    public float lv1_ZMin;
    public float lv1_ZMax;
    [Header("Level2")]
    public float lv2_XMin;
    public float lv2_XMax;
    public float lv2_ZMin;
    public float lv2_ZMax;
    [Header("Level3")]
    public float lv3_XMin;
    public float lv3_XMax;
    public float lv3_ZMin;
    public float lv3_ZMax;
    [Header("Level4")]
    public float lv4_XMin;
    public float lv4_XMax;
    public float lv4_ZMin;
    public float lv4_ZMax;

    GameObject player;
    Rigidbody playerRB;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody>();
        level = 0;

        levelEdgeHandler();
    }

    public void nextLevel()
    {
        if (level < levelStartPoints.Length)
        {
            playerRB.MovePosition(levelStartPoints[level].position);
            ++level;
        }

        levelEdgeHandler();
    }

    private void levelEdgeHandler()
    {
        switch (level)
        {
            case 0:
                xMin = lv0_XMin;
                xMax = lv0_XMax;
                zMin = lv0_ZMin;
                zMax = lv0_ZMax;
                break;

            case 1:
                xMin = lv1_XMin;
                xMax = lv1_XMax;
                zMin = lv1_ZMin;
                zMax = lv1_ZMax;
                break;

            case 2:
                xMin = lv2_XMin;
                xMax = lv2_XMax;
                zMin = lv2_ZMin;
                zMax = lv2_ZMax;
                break;

            case 3:
                xMin = lv3_XMin;
                xMax = lv3_XMax;
                zMin = lv3_ZMin;
                zMax = lv3_ZMax;
                break;

            case 4:
                xMin = lv4_XMin;
                xMax = lv4_XMax;
                zMin = lv4_ZMin;
                zMax = lv4_ZMax;
                break;

            default:
                break;
        }
    }
}
