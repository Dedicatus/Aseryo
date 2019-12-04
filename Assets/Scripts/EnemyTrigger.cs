using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField]
    private bool repeat = false;
    
    bool triggered = false;
    bool allDead = false;
    public GameObject[] enemies;
    public GameObject[] spawnPoints;
    int enemyCount = 0;
    

    void OnTriggerEnter(Collider coll)
    {
        if (triggered == true || coll.tag != "Player") return;

        for (int i = 0; i < enemies.Length; ++i)
        {
            Instantiate(enemies[i], spawnPoints[i].gameObject.transform.position, spawnPoints[i].gameObject.transform.rotation);
            enemyCount++;
        }

        if (!repeat) triggered = true;
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
