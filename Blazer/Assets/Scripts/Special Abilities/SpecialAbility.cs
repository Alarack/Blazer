using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpecialAbility {

    public string abilityName;
    public Entity source;
    public SpecialAbilityRecovery recoveryMethod;

    public List<Effect> effects = new List<Effect>();

    private SpecialAbilityData abilitydata;

    public virtual void Initialize(Entity source, SpecialAbilityData abilitydata) {
        this.source = source;
        this.abilitydata = abilitydata;

        SetUpAbility();
    }

    private void SetUpAbility() {

        abilityName = abilitydata.abilityName;

        effects = abilitydata.GetAllEffects();

        for (int i = 0; i < effects.Count; i++) {
            effects[i].Initialize(source, this);
        }
        

    }



    public virtual void Activate() {
        //if (!recoveryMethod.Ready)
        //    return;

        //Debug.Log(abilityName + " has been activated");

        for (int i = 0; i < effects.Count; i++) {
            effects[i].Apply();
        }

    }

}
