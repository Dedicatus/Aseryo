using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBall : MonoBehaviour
{
    public enum LootBallType {Fire, Ice, Wind};
    public LootBallType type;
    public float lootAmount;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Player player = coll.GetComponent<Player>();
            switch (type)
            {
                case LootBallType.Fire:
                    player.fireAmount += lootAmount;
                    break;
                case LootBallType.Ice:
                    player.iceAmount += lootAmount;
                    break;
                case LootBallType.Wind:
                    player.windAmount += lootAmount;
                    break;
            }
            player.GetComponent<Player>().addUltCharge(lootAmount);
            player.GetComponent<Player>().addExtraCharge();
        }
    }
}
