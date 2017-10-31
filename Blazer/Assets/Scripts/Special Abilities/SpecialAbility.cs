using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpecialAbility {

    public string abilityName;
    [System.NonSerialized]
    public Sprite abilityIcon;
    public Entity source;
    public float useDuration;
    public bool overrideOtherAbilities;

    public List<Entity> targets = new List<Entity>();

    [System.NonSerialized]
    protected List<SpecialAbility> sequencedAbilities = new List<SpecialAbility>();

    public SpecialAbility ParentAbility { get; set; }
    public float sequenceWindow = 0.5f;

    private Timer sequenceTimer;
    private int sequenceIndex = 0;

    public bool InUse { get; protected set; }

    protected Timer useTimer;
    public int UseCount { get; protected set; }
    public SpecialAbilityRecovery Recovery { get { return recoveryMethod; } }

    protected SpecialAbilityRecovery recoveryMethod;

    public List<Effect> effects = new List<Effect>();

    private SpecialAbilityData abilitydata;

    public virtual void Initialize(Entity source, SpecialAbilityData abilitydata, List<SpecialAbilityData> sequencedAbilities = null) {
        this.source = source;
        this.abilitydata = abilitydata;

        SetUpAbility();

        if (sequencedAbilities != null && sequencedAbilities.Count > 0) {
            for (int i = 0; i < sequencedAbilities.Count; i++) {
                SpecialAbility sAbility = new SpecialAbility();
                this.sequencedAbilities.Add(sAbility);
                sAbility.ParentAbility = this;
                sAbility.Initialize(source, sequencedAbilities[i]);
            }

            sequenceTimer = new Timer("SequenceTimer", sequenceWindow, false, ResetSequenceIndex);
        }
    }

    private void SetUpAbility() {
        abilityName = abilitydata.abilityName;
        effects = abilitydata.GetAllEffects();
        recoveryMethod = abilitydata.GetRecoveryMechanic();
        sequenceWindow = abilitydata.sequenceWindow;
        useDuration = abilitydata.useDuration;
        useTimer = new Timer("Use Timer", useDuration, true, PopAbilityUseTimer);
        overrideOtherAbilities = abilitydata.overrideOtherAbilities;
        abilityIcon = abilitydata.abilityIcon;

        if (recoveryMethod != null) {
            recoveryMethod.Initialize(this);
            //Debug.Log(recoveryMethod.recoveryType.ToString());
        }

        for (int i = 0; i < effects.Count; i++) {
            effects[i].Initialize(this);
        }

        SetupRiders();
    }


    public void IncrementSequenceIndex() {
        sequenceIndex++;
        if (sequenceIndex >= sequencedAbilities.Count) {
            sequenceIndex = 0;
        }
    }

    private void ResetSequenceIndex() {
        //Debug.Log("resetting sequence index");
        sequenceIndex = 0;
    }

    public Effect GetEffectByName(string effectName) {
        if (string.IsNullOrEmpty(effectName))
            return null;

        for (int i = 0; i < effects.Count; i++) {
            if (effects[i].effectName == effectName) {
                return effects[i];
            }
        }

        return null;
    }

    public virtual bool Activate() {
        if (!recoveryMethod.Ready) {
            //Debug.Log(abilityName + " is On Cooldown: " + ((RecoveryCooldown)recoveryMethod).cooldown);
            return false;
        }
        //Debug.Log(abilityName + " has been activated");
        if (sequencedAbilities.Count < 1) {
            for (int i = 0; i < effects.Count; i++) {
                targets.Clear();
                effects[i].Activate();
            }

            FinishActivation();
            //Debug.Log(abilityName + " has been activated");
            return true;
        }

        targets.Clear();
        if (sequencedAbilities[sequenceIndex].Activate()) {
            sequenceTimer.ResetTimer();
        }

        FinishActivation();
        return true;
    }

    public void AddTarget(Entity target) {
        if (!targets.Contains(target)) {
            targets.Add(target);
        }

    }

    private void FinishActivation() {
        if (recoveryMethod != null) {
            recoveryMethod.Trigger();
        }

        InUse = true;

    }

    public virtual void ManagedUpdate() {

        if (recoveryMethod != null)
            recoveryMethod.ManagedUpdate();

        for (int i = 0; i < effects.Count; i++) {
            effects[i].ManagedUpdate();
        }

        AbilityInUse();

        for (int i = 0; i < sequencedAbilities.Count; i++) {
            sequencedAbilities[i].ManagedUpdate();
        }

        if(sequenceTimer != null) {
            sequenceTimer.UpdateClock();
        }

    }

    public virtual void ResetUseCount() {
        UseCount = 0;
    }

    protected void AbilityInUse() {
        if (InUse) {
            useTimer.UpdateClock();
        }
    }

    protected virtual void PopAbilityUseTimer() {
        InUse = false;

        if (ParentAbility != null) {
            ParentAbility.IncrementSequenceIndex();
        }

    }

    protected void SetupRiders() {
        for (int i = 0; i < effects.Count; i++) {
            effects[i].SetUpRiders();
        }
    }

}
