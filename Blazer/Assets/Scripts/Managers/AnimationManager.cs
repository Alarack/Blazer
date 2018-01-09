using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {



    private Entity owner;


    private void Awake() {
        this.owner = GetComponentInParent<Entity>();
    }



    public void Initialize(Entity owner) {
        this.owner = owner;

    }



    public void Attack(AnimationEvent animEvent) {
        //Debug.Log("Sending Attack");

        EventData data = new EventData();
        data.AddString("AttackName", animEvent.stringParameter);
        //data.AddMonoBehaviour("Entity", owner);
        data.AddInt("ID", owner.SessionID);

        Debug.Log(owner.entityName + " " + owner.SessionID + " is sending an animation event");

        Grid.EventManager.SendEvent(Constants.GameEvent.AnimationEvent, data);



    }


}
