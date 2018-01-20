using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect {

    public string effectName;
    public string riderTarget;
    public Constants.SpecialAbilityEffectType effectType;
    public Constants.EffectDeliveryMethod deliveryMethod;
    public string animationTrigger;

    public bool applyToSpecificTarget;
    public int targetIndex;

    //public bool requireMultipleHits;
    //public int requiredHits;
    //protected int hitCounter;

    public Entity Source { get { return parentAbility.source; } }

    [System.NonSerialized]
    protected SpecialAbility parentAbility;

    [System.NonSerialized]
    protected List<Effect> riders = new List<Effect>();

    public List<EffectEventOptions> eventOptions = new List<EffectEventOptions>();
    protected Dictionary<Constants.EffectEventOption, bool> eventDict = new Dictionary<Constants.EffectEventOption, bool>();
    //protected List<GameObject> currentTargets = new List<GameObject>();

    public EffectDeliveryRaycast rayCastDelivery = new EffectDeliveryRaycast();
    public EffectDeliveryProjectile projectileDelivery = new EffectDeliveryProjectile();
    public EffectDeliveryMelee meleeDelivery = new EffectDeliveryMelee();

    public virtual void Initialize(SpecialAbility parentAbility) {
        this.parentAbility = parentAbility;

        //switch (deliveryMethod) {
        //    case Constants.EffectDeliveryMethod.Melee:
        //        meleeDelivery = new EffectDeliveryMelee();
        //        break;

        //    case Constants.EffectDeliveryMethod.Projectile:
        //        projectileDelivery = new EffectDeliveryProjectile();
        //        break;

        //    case Constants.EffectDeliveryMethod.Raycast:
        //        rayCastDelivery = new EffectDeliveryRaycast();
        //        break;
        //}

        //Debug.Log(effectName + " has been initialized");

        for (int i = 0; i < eventOptions.Count; i++) {
            eventDict.Add(eventOptions[i].option, eventOptions[i].sendEvent);
        }

    }

    public virtual void ManagedUpdate() {

    }

    #region EVENTS
    private void SendEffectAppliedEvent(Entity target) {
        EventData data = new EventData();
        data.AddMonoBehaviour("Cause", Source);
        data.AddMonoBehaviour("Target", target);
        data.AddInt("EffectType", (int)effectType);

        Grid.EventManager.SendEvent(Constants.GameEvent.EffectApplied, data);

    }


    #endregion


    public virtual void Activate() {
        //Debug.Log("An effect of type " + effectType.ToString() + " on the ability " + parentAbility.abilityName + " is being activated");
        //Debug.Log(deliveryMethod + " is my delivery method");
    }

    public virtual void BeginDelivery() {

        if (!string.IsNullOrEmpty(animationTrigger)) {
            parentAbility.source.MyAnimator.SetTrigger(animationTrigger);
        }
        //else {
        //    Debug.LogError("No animation trigger on " + effectName);
        //}


        switch (deliveryMethod) {
            case Constants.EffectDeliveryMethod.Raycast:
                rayCastDelivery.Deliver();
                break;

            case Constants.EffectDeliveryMethod.Projectile:
                projectileDelivery.Deliver();
                break;

            case Constants.EffectDeliveryMethod.Melee:
                meleeDelivery.Deliver();
                break;

            case Constants.EffectDeliveryMethod.SelfTargeting:
                Apply(Source.gameObject);
                break;

            case Constants.EffectDeliveryMethod.None:

                int count = parentAbility.targets.Count;

                for (int i = 0; i < count; i++) {
                    if(parentAbility.targets[i] != null)
                        Apply(parentAbility.targets[i].gameObject);
                }

                //Debug.Log(effectName + " has no delivery");
                break;
        }
    }

    public virtual void Apply(GameObject target) {
        Entity targetEntity = target.GetComponent<Entity>();
        if (target != null) {

            if (parentAbility.ParentAbility != null) {
                parentAbility.ParentAbility.AddTarget(targetEntity);
            }
            else {
                parentAbility.AddTarget(targetEntity);
            }
        }

        ApplyRiderEffects(target);

        if (eventDict.ContainsKey(Constants.EffectEventOption.Applied) && eventDict[Constants.EffectEventOption.Applied]) {
            SendEffectAppliedEvent(targetEntity);
        }
        //Debug.Log(effectName + " is being applied on " + target.gameObject.name);
    }

    public virtual void Remove() {

    }

    public virtual void AddRider(Effect effect) {

        if (!riders.Contains(effect)) {
            riders.Add(effect);
        }
    }

    public virtual void SetUpRiders() {
        if (deliveryMethod != Constants.EffectDeliveryMethod.Rider)
            return;

        Effect host = parentAbility.GetEffectByName(riderTarget);

        if (host != null) {
            host.AddRider(this);
        }

    }

    protected virtual List<Effect> CloneRiders() {
        List<Effect> clones = new List<Effect>();

        for (int i = 0; i < riders.Count; i++) {
            if (riders[i] is EffectAttack) {
                EffectAttack clone = new EffectAttack((EffectAttack)riders[i]);
                clones.Add(clone);
            }


            //clones.Add(riders[i]);
        }

        return clones;
    }

    protected virtual void ApplyRiderEffects(GameObject target) {
        for (int i = 0; i < riders.Count; i++) {
            riders[i].Apply(target);
        }
    }


    protected virtual bool CheckForSpecificTarget(GameObject target) {
        Entity targetEntity = target.GetComponent<Entity>();

        if (applyToSpecificTarget) {
            if (parentAbility.targets.Count < targetIndex - 1) {
                //Debug.Log("Target out of range");
                return false;
            }

            if (targetEntity != parentAbility.targets[targetIndex - 1]) {
                //Debug.Log("target no at right index ");
                return false;
            }

            //Debug.Log(targetEntity.gameObject.name + " is the " + targetIndex + "th target");
        }

        return true;
    }

    [System.Serializable]
    public class EffectEventOptions {
        public Constants.EffectEventOption option;
        public bool sendEvent = false;

        public EffectEventOptions() {

        }
    }

}