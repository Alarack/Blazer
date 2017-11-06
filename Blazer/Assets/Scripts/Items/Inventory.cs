using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class Inventory : MonoBehaviour {

    public List<ItemData> items = new List<ItemData>();

    private Entity owner;


    public void Initialize(Entity owner) {
        this.owner = owner;

    }


    public void AddItemEntry(ItemData item) {
        items.Add(item);

        owner.AbilityManager.PopulateSpecialAblities(item.abilityData);

        //item.Initialize(this, owner);
    }



    //[System.Serializable]
    //public class InventoryEntry {
    //    public Item item;
    //    public int count;
    //    public bool active;
    //    //public bool dropable;
    //    //public float dropChance;

    //    public InventoryEntry(Item item) {
    //        this.item = item;
    //        count = 1;
    //        active = true;
    //    }

    //    public void AddItem(Item item) {
    //        if (this.item.itemID == item.itemID) {
    //            count++;
    //        }
    //    }
    //}

}
