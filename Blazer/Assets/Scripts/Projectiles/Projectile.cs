using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public enum ProjectileType {
        None = 0,
        Bullet = 1,
        Rocket = 2,
    }


    [Header("Basic Stats")]
    public ProjectileType projectileType;
    public float life;
    public float damage;

    public StatCollectionData statTemplate;
    public StatCollection stats;


    protected bool penetrating;
    protected int numPen;
    protected int curPen;

    protected Effect parentEffect;
    protected ProjectileMovement movement;
    protected LayerMask layerMask;

    protected virtual void Awake() {
        movement = GetComponent<ProjectileMovement>();
    }


    public void Initialize(Effect parentEffect, LayerMask mask, float life = 0f, float damage = 0f) {
        this.parentEffect = parentEffect;
        layerMask = mask;
        stats = new StatCollection();
        stats.Initialize(statTemplate);
        movement.Initialize();

        if(this.parentEffect is EffectAttack) {
            EffectAttack attackEffect = this.parentEffect as EffectAttack;
            penetrating = attackEffect.penetrate;
            numPen = attackEffect.numPenetrations;
        }

        //Debug.Log(stats.GetStatCurrentValue(Constants.BaseStatType.Lifetime) + " is my lifetime from stats");

        this.life = life + stats.GetStatCurrentValue(Constants.BaseStatType.Lifetime);
        this.damage = damage + stats.GetStatCurrentValue(Constants.BaseStatType.BaseDamage);

        //Debug.Log(this.life.ToString() + " is my lifetime");

        if(this.life > 0f) {
            //Debug.Log("Cleaning up in " + life.ToString());
            Invoke("CleanUp", this.life);
        }

    }

    public virtual void CleanUp() {
        //Debug.Log("Cleaning");
        Destroy(gameObject);

    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {

        if ((layerMask & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) {

            parentEffect.Apply(other.gameObject);

            if (!penetrating) {
                CleanUp();
            }
            else {
                HandlePenetration();
            }

        }

    }

    protected void HandlePenetration() {
        if (curPen >= numPen) {
            CleanUp();
        }
        else {
            curPen++;
        }
    }

}
