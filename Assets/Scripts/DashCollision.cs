using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class DashCollision : MonoBehaviour
{
    public GameObject parent;
    Player mPlayer;

    // Start is called before the first frame update
    void Start()
    {
        mPlayer = parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        mPlayer = parent.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(coll.gameObject);
        }
    }
}
