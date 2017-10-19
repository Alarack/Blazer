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


    //public override void Activate() {
    //    base.Activate();




    //    //TryShootRay();
    //}

    //public override void Apply(GameObject target) {
    //    base.Apply(target);


    //}


    //protected override IEnumerator BurstFire(float delay, int number) {

    //    for(int i = 0; i < number; i++) {
    //        //TryShootRay();
    //        DoAttack();
    //        yield return new WaitForSeconds(delay);
    //    }

    //}

    //protected override void Fire() {
    //    //TryShootRay();
    //    DoAttack();
    //}


    //protected void DoAttack() {

    //    if (penetrate) {
    //        TryShootMultiRay();
    //    }
    //    else {
    //        TryShootRay();
    //    }

    //}


    //private void ConfigureRay() {

    //    switch (targetingMethod) {
    //        case TargetingMethod.StraightLeftRight:
    //            if (parentAbility.source.Facing == Constants.EntityFacing.Left) {
    //                shootDirection = Vector2.left;
    //                shotOrigin = parentAbility.source.leftShotOrigin.position;
    //            }
    //            else {
    //                shootDirection = Vector2.right;
    //                shotOrigin = parentAbility.source.rightShotOrigin.position;
    //            }
    //            break;
    //    }

    //}


    //private void TryShootRay() {

    //    ConfigureRay();
        
    //    RaycastHit2D hit = Physics2D.Raycast(shotOrigin, shootDirection, range, layerMask);

    //    if (hit.collider != null) {
    //        PerformRaycastHit(hit);
    //    }

    //}

    //private void TryShootMultiRay() {
    //    ConfigureRay();

    //    RaycastHit2D[] hits = Physics2D.RaycastAll(shotOrigin, shootDirection, range, layerMask);
    //    int count;

    //    if (numPenetrations > 0) {
    //        count = Mathf.Min(hits.Length, (numPenetrations));
    //    }
    //    else {
    //        count = hits.Length;
    //    }

    //    for(int i = 0; i < count; i++) {
    //        RaycastHit2D hit = hits[i];

    //        if (hit.collider != null) {
    //            PerformRaycastHit(hit);
    //        }
    //    }
    //}


    //private void PerformRaycastHit(RaycastHit2D hit) {
    //    Debug.Log(hit.collider.gameObject.name + " was hit");

    //    Vector2 rayDir = Vector2.Reflect((hit.point - shotOrigin).normalized, hit.normal);
    //    Quaternion impactRotation = TargetingUtilities.CalculateImpactRotation(rayDir);

    //    GameObject hitPrefab = Resources.Load(impactEffectName) as GameObject;

    //    GameObject hitEffect = VisualEffectManager.CreateVisualEffect(hitPrefab, hit.point, impactRotation); 

    //    ParticleSystem[] ps = hitEffect.GetComponentsInChildren<ParticleSystem>();

    //    SpriteRenderer hitSprite = hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>();

    //    for(int i = 0; i < ps.Length; i++) {
    //        ps[i].GetComponent<ParticleSystemRenderer>().sortingOrder = hitSprite.sortingOrder;
    //    }

    //    hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-rayDir * 150f);

    //    Debug.Log(parentAbility.abilityName + " deals " + (effectDamage + parentAbility.source.stats.GetStatCurrentValue(Constants.EntityStat.BaseDamage) + " points of damage"));

    //}

    //private Quaternion CalculateImpactRotation(Vector2 hitPoint, Vector2 hitNormal) {

    //    Vector2 rayDir = Vector2.Reflect((hitPoint - shotOrigin).normalized, hitNormal);
    //    float angle = Mathf.Atan2(-rayDir.x, rayDir.y) * Mathf.Rad2Deg;
    //    Quaternion rot = Quaternion.AngleAxis(angle, Vector2.right);

    //    return rot;
    //}
}
