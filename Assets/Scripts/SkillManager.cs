using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public enum Trait { NONE, FIRST, SECOND, THIRD };
    public Trait lv1Trait, lv2Trait, lv3Trait, lv4Trait;
    // Start is called before the first frame update
    void Start()
    {
        lv1Trait = Trait.NONE;
        lv2Trait = Trait.NONE;
        lv3Trait = Trait.NONE;
        lv4Trait = Trait.NONE;
    }

    void Update()
    {
        
    }

    public void updateTrait(int traitNum)
    {
        if (lv1Trait == Trait.NONE)
        {
            //Debug.Log("die");
            lv1Trait = setTrait(traitNum);
            return;
        }

        if (lv2Trait == Trait.NONE)
        {
            lv2Trait = setTrait(traitNum);
            return;
        }

        if (lv3Trait == Trait.NONE)
        {
            lv3Trait = setTrait(traitNum);
            return;
        }

        if (lv4Trait == Trait.NONE)
        {
            lv4Trait = setTrait(traitNum);
            return;
        }
    }

    private Trait setTrait(int traitNum)
    {
        Trait trait = Trait.NONE;
        switch (traitNum)
        {
            case 0:
                trait = Trait.FIRST;
                break;
            case 1:
                trait = Trait.SECOND;
                break;
            case 2:
                trait = Trait.THIRD;
                break;
        }
        return trait;
    }
}
