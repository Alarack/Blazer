using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : Status {


    protected float effectDamage;

    

    public void InitializeDamageOverTime(float damge, Entity source) {
        effectDamage = damge;
        this.source = source;
    }


    protected override void Tick() {
        base.Tick();

        if (targetEntity != null) {
            CombatManager.AlterStat(source, target.GetComponent<Entity>(), Constants.BaseStatType.Health, effectDamage);
            //Debug.Log(effectDamage);
        }
        else {
            Debug.Log("Target null");
        }

    }

}
