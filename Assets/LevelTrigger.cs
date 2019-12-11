using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    LevelManager levelManager;
    public bool activated = false;

    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("System").transform.Find("LevelManager").GetComponent<LevelManager>();

        if (!activated)
        {
            transform.parent.Find("Torch1").gameObject.SetActive(false);
            transform.parent.Find("Torch2").gameObject.SetActive(false);
            transform.parent.Find("Steam").gameObject.SetActive(false);
            transform.parent.Find("Guide").gameObject.SetActive(false);
            transform.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Update()
    {
        if (gameObject.GetComponent<BoxCollider>().enabled == true)
        {
            transform.GetComponent<BoxCollider>().enabled = true;
            transform.parent.Find("Torch1").gameObject.SetActive(true);
            transform.parent.Find("Torch2").gameObject.SetActive(true);
            transform.parent.Find("Steam").gameObject.SetActive(true);
            transform.parent.Find("Guide").gameObject.SetActive(true);
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
