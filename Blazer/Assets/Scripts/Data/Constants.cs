using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {

    public enum EntityStat {
        None = 0,
        Health = 1,
        BaseDamage = 2,
        MoveSpeed = 3,
        JumpForce = 4,
        CritChance = 5,
        CritDamage = 6,
        AttackSpeed = 7,
    }

    public enum EntityFacing {
        None = 0,
        Left = 1,
        Right = 2,
    }

    public enum SpecialAbilityRecoveryType {
        None = 1,
        Timed = 2,
        Kills = 3,
        DamageDealt = 4,
        DamageTaken = 5,
        CurrencyChanged = 6,
    }

    public enum SpecialAbilityEffectType {
        None = 0,
        RayCastAttack = 1,
        SelfBuff = 2,
    }

    public enum GameEvent {
        None = 0,
        AbilityActivated = 1,

    }

}
