using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBall : MonoBehaviour
{
    public enum LootBallType {Fire, Ice, Electric};
    public LootBallType type;
    public int lootAmount;

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
                    player.GetComponent<Player>().addUltCharge();
                    break;
                case LootBallType.Ice:
                    player.iceAmount += lootAmount;
                    player.GetComponent<Player>().addUltCharge();
                    break;
                case LootBallType.Electric:
                    player.elecAmount += lootAmount;
                    player.GetComponent<Player>().addUltCharge();
                    break;
            }
            
        }
    }
}
