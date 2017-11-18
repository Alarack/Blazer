using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class StatCollection {

    public enum StatModificationType {
        Additive,
        Multiplicative
    }


    private List<BaseStat> baseStats = new List<BaseStat>();
    //private Entity owner;
    private StatCollectionData statTemplate;


    public void Initialize(StatCollectionData statTemplate = null) {
        //this.owner = owner;

        if (statTemplate != null)
            this.statTemplate = statTemplate;
        else
            this.statTemplate = GameManager.GetDefaultStatCollection();

        InitializeDefaultStats();
    }

    public float GetStatModifiedValue(Constants.BaseStatType statType) {
        for(int i = 0; i < baseStats.Count; i++) {
            if(baseStats[i].statType == statType) {
                return baseStats[i].GetModifiedValue();
            }
        }

        return 0;
    }

    public float GetStatMultipler(Constants.BaseStatType statType) {
        for (int i = 0; i < baseStats.Count; i++) {
            if (baseStats[i].statType == statType) {
                return baseStats[i].GetTotalMultiplier();
            }
        }

        return 1f;
    }


    public void ApplyUntrackedMod(Constants.BaseStatType statType, float value, Entity source, StatModificationType modType = StatModificationType.Additive) {
        BaseStat targetStat = GetStat(statType);
        targetStat.ModifyStat(value, modType);
    }

    public void ApplyTrackedMod(Constants.BaseStatType statType, StatModifer mod) {
        BaseStat targetStat = GetStat(statType);
        targetStat.ModifyStat(mod);
    }

    public void RemoveTrackedMod(Constants.BaseStatType statType, StatModifer mod) {
        BaseStat targetStat = GetStat(statType);
        targetStat.RemoveModifier(mod);
    }


    private void InitializeDefaultStats() {
        for(int i = 0; i < statTemplate.stats.Count; i++) {
            BaseStat newStat = new BaseStat(statTemplate.stats[i].stat, statTemplate.stats[i].maxValue, statTemplate.stats[i].maxValue);
            //Debug.Log("Adding " + newStat.statType.ToString() + " with a value of " + newStat.MaxValue);
            baseStats.Add(newStat);
        }
    }

    private BaseStat GetStat(Constants.BaseStatType statType) {
        for (int i = 0; i < baseStats.Count; i++) {
            if (baseStats[i].statType == statType) {
                return baseStats[i];
            }
        }

        return null;
    }



    [System.Serializable]
    public class BaseStat {
        public Constants.BaseStatType statType;
        public float BaseValue { get; private set; }
        public float MaxValue { get; private set; }
        private List<StatModifer> mods = new List<StatModifer>();

        public BaseStat(Constants.BaseStatType statType, float baseValue, float maxValue) {
            this.statType = statType;
            BaseValue = baseValue;
            MaxValue = maxValue;
        }

        public float GetModifiedValue() {
            

            float result = BaseValue + GetTotalAddativeMod();
            result *= GetTotalMultiplier();

            //Debug.Log("Getting a value of " + statType + ". Value of: " + result);

            //if (result <= 0f)
            //    result = 0f;

            return result;
        }

        public float GetTotalMultiplier() {
            float totalMultiplier = 1f;
            List<StatModifer> allMulipliers = new List<StatModifer>();

            for (int i = 0; i < mods.Count; i++) {
                if (mods[i].modType == StatModificationType.Multiplicative) {
                    allMulipliers.Add(mods[i]);
                    totalMultiplier += mods[i].value;
                }
            }

            if (allMulipliers.Count < 1) {
                return 1f;
            }

            return totalMultiplier;
        }

        public float GetTotalAddativeMod() {
            float result = 0;
            for (int i = 0; i < mods.Count; i++) {
                if (mods[i].modType == StatModificationType.Additive) {
                    result += mods[i].value;
                }
            }

            return result;
        }


        public void ModifyStat(StatModifer mod) {
            mods.Add(mod);
        }

        public void RemoveModifier(StatModifer mod) {
            if (mods.Contains(mod))
                mods.Remove(mod);
        }

        public void ModifyStat(float value, StatModificationType modType = StatModificationType.Additive) {

            mods.Add(new StatModifer(value, modType));

            //switch (modType) {
            //    case StatModificationType.Additive:
            //        //CurrentValue += value;
            //        mods.Add(new StatModifer(value, StatModificationType.Additive));

            //        break;

            //    case StatModificationType.Multiplicative:
            //        //CurrentValue *= value;
            //        mods.Add(new StatModifer(value, StatModificationType.Multiplicative));
            //        break;
            //}

            //if (BaseValue > MaxValue) {
            //    BaseValue = MaxValue;
            //}

            //if (BaseValue <= 0f)
            //    BaseValue = 0f;
        }

        public void ModifyMaxValue(float value, bool updateCurrent = false) {

            MaxValue += value;

            if (updateCurrent) {
                ModifyStat(value);
            }

            if (BaseValue > MaxValue)
                BaseValue = MaxValue;

            if (MaxValue <= 0f)
                MaxValue = 0f;
        }
    }


    [System.Serializable]
    public class StatModifer {
        public float value;
        public StatModificationType modType;

        public StatModifer(float value, StatModificationType modType) {
            this.value = value;
            this.modType = modType;
        }
    }


}
