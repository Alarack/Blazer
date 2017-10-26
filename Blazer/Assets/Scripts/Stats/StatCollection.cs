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

    public float GetStatCurrentValue(Constants.BaseStatType statType) {
        for(int i = 0; i < baseStats.Count; i++) {
            if(baseStats[i].statType == statType) {
                return baseStats[i].CurrentValue;
            }
        }

        return 0;
    }


    public void AlterStat(Constants.BaseStatType statType, float value, Entity source) {
        BaseStat targetStat = GetStat(statType);
        targetStat.ModifyStat(value);
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
        public float CurrentValue { get; private set; }
        public float MaxValue { get; private set; }

        public BaseStat(Constants.BaseStatType statType, float currentValue, float maxValue) {
            this.statType = statType;
            CurrentValue = currentValue;
            MaxValue = maxValue;
        }

        public void ModifyStat(float value, StatModificationType modType = StatModificationType.Additive) {

            switch (modType) {
                case StatModificationType.Additive:
                    CurrentValue += value;
                    break;

                case StatModificationType.Multiplicative:
                    CurrentValue *= value;
                    break;
            }

            if (CurrentValue > MaxValue) {
                CurrentValue = MaxValue;
            }

            if (CurrentValue <= 0f)
                CurrentValue = 0f;
        }

        public void ModifyMaxValue(float value, bool updateCurrent = false) {

            MaxValue += value;

            if (updateCurrent) {
                ModifyStat(value);
            }

            if (CurrentValue > MaxValue)
                CurrentValue = MaxValue;

            if (MaxValue <= 0f)
                MaxValue = 0f;
        }
    }

}
