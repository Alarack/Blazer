using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActivateable : Activateable {

    public Animator myAnim;
    public override void ActivationFunction()
    {
        Debug.Log("Chest Opened");
        myAnim.SetTrigger("Open");
        GetComponent<LootManager>().SpawnLoot();
        DisableActivateable();
    }
}
