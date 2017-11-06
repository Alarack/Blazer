using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemPools))]
public class ItemPoolsEditor : Editor {

    private ItemPools _itemPools;

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();

        _itemPools = (ItemPools)target;

        _itemPools.rarity = EditorHelper.DrawExtendedList("Rarity", _itemPools.rarity, "Category", DrawRarity);

        EditorGUILayout.Separator();

        _itemPools.pools = EditorHelper.DrawExtendedList("Item Pools", _itemPools.pools, "Pool", DrawItemPools);

        if(GUI.changed)
            EditorUtility.SetDirty(target);

    }




    private ItemPools.ItemPoolCategory DrawItemPools(ItemPools.ItemPoolCategory entry) {

        entry.itemPool = EditorHelper.EnumPopup("Pool Category", entry.itemPool);
        entry.items = EditorHelper.DrawList("Items", entry.items, true, null, true, DrawItemData);


        return entry;
    }

    private ItemPools.Rarity DrawRarity(ItemPools.Rarity entry) {

        entry.rarity = EditorHelper.EnumPopup("Categoy", entry.rarity);
        entry.threshold = EditorHelper.PercentFloatField("Threshold", entry.threshold);

        return entry;
    }





    private ItemData DrawItemData(List<ItemData> itemData, int index) {
        ItemData result = EditorHelper.ObjectField<ItemData>("Item", itemData[index]);
        return result;
    }

}
