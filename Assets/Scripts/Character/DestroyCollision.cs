using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollision : MonoBehaviour
{
    FaintEffect faintEffect;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            faintEffect = coll.transform.GetComponent<FaintEffect>();
            faintEffect.startGroundBlood();
            Destroy(coll.gameObject);
        }

    }
}
