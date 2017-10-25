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

    public Entity Source { get { return parentAbility.source; } }

    [System.NonSerialized]
    protected SpecialAbility parentAbility;

    [System.NonSerialized]
    protected List<Effect> riders = new List<Effect>();

    //protected List<GameObject> currentTargets = new List<GameObject>();

    public EffectDeliveryRaycast rayCastDelivery = new EffectDeliveryRaycast();
    public EffectDeliveryProjectile projectileDelivery = new EffectDeliveryProjectile();
    public EffectDeliveryMelee meleeDelivery = new EffectDeliveryMelee();

    public virtual void Initialize(SpecialAbility parentAbility) {
        this.parentAbility = parentAbility;
    }

    public virtual void ManagedUpdate() {

    }


    public virtual void Activate() {
        Debug.Log("An effect of type " + effectType.ToString() + " on the ability " + parentAbility.abilityName + " is being activated");
        //Debug.Log(deliveryMethod + " is my delivery method");
    }

    public virtual void BeginDelivery() {

        if(!string.IsNullOrEmpty(animationTrigger))
            parentAbility.source.MyAnimator.SetTrigger(animationTrigger);

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
        }
    }

    public virtual void Apply(GameObject target) {
        //if(!currentTargets.Contains(target))
        //    currentTargets.Add(target);

        ApplyRiderEffects(target);

        Debug.Log(effectName + " is being applied on " + target.gameObject.name);


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

    protected virtual void ApplyRiderEffects(GameObject target) {
        for (int i = 0; i < riders.Count; i++) {
            riders[i].Apply(target);
        }
    }

}