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

        Debug.Log("My state is:" + mPlayer.state);
    }

    // Update is called once per frame
    void Update()
    {
        mPlayer = parent.GetComponent<Player>();

        Debug.Log("My state is:" + mPlayer.state);
        //gameObject.transform.parent.GetComponent(Player).state =
        //Debug.Log(gameObject.transform.parent.GetComponent(Player).state);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy" && mPlayer.state == playerStates.DASHING)
        {
            Destroy(coll.gameObject);
        }
    }
}
