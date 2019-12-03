using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect : MonoBehaviour
{
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startChoose()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void chooseTrait()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
