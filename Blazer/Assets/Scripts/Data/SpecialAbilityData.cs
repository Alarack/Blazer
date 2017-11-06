using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpecialAbility")]
[System.Serializable]
public class SpecialAbilityData : ScriptableObject {

    public string abilityName;
    public Sprite abilityIcon;
    public float useDuration;
    public bool overrideOtherAbilities;
    public float procChance = 1f;
    public List<Constants.SpecialAbilityEffectType> effectTypes = new List<Constants.SpecialAbilityEffectType>();
    public Constants.SpecialAbilityRecoveryType recoveryType;
    public Constants.SpecialAbilityActivationMethod activationMethod;
    public Constants.SpecialAbilityType abilityType;

    public AbilityRecoveryManager recoveryManager = new AbilityRecoveryManager();

    public EffectHolder effectHolder = new EffectHolder();

    public List<SpecialAbilityData> sequencedAbilities = new List<SpecialAbilityData>();
    public float sequenceWindow = 0.5f;

    public List<SpecialAbilityRecovery> GetAllRecoveryMechanics() {
        List<SpecialAbilityRecovery> results = new List<SpecialAbilityRecovery>();

        for(int i = 0; i < recoveryManager.recoveryTypes.Count; i++) {
            results.Add(recoveryManager.GetRecoveryMethodByType(recoveryManager.recoveryTypes[i]));
        }

        return results;
    }

    public SpecialAbilityRecovery GetRecoveryMechanic() {

        SpecialAbilityRecovery result = ObjectCopier.Clone(recoveryManager.GetRecoveryMethodByType(recoveryType)) as SpecialAbilityRecovery;

        return result;
    }


    public List<Effect> GetAllEffects() {
        List<Effect> results = new List<Effect>();
        for(int i = 0; i < effectTypes.Count; i++) {

            EffectSet holder = effectHolder.GetEffectSet(effectTypes[i]);

            if(holder != null) {
                results.AddRange(holder.effects);
            }

        }
        return results;
    }





    [System.Serializable]
    public class EffectHolder {

        public List<EffectAttack> attacks = new List<EffectAttack>();
        public List<EffectStatus> statusEffects = new List<EffectStatus>();



        public EffectSet GetEffectSet(Constants.SpecialAbilityEffectType effectType) {
            switch (effectType) {
                case Constants.SpecialAbilityEffectType.AttackEffect:
                    EffectSet raycastAttacks = new EffectSet(effectType, attacks.ConvertAll<Effect>(b => (Effect)b));

                    return raycastAttacks;

                case Constants.SpecialAbilityEffectType.StatusEffect:

                    EffectSet statusAttacks = new EffectSet(effectType, statusEffects.ConvertAll<Effect>(b => (Effect)b));

                    return statusAttacks;

                default:
                    return null;
            }
        }
    }

    [System.Serializable]
    public class EffectSet {
        public Constants.SpecialAbilityEffectType effectType;
        public List<Effect> effects = new List<Effect>();


        public EffectSet(Constants.SpecialAbilityEffectType effectType, List<Effect> effects) {
            this.effects = effects;
            this.effectType = effectType;
        }

    }


}
