using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashHUDController : MonoBehaviour
{
    GameObject player;
    Image img;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        img.color = Color.Lerp(new Color(0, 0, 0), new Color(0.8f, 0.05f, 0), Mathf.PingPong(Time.time, 1));
    }
}
