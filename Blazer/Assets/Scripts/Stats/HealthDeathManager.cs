using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class HealthDeathManager : MonoBehaviour {


    public GameObject deathEffect;

    protected Entity owner;





    public virtual void Initialize(Entity owner) {
        this.owner = owner;

        RegisterListeners();
    }

    protected virtual void RegisterListeners() {
        Grid.EventManager.RegisterListener(Constants.GameEvent.StatChanged, OnStatChanged);
    }

    protected void OnStatChanged(EventData data) {
        Constants.BaseStatType stat = (Constants.BaseStatType)data.GetInt("Stat");
        Entity target = data.GetMonoBehaviour("Target") as Entity;

        if (target != owner)
            return;

        if(stat == Constants.BaseStatType.Health) {
            if(owner.stats.GetStatCurrentValue(Constants.BaseStatType.Health) <= 0f) {
                Die();
            }
        }

    }



    protected virtual void Die() {
        ShowDeathEffect();
        GameManager.UnregisterEntity(owner);
        owner.UnregisterListeners();


        Destroy(owner.gameObject);
    }

    protected virtual void ShowDeathEffect() {
        if (deathEffect == null)
            return;

        GameObject deathVisual = Instantiate(deathEffect, owner.transform.position, Quaternion.identity) as GameObject;


    }

}
