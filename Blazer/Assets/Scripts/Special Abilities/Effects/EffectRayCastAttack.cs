using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectRayCastAttack : EffectAttack {

    public enum TargetingMethod {
        None = 0,
        StraightLeftRight = 1,
    }


    public TargetingMethod targetingMethod = TargetingMethod.StraightLeftRight;
    public LayerMask layerMask;



    private Vector2 shootDirection;


    public override void Apply() {
        base.Apply();

        if (burstAttack) {
            parentAbility.source.StartCoroutine(BurstFire(burstInterval, burstNumber));
        }
        else {
            Fire();
        }


        //TryShootRay();
    }

    protected override IEnumerator BurstFire(float delay, int number) {

        for(int i = 0; i < number; i++) {
            TryShootRay();
            yield return new WaitForSeconds(delay);
        }

    }

    protected override void Fire() {
        TryShootRay();
    }


    private void ConfigureRay() {

        switch (targetingMethod) {
            case TargetingMethod.StraightLeftRight:
                if (parentAbility.source.Facing == Constants.EntityFacing.Left) {
                    shootDirection = Vector2.left;
                    shotOrigin = parentAbility.source.leftShotOrigin.position;
                }
                else {
                    shootDirection = Vector2.right;
                    shotOrigin = parentAbility.source.rightShotOrigin.position;
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

            /*GameObject hitEffect = */VisualEffectManager.CreateVisualEffect(hitPrefab, hit.point, rot); //Instantiate(hitPrefab, hit.point, rot) as GameObject;

            hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-rayDir * 150f);

            Debug.Log(parentAbility.abilityName + " deals " + (baseDamage + parentAbility.source.stats.GetStatCurrentValue(Constants.EntityStat.BaseDamage) + " points of damage"));

        }

    }

}
