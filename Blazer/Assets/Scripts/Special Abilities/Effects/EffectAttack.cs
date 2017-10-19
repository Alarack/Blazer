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
    public float range;

    public bool penetrate;
    public int numPenetrations;

    protected Vector2 shotOrigin;



    public override void Initialize(SpecialAbility parentAbility) {
        base.Initialize(parentAbility);
        rayCastDelivery.Initialize(parentAbility, this);

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
        base.Apply(target);


        Debug.Log(target.name + " was hit");

        //target.GetComponent<Rigidbody2D>().AddForce(-rayDir * 150f);

        float damage;
        if (scaleFromBaseDamage)
            damage = effectDamage + (parentAbility.source.stats.GetStatCurrentValue(Constants.EntityStat.BaseDamage) * percentOfBaseDamage);
        else
            damage = effectDamage;


        Debug.Log(parentAbility.abilityName + " deals " + damage + " points of damage to " + target.gameObject.name);


    }


}
