using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect : MonoBehaviour
{
    SkillManager skillManager;

    public GameObject traitButton1, traitButton2, traitButton3;
    private SkillManager.Trait lv1Trait, lv2Trait, lv3Trait, lv4Trait;
    //public GameObject gm;
    private void Start()
    {
        skillManager = transform.parent.parent.Find("SkillManager").GetComponent<SkillManager>();
        lv1Trait = skillManager.lv1Trait;
        lv2Trait = skillManager.lv2Trait;
        lv3Trait = skillManager.lv3Trait;
        lv4Trait = skillManager.lv4Trait;
    }

    void Update()
    {
        inputHandler();
    }

    void inputHandler()
    {
        if (Input.GetKey(KeyCode.JoystickButton1))
        {
            Debug.Log(111);
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void startChoose()
    {
        gameObject.SetActive(true);
        setUIContain();
        Time.timeScale = 0;  
    }

    public void chooseTrait(int traitNum)
    {
        skillManager.updateTrait(traitNum);
        lv1Trait = skillManager.lv1Trait;
        lv2Trait = skillManager.lv2Trait;
        lv3Trait = skillManager.lv3Trait;
        lv4Trait = skillManager.lv4Trait;

        gameObject.SetActive(false);
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().resetUltCharge();
    }

    private void setUIContain()
    {
        if (lv1Trait == SkillManager.Trait.NONE)
        {
            traitButton1.transform.GetComponentInChildren<Text>().text = "+DashArea";
            traitButton2.transform.GetComponentInChildren<Text>().text = "+HP";
            traitButton3.transform.GetComponentInChildren<Text>().text = "+UltTime";
            return;
        }

        if (lv2Trait == SkillManager.Trait.NONE)
        {
            traitButton1.transform.GetComponentInChildren<Text>().text = "-DashCD";
            traitButton2.transform.GetComponentInChildren<Text>().text = "Revive";
            traitButton3.transform.GetComponentInChildren<Text>().text = "AutoUltCharge";
            return;
        }

        if (lv3Trait == SkillManager.Trait.NONE)
        {
            traitButton1.transform.GetComponentInChildren<Text>().text = "Explose";
            traitButton2.transform.GetComponentInChildren<Text>().text = "AutoHeal";
            traitButton3.transform.GetComponentInChildren<Text>().text = "AJAYBE";
            return;
        }

        if (lv4Trait == SkillManager.Trait.NONE)
        {
            traitButton1.transform.GetComponentInChildren<Text>().text = "DashHeal";
            traitButton2.transform.GetComponentInChildren<Text>().text = "AvoidChance";
            traitButton3.transform.GetComponentInChildren<Text>().text = "AddPerChargeAmount";
            return;
        }

    }
}
