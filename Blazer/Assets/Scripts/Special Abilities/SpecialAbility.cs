using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpecialAbility {

    public string abilityName;
    public Entity source;
    public float useDuration;
    public bool overrideOtherAbilities;

    public bool InUse { get; protected set; }

    protected SpecialAbilityRecovery recoveryMethod;
    protected float useTimer;

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
        useDuration = abilitydata.useDuration;
        overrideOtherAbilities = abilitydata.overrideOtherAbilities;

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

        InUse = true;

    }

    public virtual void ManagedUpdate() {

        if(recoveryMethod != null)
            recoveryMethod.ManagedUpdate();

        AbilityInUse();
    }


    protected void AbilityInUse() {
        if (InUse) {
            useTimer += Time.deltaTime;
            if(useTimer >= useDuration) {
                InUse = false;
                useTimer = 0f;
            }
        }
    }

}
