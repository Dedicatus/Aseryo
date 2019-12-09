using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    LevelManager levelManager;
    public bool activated = false;

    private void Start()
    {
        levelManager = GameObject.Find("System").transform.Find("LevelManager").GetComponent<LevelManager>();

        if (!activated)
        {
            transform.Find("Torch").gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Update()
    {
        if (gameObject.GetComponent<BoxCollider>().enabled == true)
        {
            transform.Find("Torch").gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Dash")
        {
            levelManager.nextLevel();
        }
    }
}
