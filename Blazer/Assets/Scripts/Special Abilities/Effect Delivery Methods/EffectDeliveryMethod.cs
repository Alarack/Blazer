using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectDeliveryMethod {

    public Constants.EffectDeliveryMethod deliveryMethodType;


    public float range;

    [System.NonSerialized]
    protected SpecialAbility parentAbility;

    [System.NonSerialized]
    protected EffectAttack parentEffect;

    protected Vector2 effectOrigin;

    public virtual void Initialize(SpecialAbility parentAbility, EffectAttack parentEffect) {
        this.parentAbility = parentAbility;
        this.parentEffect = parentEffect;

    }

    public virtual void Deliver() {


    }

    protected virtual void CreateHitEffects(Vector2 location) {

    }

}
