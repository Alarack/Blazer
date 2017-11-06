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

    public enum SpecialAbilityType {
        None = 0,
        Attack = 1,
        Buff = 2,
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
        StatusEffect = 2,
    }

    public enum SpecialAbilityActivationMethod {
        None = 0,
        Manual = 1,
        Timed = 2,
        DamageDealt = 3,
        Passive = 4,
        EntityKilled = 5,
        DamageTaken = 6,
        EffectApplied = 7,
    }

    public enum StatusEffectType {
        None = 0,
        //Burning = 1,
        Stun = 2,
        //KnockBack = 3,
        AffectMovement = 4,
        DamageOverTime = 5
    }

    public enum StatusStackingMethod {
        None = 0,
        LimitedStacks = 1,
        StacksWithOtherAbilities = 2,

    }


    public enum EffectDeliveryMethod {
        None = 0,
        Raycast = 1,
        Projectile = 2,
        Melee = 3,
        Rider = 4,
        SelfTargeting = 5,

    }

    public enum EffectEventOption {
        None = 0,
        Applied = 1,
        Removed = 2,
    }

    public enum ItemPool {
        None = 0,
        Boss = 1,
        Secret = 2,
        Shop = 3,
        Standard = 4
    }

    public enum GameEvent {
        None = 0,
        AbilityActivated = 1,
        StatChanged = 2,
        AnimationEvent = 3,
        EntityDied = 4,
        EffectApplied = 5,

    }

}
