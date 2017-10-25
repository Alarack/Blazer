using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public static CombatManager combatManager;



	void Awake () {

        if(combatManager == null) {
            combatManager = this;
        }
        else {
            Destroy(gameObject);
        }
	}



    public static void AlterStat(Entity causeOfChagne, Entity targetOfChagnge, Constants.BaseStatType stat, float value) {

        targetOfChagnge.stats.AlterStat(stat, value, causeOfChagne);


        EventData data = new EventData();

        data.AddMonoBehaviour("Cause", causeOfChagne);
        data.AddMonoBehaviour("Target", targetOfChagnge);
        data.AddInt("Stat", (int)stat);
        data.AddFloat("Value", value);

        Grid.EventManager.SendEvent(Constants.GameEvent.StatChanged, data);

        if(stat == Constants.BaseStatType.Health && value < 0f) {
            VisualEffectManager.MakeFloatingText(Mathf.Abs(value).ToString(), targetOfChagnge.transform.position);
        }

    }


}
