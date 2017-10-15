using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryCooldown : SpecialAbilityRecovery {

    public float cooldown;

    private float _timer;

    public override void Recover() {
        base.Recover();

        if (!Ready) {
            IncrementTimer();
            CheckReady();
        }
    }

    private void IncrementTimer() {
        if (_timer < cooldown) {
            _timer += Time.deltaTime;
        }
    }

    private void CheckReady() {
        if (_timer >= cooldown) {
            _timer = 0f;
            Ready = true;
        }
    }

}
