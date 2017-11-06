using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : AttackMedium {


    public List<MeleeHitTarget> targets = new List<MeleeHitTarget>();

    public int hits = 1;

    public override void Initialize(Effect parentEffect, LayerMask mask, float life = 0, float damage = 0) {
        base.Initialize(parentEffect, mask, life, damage);
    }

    public override void CleanUp() {
        //Debug.Log("Cleaning Melee");
        base.CleanUp();
        //CreateImpactEffect();
        Destroy(gameObject);
    }

    protected virtual void OnTriggerStay2D(Collider2D other) {
        if ((LayerMask & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) {

            Entity hitTarget = other.gameObject.GetComponent<Entity>();

            if (!CheckHitList(hitTarget)) {
                return;
            }

            parentEffect.Apply(other.gameObject);
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

    private bool CheckHitList(Entity hitTarget) {
        if (hitTarget == null) {
            Debug.Log("[Melee Hit] struck something that wasn't an entity");
            return false;
        }

        for (int i = 0; i < targets.Count; i++) {
            if (targets[i].target == hitTarget) {

                if (targets[i].hitCount >= hits) {
                    //Debug.Log(hitTarget.name + " has already been hit the maximum number of times");
                    return false;
                }
                else {
                    targets[i].AddHit();
                    return true;
                }
            }
        }
        CreateHitEntry(hitTarget);

        return true;
    }

    private void CreateHitEntry(Entity target) {
        for (int i = 0; i < targets.Count; i++) {
            if (targets[i].target == target) {
                targets[i].AddHit();
                //Debug.Log("Adding another hit for " + target.gameObject.name);
                return;
            }
        }

        MeleeHitTarget newtarget = new MeleeHitTarget(target);
        targets.Add(newtarget);
        //Debug.Log("Adding " + target.gameObject.name + " to hit list");
    }


    [System.Serializable]
    public class MeleeHitTarget {
        public Entity target;
        public int hitCount;

        public MeleeHitTarget(Entity target) {
            this.target = target;
            hitCount = 1;
        }

        public void AddHit() {
            hitCount++;
        }
    }

}