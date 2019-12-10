using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public bool activated = true;
    [SerializeField]
    private bool repeat = false;
    
    bool triggered = false;
    bool allDead = false;
    public GameObject[] enemies;
    public GameObject[] spawnPoints;
    public GameObject[] chainedTrigger;

    public int enemyCount;

    private void Start()
    {
        if (!activated)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (triggered == true || coll.tag != "Player") return;
        

        enemyCount = enemies.Length;
        for (int i = 0; i < enemies.Length; ++i)
        {
            Instantiate(enemies[i], spawnPoints[i].gameObject.transform.position, spawnPoints[i].gameObject.transform.rotation, transform.Find("EnemyHolder").transform);
        }

        //Debug.Log("1: " + enemyCount);
        if (!repeat) triggered = true;                                                                                                                                                                                                                                                                                                            
    }

    void Update()
    {
        if (!triggered || allDead) return;
        if (enemyCount == 0)
        {
            allDead = true;
            if (chainedTrigger != null)
            {
                for (int i = 0; i < chainedTrigger.Length; ++i)
                {
                    chainedTrigger[i].transform.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
    }
        
}
