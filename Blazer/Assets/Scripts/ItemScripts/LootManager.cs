using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {

    public float dropChance;
    public Constants.ItemPool pool;




    public void SpawnLoot() {
        if (!CheckDrop())
            return;

        //Debug.Log(GameManager.GetItemPools().DetermineRarity() + " is the rarity");

        ItemData item = GameManager.GetItemPools().GetItem(pool);

        for (int i = 0; i < 10; i++) {
            if(item == null) {
                item = GameManager.GetItemPools().GetItem(pool);
            }
            else {
                break;
            }
        }

        if (item == null)
            return;

        GameObject drop = Resources.Load("Item Prefabs/Item Pickup") as GameObject;
        GameObject activeDrop = Instantiate(drop, new Vector3(transform.position.x, transform.position.y, transform.position.z -2), Quaternion.identity) as GameObject;

        ItemPickup pickup = activeDrop.GetComponent<ItemPickup>();

        pickup.Initialize(item);
    }




    private bool CheckDrop() {
        int roll = Random.Range(1, 101);

        if (roll <= dropChance) {
            return true;
        }
        else {
            return false;
        }
    }

}
