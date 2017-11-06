using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour {

    [Header("Icon")]
    public SpriteRenderer image;


    [Header("Layer Mask")]
    public LayerMask layerMask;


    private ItemData item;

    public virtual void Initialize(ItemData item) {
        this.item = item;
        image.sprite = item.icon;
    }




    protected virtual void OnTriggerEnter2D(Collider2D other) {

        if ((layerMask & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) {
            Entity otherEntity = other.GetComponent<Entity>();

            if (otherEntity == null)
                return;

            switch (item.itemType) {
                case ItemData.ItemType.Passive:
                    otherEntity.inventory.AddItemEntry(item);
                    break;
            }


            Destroy(gameObject);

        }

    }

}
