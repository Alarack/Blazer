using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Activateable {

    public enum DoorState
    {
        Open,
        Closed,
        Locked,
        Broken
    }

    public Animator myAnim;
    public DoorState currentState;
    public Collider2D doorColl;

    public void Start()
    {
        switch (currentState)
        {
            case DoorState.Open:
                doorColl.enabled = false;
                break;
            case DoorState.Closed:
                doorColl.enabled = true;
                break;
            case DoorState.Locked:
                currentState = DoorState.Closed;
                doorColl.enabled = true;
                break;
            case DoorState.Broken:
                doorColl.enabled = false;
                break;
        }
    }



    public override void ActivationFunction()
    {
        switch (currentState) {
            case DoorState.Open:
                myAnim.SetBool("Open", false);
                currentState = DoorState.Closed;
                doorColl.enabled = true;
                break;
            case DoorState.Closed:
                myAnim.SetBool("Open", true);
                currentState = DoorState.Open;
                doorColl.enabled = false;
                break;
            case DoorState.Locked:
                currentState = DoorState.Closed;
                break;
            case DoorState.Broken:
                break;
        }


    }
}