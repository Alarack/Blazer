using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectDeliveryProjectile : EffectDeliveryMethod {




    public Projectile.ProjectileType projectileType;
    public string prefabName;
    public float error;


    public bool kickBack;
    public float kickStrength;

    private Transform shotPos;

    public override void Deliver() {
        base.Deliver();

        CreateProjectile();
    }



    private void ConfigureProjectile() {

        switch (targetingMethod) {
            case TargetingMethod.StraightLeftRight:
                if (parentAbility.source.Facing == Constants.EntityFacing.Left) {
                    shootDirection = Vector2.left;
                    effectOrigin = parentAbility.source.leftShotOrigin.position;
                    shotPos = parentAbility.source.leftShotOrigin;
                }
                else {
                    shootDirection = Vector2.right;
                    effectOrigin = parentAbility.source.rightShotOrigin.position;
                    shotPos = parentAbility.source.rightShotOrigin;
                }
                break;
        }


    }




    private void CreateProjectile() {

        ConfigureProjectile();

        GameObject loadedPrefab = Resources.Load("Projectiles/" + prefabName) as GameObject;

        if(loadedPrefab == null) {
            Debug.LogError("Prefab was null");
            return;
        }

        //Quaternion rot = Quaternion.FromToRotation(effectOrigin, shootDirection);//Quaternion.LookRotation(shootDirection, Vector3.forward);



        GameObject shot = VisualEffectManager.CreateVisualEffect(loadedPrefab, effectOrigin, shotPos.rotation);
        Projectile shotScript = shot.GetComponent<Projectile>();


        if (error != 0f) {
            float e = Random.Range(-error, error);
            shot.transform.rotation = shotPos.rotation * Quaternion.Euler(shotPos.rotation.x, shotPos.rotation.y, e);
        }

        shotScript.Initialize(parentEffect, layerMask, 0f, parentEffect.effectDamage);

        if (kickBack) {
            parentAbility.source.GetComponent<Rigidbody2D>().AddForce(-shotPos.up * kickStrength);
        }


    }

}
