using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpecialAbility")]
[System.Serializable]
public class SpecialAbilityData : ScriptableObject {

    public string abilityName;
    public List<Constants.SpecialAbilityEffectType> effects = new List<Constants.SpecialAbilityEffectType>();

    public SpecialAbilityRecovery recoveryMethod;

    //public List<Effect> testEffects = new List<Effect>();
    public EffectHolder effectHolder = new EffectHolder();



    public List<Effect> GetAllEffects() {
        List<Effect> results = new List<Effect>();

        for(int i = 0; i < effects.Count; i++) {
            results.AddRange(effectHolder.GetEffectSet(effects[i]).effects);
        }


        return results;
    }





    [System.Serializable]
    public class EffectHolder {

        public List<EffectRayCastAttack> rayCastAttacks = new List<EffectRayCastAttack>();




        public EffectSet GetEffectSet(Constants.SpecialAbilityEffectType effectType) {
            switch (effectType) {
                case Constants.SpecialAbilityEffectType.RayCastAttack:
                    EffectSet raycastAttacks = new EffectSet(effectType, rayCastAttacks.ConvertAll<Effect>(b => (Effect)b));

                    return raycastAttacks;

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
