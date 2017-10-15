using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectRayCastAttack : Effect {

    public enum TargetingMethod {
        None = 0,
        StraightLeftRight = 1,
    }


    public TargetingMethod targetingMethod = TargetingMethod.StraightLeftRight;
    public LayerMask layerMask;
    public string impactEffectName;
    public string fireEffectName;


    private Vector2 shootDirection;
    private Vector2 shotOrigin;

    public override void Apply() {
        base.Apply();

        TryShootRay();
    }




    private void ConfigureRay() {

        switch (targetingMethod) {
            case TargetingMethod.StraightLeftRight:
                if (source.Facing == Constants.EntityFacing.Left) {
                    shootDirection = Vector2.left;
                    shotOrigin = source.leftShotOrigin.position;
                }
                else {
                    shootDirection = Vector2.right;
                    shotOrigin = source.rightShotOrigin.position;
                }
                break;
        }

    }


    private void TryShootRay() {


        ConfigureRay();
        


        RaycastHit2D hit = Physics2D.Raycast(shotOrigin, shootDirection, Mathf.Infinity, layerMask);


        if (hit.collider != null) {
            Debug.Log(hit.collider.gameObject.name + " was hit");

            Vector2 rayDir = Vector2.Reflect((hit.point - shotOrigin).normalized, hit.normal);

            float angle = Mathf.Atan2(-rayDir.x, rayDir.y) * Mathf.Rad2Deg;

            Quaternion rot = Quaternion.AngleAxis(angle, Vector2.right);

            GameObject hitPrefab = Resources.Load(impactEffectName) as GameObject;

            GameObject hitEffect = VisualEffectManager.CreateVisualEffect(hitPrefab, hit.point, rot); //Instantiate(hitPrefab, hit.point, rot) as GameObject;

            hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-rayDir * 150f);

        }

    }

}
