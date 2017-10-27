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

        EventData data = new EventData();
        data.AddString("AttackName", animEvent.stringParameter);
        data.AddMonoBehaviour("Entity", owner);

        Grid.EventManager.SendEvent(Constants.GameEvent.AnimationEvent, data);

        Debug.Log("Sending Attack");

    }


}
