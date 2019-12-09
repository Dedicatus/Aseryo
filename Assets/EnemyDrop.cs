using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject[] DropObjects;

    public void dropLoot()
    {
        int index = Random.Range(0, DropObjects.Length);

        GameObject temp = Instantiate(DropObjects[index], transform.position, transform.rotation * Quaternion.Euler(0, 0, 0), GameObject.Find("Environment").transform.Find("Loot"));
    }

    private void Update()
    {
        //Debug.Log(DropObjects.Length);
    }
}
