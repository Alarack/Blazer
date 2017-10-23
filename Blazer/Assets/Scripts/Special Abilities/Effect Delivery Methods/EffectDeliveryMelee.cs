using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectDeliveryMelee : EffectDeliveryMethod {

    public string prefabName;




    public override void Deliver() {
        base.Deliver();

        CreateMeleeAttack();
    }



    private void ConfigureMeleeAttack() {
        switch (targetingMethod) {
            case TargetingMethod.StraightLeftRight:
                if (parentAbility.source.Facing == Constants.EntityFacing.Left) {
                    shootDirection = Vector2.left;
                    effectOrigin = parentAbility.source.leftShotOrigin.position;
                    //shotPos = parentAbility.source.leftShotOrigin;
                }
                else {
                    shootDirection = Vector2.right;
                    effectOrigin = parentAbility.source.rightShotOrigin.position;
                    //shotPos = parentAbility.source.rightShotOrigin;
                }
                break;
        }
    }


    private void CreateMeleeAttack() {
        ConfigureMeleeAttack();

        GameObject loadedPrefab = Resources.Load("Melee/" + prefabName) as GameObject;

        if (loadedPrefab == null) {
            Debug.LogError("Prefab was null");
            return;
        }

        GameObject hit = VisualEffectManager.CreateVisualEffect(loadedPrefab, effectOrigin, Quaternion.identity);
        MeleeHit hitScript = hit.GetComponent<MeleeHit>();

        hit.transform.SetParent(parentAbility.source.transform, true);

        hitScript.Initialize(parentEffect, layerMask, 0f, parentEffect.effectDamage);

    }



}
