using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongMaxHealthController : MonoBehaviour
{
    GameObject player;
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.GetComponent<Player>().isAddMaxHealth)
        {
            img.enabled = true;
            //gameObject.SetActive(true);
        }
    }
}
