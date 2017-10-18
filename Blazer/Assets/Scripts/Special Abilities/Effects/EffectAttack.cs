using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectAttack : Effect {

    public int baseDamage;

    public bool burstAttack;
    public int burstNumber;
    public float burstInterval;

    public string impactEffectName;
    public string fireEffectName;

    protected Vector2 shotOrigin;





    protected virtual IEnumerator BurstFire(float delay, int number) {
        yield return new WaitForSeconds(delay);
    }

    protected virtual void Fire() {

    }


}
