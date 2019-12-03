using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public int traitLevel;
    public enum lv1Trait { A, B, C };
    public enum lv2Trait { A, B, C };
    public enum lv3Trait { A, B, C };
    public enum lv4Trait { A, B, C };
    // Start is called before the first frame update
    void Start()
    {
        traitLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
