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

    public void updateTrait(int traitNum)
    {
        if (lv1Trait == Trait.NONE)
        {
            Debug.Log("die");
            setTrait(lv1Trait, traitNum);
            return;
        }

        if (lv2Trait == Trait.NONE)
        {
            setTrait(lv2Trait, traitNum);
            return;
        }

        if (lv3Trait == Trait.NONE)
        {
            setTrait(lv3Trait, traitNum);
            return;
        }

        if (lv4Trait == Trait.NONE)
        {
            setTrait(lv4Trait, traitNum);
            return;
        }
    }

    private void setTrait(Trait* trait, int traitNum)
    {   
        switch (traitNum)
        {
            case 0:
                trait = Trait.FIRST;
                Debug.Log("no die");
                break;
            case 1:
                trait = Trait.SECOND;
                break;
            case 2:
                trait = Trait.THIRD;
                break;
        }
        trait = Trait.FIRST;
    }
}
