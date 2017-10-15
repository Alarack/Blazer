using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpecialAbilityRecovery {

    public Constants.SpecialAbilityRecoveryType recoveryType;
    public bool Ready { get; protected set; }


    public virtual void Recover() {

    }


}
