using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    Enemy enemy;
    public GameObject OnFireObj;
    public GameObject IcedObj;
    bool isFireEffect;
    bool isIceEffect;
    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.GetComponent<Enemy>();
        isFireEffect = false;
        isIceEffect = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkFireEffect();
        checkIceEffect();
    }

    void checkFireEffect()
    {
        if (enemy.onFireCount > 0 && isFireEffect == false)
        {
            GameObject temp = Instantiate(OnFireObj, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0), transform);
            Destroy(temp, enemy.onFireCount);
            isFireEffect = true;
        }
        if (enemy.onFireCount <= 0 && isFireEffect == true)
            isFireEffect = false;
    }

    void checkIceEffect()
    {
        if (enemy.iceCount > 0 && isIceEffect == false)
        {
            GameObject temp = Instantiate(IcedObj, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0), transform);
            Destroy(temp, enemy.iceCount);
            isIceEffect = true;
        }
        if (enemy.iceCount <= 0 && isIceEffect == true)
            isIceEffect = false;
    }
}
