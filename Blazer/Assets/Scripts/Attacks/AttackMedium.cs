using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackMedium : MonoBehaviour {


    public string impactEffect;

    public StatCollectionData statTemplate;
    public StatCollection stats;


    protected bool penetrating;
    protected int numPen;
    protected int curPen;

    protected float life;
    protected float damage;

    protected Effect parentEffect;
    protected LayerMask layerMask;



    public virtual void Initialize(Effect parentEffect, LayerMask mask, float life = 0f, float damage = 0f) {
        this.parentEffect = parentEffect;
        layerMask = mask;
        stats = new StatCollection();
        stats.Initialize(statTemplate);


        if (this.parentEffect is EffectAttack) {
            EffectAttack attackEffect = this.parentEffect as EffectAttack;
            penetrating = attackEffect.penetrate;
            numPen = attackEffect.numPenetrations;
            impactEffect = attackEffect.impactEffectName;
        }


        this.life = life + stats.GetStatCurrentValue(Constants.BaseStatType.Lifetime);
        this.damage = damage + stats.GetStatCurrentValue(Constants.BaseStatType.BaseDamage);

        if (this.life > 0f) {
            Invoke("CleanUp", this.life);
        }

    }


    public virtual void CleanUp() {


    }

}
