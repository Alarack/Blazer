using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect  {

    public Constants.SpecialAbilityEffectType effectType;
    public Constants.EffectDeliveryMethod deliveryMethod;
    public string animationTrigger;

    public Entity Source { get { return parentAbility.source; }  }

    [System.NonSerialized]
    protected SpecialAbility parentAbility;

    public EffectDeliveryRaycast rayCastDelivery = new EffectDeliveryRaycast();
    public EffectDeliveryProjectile projectileDelivery = new EffectDeliveryProjectile();
    public EffectDeliveryMelee meleeDelivery = new EffectDeliveryMelee();


    public virtual void Initialize(SpecialAbility parentAbility) {
        this.parentAbility = parentAbility;
    }

    public virtual void Activate() {
        Debug.Log("An effect of type " + effectType.ToString() + " on the ability " + parentAbility.abilityName + " is being activated");
        //Debug.Log(deliveryMethod + " is my delivery method");
    }

    public virtual void BeginDelivery() {

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
        }
    }

    public virtual void Apply(GameObject target) {

    }

    public virtual void Remove() {

    }

}