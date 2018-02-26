using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationalStatChange : Status {

    protected float statAdjValue;
    protected Constants.BaseStatType targetStat;
    protected StatCollection.StatModificationType modType;

    protected List<StatCollection.StatModifer> mods = new List<StatCollection.StatModifer>();

    public void InitializeDurationalStatChange(Constants.BaseStatType targetStat, StatCollection.StatModificationType modType, float statAdjValue) {
        this.targetStat = targetStat;
        this.modType = modType;
        this.statAdjValue = statAdjValue;


        if(StatusManager.IsTargetAlreadyAffected(targetEntity, this, sourceAbility) == false)
            CreateStatMod();
    }

    public override void Stack() {
        base.Stack();

        CreateStatMod();
        RefreshDuration();
    }


    private void CreateStatMod() {
        Debug.Log("Creating a stat mod : " + targetStat + " " + statAdjValue);

        StatCollection.StatModifer mod = new StatCollection.StatModifer(statAdjValue, modType);
        mods.Add(mod);
        CombatManager.ApplyTrackedStatMod(source, targetEntity, targetStat, mod);

    }

    protected override void CleanUp() {
        int count = mods.Count;

        for(int i = 0; i < count; i++) {
            Debug.Log("Cleaning durational stuff");
            CombatManager.RemoveTrackedStatMod(targetEntity, targetStat, mods[i]);
        }

        base.CleanUp();
    }


}
