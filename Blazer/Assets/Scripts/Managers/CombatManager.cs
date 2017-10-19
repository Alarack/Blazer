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



    public static void AlterStat(Entity causeOfChagne, Entity targetOfChagnge, Constants.EntityStat stat, float value) {

        targetOfChagnge.stats.AlterStat(stat, value, causeOfChagne);


        EventData data = new EventData();

        data.AddMonoBehaviour("Cause", causeOfChagne);
        data.AddMonoBehaviour("Target", targetOfChagnge);
        data.AddInt("Stat", (int)stat);
        data.AddFloat("Value", value);

        Grid.EventManager.SendEvent(Constants.GameEvent.StatChanged, data);

    }


}
