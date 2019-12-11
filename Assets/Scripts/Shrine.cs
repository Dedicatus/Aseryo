using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    private bool activated;
    private bool choosing;
    public bool isChoosed;
    public bool isChoosing;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        activated = false;
        choosing = false;
        isChoosed = false;
        isChoosing = false;
    }

    void Update()
    {
        inputHandler();
        checkGuide();
        checkSkill();
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
        if (!isChoosed&&(Input.GetAxis("LRT") < -0.19f) && activated && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().checkUlt())
        {
            GameObject.Find("UIManager").transform.Find("SkillSelect").GetComponent<SkillSelect>().startChoose();
            isChoosing = true;
        }
    }

    void checkSkill()
    {
        if (isChoosing)
        {
            int result = GameObject.Find("UIManager").transform.Find("SkillSelect").GetComponent<SkillSelect>().isChangeDone;
            Debug.Log(result);
            if (result == 1)
            {
                transform.Find("BuddhaStatus").transform.Find("pf_torch_stick_01").gameObject.SetActive(false);
                transform.Find("ShrineGuide").gameObject.SetActive(false);
                isChoosing = false;
                isChoosed = true;
                GameObject.Find("UIManager").transform.Find("SkillSelect").GetComponent<SkillSelect>().isChangeDone = 0;
            }
            if (result == 2)
            {
                isChoosing = false;
                GameObject.Find("UIManager").transform.Find("SkillSelect").GetComponent<SkillSelect>().isChangeDone = 0;
            }
        }
    }

    void checkGuide()
    {
        if (!isChoosed)
        { 
            if(player.GetComponent<Player>().checkUlt())
                transform.Find("ShrineGuide").gameObject.SetActive(true);
            else
                transform.Find("ShrineGuide").gameObject.SetActive(false);
        }
    }
}
