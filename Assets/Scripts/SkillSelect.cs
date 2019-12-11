using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect : MonoBehaviour
{
    SkillManager skillManager;

    public int selectNum = 1;
    public GameObject traitButton1;
    public Text traitText1;
    public Image traitImg1;
    public GameObject select1;
    public GameObject traitButton2;
    public Text traitText2;
    public Image traitImg2;
    public GameObject select2;
    public GameObject traitButton3;
    public Text traitText3;
    public Image traitImg3;
    public GameObject select3;

    public bool isChanged = false;

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

        switch (selectNum)
        {
            case 0:
                select1.SetActive(true);
                select2.SetActive(false);
                select3.SetActive(false);
                break;
            case 1:
                select1.SetActive(false);
                select2.SetActive(true);
                select3.SetActive(false);
                break;
            case 2:
                select1.SetActive(false);
                select2.SetActive(false);
                select3.SetActive(true);
                break;
        }
    }

    void inputHandler()
    {
        if (Input.GetKey(KeyCode.JoystickButton1))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        if (Input.GetAxis("Horizontal_L") > 0.19f)
        {
            if (selectNum < 2 && !isChanged)
            {
                ++selectNum;
                isChanged = true;
            }
        }

        if (Input.GetAxis("Horizontal_L") < -0.19f)
        {
            if (selectNum > 0 && !isChanged)
            {
                selectNum--;
                isChanged = true;
            }
        }
        if (Input.GetAxis("Horizontal_L") > -0.19f && Input.GetAxis("Horizontal_L") < 0.19f)
        {
            isChanged = false;
        }

        if (Input.GetKey(KeyCode.JoystickButton2))
        {
            chooseTrait(selectNum);
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
            traitText1.text = "Increase attack range";
            traitText2.text = "Increase maximum HP";
            traitText3.text = "Increase Aseryo time";
            traitImg1.color = new Color(0.859f, 0.859f, 0.859f, 0.804f);
            traitImg2.color = new Color(0.859f, 0.859f, 0.859f, 0.804f);
            traitImg3.color = new Color(0.859f, 0.859f, 0.859f, 0.804f);
            return;
        }

        if (lv2Trait == SkillManager.Trait.NONE)
        {
            traitText1.text = "Reduce attack cooldown";
            traitText2.text = "Resurrection once after death";
            traitText3.text = "Regenerate Qi automatically";
            traitImg1.color = new Color(0.306f, 0.49f, 0.255f, 0.804f);
            traitImg2.color = new Color(0.306f, 0.49f, 0.255f, 0.804f);
            traitImg3.color = new Color(0.306f, 0.49f, 0.255f, 0.804f);
            return;
        }

        if (lv3Trait == SkillManager.Trait.NONE)
        {
            traitText1.text = "New skill: Qi Explosion";
            traitText2.text = "Regenerate HP automatically";
            traitText3.text = "Qi Steal";
            traitImg1.color = new Color(0.388f, 0.412f, 0.576f, 0.804f);
            traitImg2.color = new Color(0.388f, 0.412f, 0.576f, 0.804f);
            traitImg3.color = new Color(0.388f, 0.412f, 0.576f, 0.804f);
            return;
        }

        if (lv4Trait == SkillManager.Trait.NONE)
        {
            traitText1.text = "Lifesteal";
            traitText2.text = "Dodging";
            traitText3.text = "Increase Qi gaining greatly ";
            traitImg1.color = new Color(0.518f, 0.365f, 0.365f, 0.804f);
            traitImg2.color = new Color(0.518f, 0.365f, 0.365f, 0.804f);
            traitImg3.color = new Color(0.518f, 0.365f, 0.365f, 0.804f);
            return;
        }

    }
}
