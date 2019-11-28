using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{

    bool triggered = false;
    bool allDead = false;
    public GameObject[] enemies;
    public GameObject[] spawnPoints;
    int enemyCount = 0;

    void Start()
    {

    }


    void OnTriggerEnter(Collider coll)
    {
        if (triggered == true || coll.tag != "Player") return;
        triggered = true;

        for (int i = 0; i < enemies.Length; ++i)
        {
            Instantiate(enemies[i], spawnPoints[i].gameObject.transform);
            enemyCount++;
        }

    }

        void Update()
    {
        if (!triggered || allDead) return;
        int deadCount = enemyCount;
        for (int a = 0; a < enemyCount; ++a)
        {
            if (enemies[a] == null) deadCount--;
        }
        if (deadCount == 0)
        {
            allDead = true;
        }
    }
        
}
