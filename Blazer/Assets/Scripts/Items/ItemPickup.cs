using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour {

    [Header("Icon")]
    public SpriteRenderer image;


    [Header("Layer Mask")]
    public LayerMask layerMask;
    private Timer PickupDelayTimer;
    private bool canBePickedUp = false;
    public float pickupDelay;

    private ItemData item;

    public virtual void Initialize(ItemData item) {
        this.item = item;
        image.sprite = item.icon;
        PickupDelayTimer = new Timer("Spawn Timer", pickupDelay, false, PickupDelayEnd);
    }

    public void Update()
    {
        if (PickupDelayTimer != null)
            PickupDelayTimer.UpdateClock();
    }


    protected virtual void OnTriggerStay2D(Collider2D other) {

        if (canBePickedUp)
        {
            if ((layerMask & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
            {
                Entity otherEntity = other.GetComponent<Entity>();

                if (otherEntity == null)
                    return;

                switch (item.itemType)
                {
                    case ItemData.ItemType.Passive:
                        otherEntity.inventory.AddItemEntry(item);
                        Debug.Log("Collected " + item.itemName);
                        break;
                }


                Destroy(gameObject);

            }
        }
    }

    private void PickupDelayEnd()
    {
        canBePickedUp = true;
    }
}
