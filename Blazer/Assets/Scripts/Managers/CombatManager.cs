using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public static CombatManager combatManager;



    void Awake() {

        if (combatManager == null) {
            combatManager = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            ApplyUntrackedStatMod(null, GameManager.GetPlayer(), Constants.BaseStatType.Money, 1f);
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            ApplyUntrackedStatMod(null, GameManager.GetPlayer(), Constants.BaseStatType.Keys, 1f);
        }
    }



    public static void ApplyUntrackedStatMod(Entity causeOfChagne, Entity targetOfChagnge, Constants.BaseStatType stat, float value, StatCollection.StatModificationType modType = StatCollection.StatModificationType.Additive) {

        float sendableValue = 0f;

        if (stat == Constants.BaseStatType.Health) {
            float armor = targetOfChagnge.stats.GetStatModifiedValue(Constants.BaseStatType.Armor);
            //Debug.Log(armor + " is the armor of " + targetOfChagnge.entityName);
            sendableValue = Mathf.Clamp(value + armor, value, 0f);

            float damageReduction = targetOfChagnge.stats.GetStatModifiedValue(Constants.BaseStatType.DamageReduction);

            float convertedDR = Mathf.Clamp(  Mathf.Abs(damageReduction - 1f), 0f, 1f );

            sendableValue *= convertedDR;

        }
        else {
            sendableValue = value;
        }


        targetOfChagnge.stats.ApplyUntrackedMod(stat, sendableValue, causeOfChagne, modType);

        combatManager.SendStatChangeEvent(causeOfChagne, targetOfChagnge, stat, sendableValue);

        if (stat == Constants.BaseStatType.Health && value < 0f) {
            VisualEffectManager.MakeFloatingText(Mathf.Abs(sendableValue).ToString(), targetOfChagnge.transform.position);
        }

    }

    public static void ApplyTrackedStatMod(Entity causeOfChagne, Entity targetOfChange, Constants.BaseStatType stat, StatCollection.StatModifer mod) {

        targetOfChange.stats.ApplyTrackedMod(stat, mod);

        combatManager.SendStatChangeEvent(causeOfChagne, targetOfChange, stat, mod.value);

        if (stat == Constants.BaseStatType.Health && mod.value < 0f) {
            VisualEffectManager.MakeFloatingText(Mathf.Abs(mod.value).ToString(), targetOfChange.transform.position);
        }
    }

    public static void RemoveTrackedStatMod(Entity targetOfChange, Constants.BaseStatType stat, StatCollection.StatModifer mod) {
        targetOfChange.stats.RemoveTrackedMod(stat, mod);

        Debug.Log("Removing a mod: " + stat + " value of " + mod.value);

        combatManager.SendStatChangeEvent(null, targetOfChange, stat, mod.value);
    }


    private void SendStatChangeEvent(Entity causeOfChagne, Entity targetOfChagnge, Constants.BaseStatType stat, float value) {
        EventData data = new EventData();

        data.AddMonoBehaviour("Cause", causeOfChagne);
        data.AddMonoBehaviour("Target", targetOfChagnge);
        data.AddInt("Stat", (int)stat);
        data.AddFloat("Value", value);

        //Debug.Log("Event Sent: " + stat.ToString() + " :: " + value);
        EventGrid.EventManager.SendEvent(Constants.GameEvent.StatChanged, data);


    }


}
