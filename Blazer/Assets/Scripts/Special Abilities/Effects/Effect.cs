using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect  {

    public Constants.SpecialAbilityEffectType effectType;

    [System.NonSerialized]
    protected SpecialAbility parentAbility;



    public virtual void Initialize(SpecialAbility parentAbility) {
        this.parentAbility = parentAbility;
    }



    public virtual void Apply() {

        Debug.Log("An effect of type " + effectType.ToString() + " on the ability " + parentAbility.abilityName + " is being applied");
    }

    public virtual void Remove() {

    }


}
