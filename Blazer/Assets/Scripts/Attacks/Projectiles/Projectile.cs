﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : AttackMedium {

    public enum ProjectileType {
        None = 0,
        Bullet = 1,
        Rocket = 2,
    }


    [Header("Basic Stats")]
    public ProjectileType projectileType;
 
    protected ProjectileMovement movement;


    protected virtual void Awake() {
        movement = GetComponent<ProjectileMovement>();
    }


    public override void Initialize(Effect parentEffect, LayerMask mask, float life = 0f, float damage = 0f) {
        base.Initialize(parentEffect, mask, life, damage);

        movement.Initialize();
    }

    public override void CleanUp() {
        //Debug.Log("Cleaning");
        //CancelInvoke("CleanUp");
        base.CleanUp();

        CreateImpactEffect();

        Destroy(gameObject);

    }


    protected virtual void CreateImpactEffect() {

        if (string.IsNullOrEmpty(impactEffect))
            return;

        GameObject loadedEffect = Resources.Load(impactEffect) as GameObject;

        if(loadedEffect == null) {
            Debug.LogError("[Projectile] Impact Effect was null");
            return;
        }

        Vector2 dir = parentEffect.Source.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        //Quaternion impactRotation = Quaternion.LookRotation(dir, Vector3.forward);

        GameObject activeHitEffect = Instantiate(loadedEffect, transform.position, q) as GameObject;

        VisualEffectManager.SetParticleEffectLayer(activeHitEffect, gameObject);

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
        else {
            CleanUp();
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
