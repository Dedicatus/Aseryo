using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(coll.gameObject);
        }

    }
}
