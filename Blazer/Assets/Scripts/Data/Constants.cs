using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {

    public enum BaseStatType {
        None = 0,
        Health = 1,
        BaseDamage = 2,
        MoveSpeed = 3,
        JumpForce = 4,
        CritChance = 5,
        CritMultiplier = 6,
        AttackSpeed = 7,
        RotateSpeed = 8,
        Lifetime = 9,
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
        AttackEffect = 1,
        SelfBuff = 2,
    }

    public enum EffectDeliveryMethod {
        None = 0,
        Raycast = 1,
        Projectile = 2,
        Melee = 3,

    }

    public enum GameEvent {
        None = 0,
        AbilityActivated = 1,
        StatChanged = 2,

    }

}
