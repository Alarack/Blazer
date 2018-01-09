using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Pools")]
public class ItemPools : ScriptableObject {


    public List<Rarity> rarity = new List<Rarity>();
    public List<ItemPoolCategory> pools = new List<ItemPoolCategory>();



    public ItemData.ItemRarity DetermineRarity() {
        float roll = Random.Range(0f, 1f);

        

        Dictionary<ItemData.ItemRarity, float> dropChances = GetDropChances();

        //Debug.Log(roll + " is the roll");

        //Debug.Log(dropChances[ItemData.ItemRarity.Legendary] + " is the legend drop chance");

        if(roll <= dropChances[ItemData.ItemRarity.Legendary]) 
            return ItemData.ItemRarity.Legendary;

        if (roll <= dropChances[ItemData.ItemRarity.Rare])
            return ItemData.ItemRarity.Rare;

        if (roll <= dropChances[ItemData.ItemRarity.Common])
            return ItemData.ItemRarity.Common;


        return ItemData.ItemRarity.None;
    }



    private Dictionary<ItemData.ItemRarity, float> GetDropChances() {
        Dictionary<ItemData.ItemRarity, float> result = new Dictionary<ItemData.ItemRarity, float>();

        for(int i = 0; i <rarity.Count; i++) {
            result.Add(rarity[i].rarity, rarity[i].threshold);
        }

        return result;
    }



    public ItemData GetItem(Constants.ItemPool pool) {
        ItemData result = null;

        for (int i = 0; i < pools.Count; i++) {
            if (pools[i].itemPool == pool) {
                ItemPoolCategory target = pools[i];
                int randomIndex = Random.Range(0, target.items.Count);

                return target.items[randomIndex];
            }
        }

        return result;
    }


    [System.Serializable]
    public class ItemPoolCategory {
        public Constants.ItemPool itemPool;
        public List<ItemData> items = new List<ItemData>();

    }

    [System.Serializable]
    public class Rarity {
        public ItemData.ItemRarity rarity;
        public float threshold;
    }

}