using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuickBar : MonoBehaviour {


    public List<PlayerAbilitySlot> quickbarSlots = new List<PlayerAbilitySlot>();






    public void SetQuickBarSlot(SpecialAbility ability, int slotIndex) {
        if(slotIndex > quickbarSlots.Count) {
            Debug.LogError("[PlayerQuickBar] index out of range. " + slotIndex + " is more than 4");
        }


        quickbarSlots[slotIndex].Initialize(this, ability);


    }



}
