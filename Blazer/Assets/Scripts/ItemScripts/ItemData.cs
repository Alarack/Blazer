using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemData : ScriptableObject  {

    public enum ItemType {
        None = 0,
        Passive = 1,
        Use = 2,
    }

    public enum ItemRarity {
        None = 0,
        Common = 1,
        Rare = 2,
        Legendary = 3
    }


    public ItemIDs.ItemID itemID;
    public ItemType itemType;
    public ItemRarity itemRarity;
    public Constants.ItemPool itemPoolCategory;
    public string itemName;
    public Sprite icon;

    public List<SpecialAbilityData> abilityData = new List<SpecialAbilityData>();




}
