using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public bool activated = false;

    private void Update()
    {
        if (activated)
        {
            transform.Find("Torch").gameObject.SetActive(true);

        }
    }
}
