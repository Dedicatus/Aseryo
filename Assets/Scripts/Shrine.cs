using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    private bool activated;

    void Start()
    {
        activated = false;
    }

    void Update()
    {
        inputHandler();
    }

    public void ShrineEntered()
    {
        activated = true;
    }

    public void ShrineExited()
    {
        activated = false;
    }

    private void inputHandler()
    {
        if (Input.GetKey(KeyCode.JoystickButton6) && activated)
        {
            GameObject.Find("UImanager").transform.Find("SkillSelect").GetComponent<SkillSelect>().startChoose();
        }
    }

}
