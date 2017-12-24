using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectAttack : Effect {

    public int effectDamage;

    public bool scaleFromBaseDamage;
    public float percentOfBaseDamage = 1f;

    public bool burstAttack;
    public int burstNumber;
    public float burstInterval;

    public string impactEffectName;
    public string fireEffectName;
    //public float range;

    public bool penetrate;
    public int numPenetrations;

    protected Vector2 shotOrigin;


    public override void Initialize(SpecialAbility parentAbility) {
        base.Initialize(parentAbility);

        switch (deliveryMethod) {
            case Constants.EffectDeliveryMethod.Raycast:
                rayCastDelivery.Initialize(parentAbility, this);
                break;

            case Constants.EffectDeliveryMethod.Projectile:
                projectileDelivery.Initialize(parentAbility, this);
                break;

            case Constants.EffectDeliveryMethod.Melee:
                meleeDelivery.Initialize(parentAbility, this);
                break;
        }
    }

    public override void Activate() {
        base.Activate();

        Debug.Log("Activating an attack");

        if (burstAttack) {
            parentAbility.source.StartCoroutine(BurstFire(burstInterval, burstNumber));
        }
        else {
            Fire();
        }
    }

    protected virtual IEnumerator BurstFire(float delay, int number) {

        for(int i = 0; i < number; i++) {
            BeginDelivery();
            yield return new WaitForSeconds(delay);
        }
    }

    protected virtual void Fire() {
        BeginDelivery();
    }

    public override void Apply(GameObject target) {

        float damage;
        if (scaleFromBaseDamage)
            damage = effectDamage + (parentAbility.source.stats.GetStatModifiedValue(Constants.BaseStatType.BaseDamage) * percentOfBaseDamage);
        else
            damage = effectDamage;

        //Debug.Log(damage);

        Entity targetEntity = target.GetComponent<Entity>();

        if(targetEntity != null) {
            CombatManager.ApplyUntrackedStatMod(Source, targetEntity, Constants.BaseStatType.Health, damage);
        }

        base.Apply(target);
    }

}