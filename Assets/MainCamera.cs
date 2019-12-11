using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera current;

    public float[] cameraBoundaries;

    public Player player;
    public Player otherFocus = null;

    bool paused = false;

    public MainCamera()
    {
        current = this;

    }
    // Use this for initialization
    void Start()
    {
        current = this;
        cameraBoundaries = new float[4] { -100, -100, 100, 100 }; //minx, miny, maxx, maxy
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector3 pos = transform.position;
        pos.y = 0;
        Vector3 minus;
        if (otherFocus == null || !otherFocus.isActiveAndEnabled)
        {
            minus = player.transform.position - pos;
            if (minus.magnitude <= 1.5f)
            {
                pos.x = Mathf.Clamp(player.transform.position.x, cameraBoundaries[0], cameraBoundaries[2]);
                pos.z = Mathf.Clamp(player.transform.position.z, cameraBoundaries[1], cameraBoundaries[3]);
            }
            else
            {
                pos.x = Mathf.Clamp(transform.position.x + minus.normalized.x * 2f, cameraBoundaries[0], cameraBoundaries[2]);
                pos.z = Mathf.Clamp(transform.position.z + minus.normalized.z * 2f, cameraBoundaries[1], cameraBoundaries[3]);
            }
        }
        else
        {
            minus = (otherFocus.transform.position + player.transform.position) / 2f - pos;
            Vector3 midp = (otherFocus.transform.position + player.transform.position) / 2f;
            if (minus.magnitude <= 1.5f)
            {
                pos.x = Mathf.Clamp(midp.x, cameraBoundaries[0], cameraBoundaries[2]);
                pos.z = Mathf.Clamp(midp.z, cameraBoundaries[1], cameraBoundaries[3]);
            }
            else
            {
                pos.x = Mathf.Clamp(transform.position.x + minus.normalized.x * 1f, cameraBoundaries[0], cameraBoundaries[2]);
                pos.z = Mathf.Clamp(transform.position.z + minus.normalized.z * 1f, cameraBoundaries[1], cameraBoundaries[3]);
            }
        }

        pos.y = transform.position.y;
        transform.position = pos;
    }
}
