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

        if (item == null)
            return;

        GameObject drop = Resources.Load("Items/Item Pickup") as GameObject;
        GameObject activeDrop = Instantiate(drop, transform.position, Quaternion.identity) as GameObject;

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
