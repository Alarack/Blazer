using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpecialAbility {

    public string abilityName;
    public Entity source;
    //public List<SpecialAbilityRecovery> recoveryMethods = new List<SpecialAbilityRecovery>();
    protected SpecialAbilityRecovery recoveryMethod;

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

        recoveryMethod = abilitydata.GetRecoveryMechanic();

        if(recoveryMethod != null) {
            recoveryMethod.Initialize(this);
            //Debug.Log(recoveryMethod.recoveryType.ToString());
        }



        for (int i = 0; i < effects.Count; i++) {
            effects[i].Initialize(this);
        }
    }



    public virtual void Activate() {
        if (!recoveryMethod.Ready)
            return;

        //Debug.Log(abilityName + " has been activated");

        for (int i = 0; i < effects.Count; i++) {
            effects[i].Activate();
        }

        if(recoveryMethod != null) {
            recoveryMethod.Trigger();
        }

    }

    public virtual void ManagedUpdate() {

        if(recoveryMethod != null)
            recoveryMethod.ManagedUpdate();

        //for(int i = 0; i < recoveryMethods.Count; i++) {
        //    recoveryMethods[i].ManagedUpdate();
        //}

    }

}
