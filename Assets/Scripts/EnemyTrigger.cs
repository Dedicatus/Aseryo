using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField]
    bool activated = true;
    [SerializeField]
    private bool repeat = false;
    
    bool triggered = false;
    bool allDead = false;
    public GameObject[] enemies;
    public GameObject[] spawnPoints;

    [Header("Chains")]
    public bool useTimer = false;
    public float endTime = 0.0f;
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

        if (useTimer)
        {
            GameObject.FindGameObjectWithTag("System").transform.Find("UIManager").GetComponent<UIManager>().showTimer(endTime);
        }

        enemyCount = enemies.Length;
        for (int i = 0; i < enemies.Length; ++i)
        {
            Instantiate(enemies[i], spawnPoints[i].gameObject.transform.position, spawnPoints[i].gameObject.transform.rotation, transform.Find("EnemyHolder").transform);
        }

        if (!repeat) triggered = true;                                                                                                                                                                                                                                                                                                            
    }

    void Update()
    {
        if (!triggered || allDead) return;

        if (triggered && useTimer)
        {
            if (endTime >= 0)
            {
                endTime -= Time.deltaTime;
            }
            else
            {
                enemyCount = 0;
            }
        }

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
