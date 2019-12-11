using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject fire;
    GameObject ice;
    GameObject wind;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fire = transform.Find("Fire").gameObject;
        ice = transform.Find("Ice").gameObject;
        wind = transform.Find("Wind").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        checkElement();
    }

    void checkElement()
    {
        if (player.GetComponent<Player>().Utype == Player.UltType.FIRE)
        {
            fire.SetActive(true);
            ice.SetActive(false);
            wind.SetActive(false);
        }
        if (player.GetComponent<Player>().Utype == Player.UltType.ICE)
        {
            fire.SetActive(false);
            ice.SetActive(true);
            wind.SetActive(false);
        }
        if (player.GetComponent<Player>().Utype == Player.UltType.WIND)
        {
            fire.SetActive(false);
            ice.SetActive(false);
            wind.SetActive(true);
        }
    }
}
