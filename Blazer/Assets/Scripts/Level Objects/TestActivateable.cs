using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActivateable : Activateable {

    public override void ActivationFunction()
    {
        Debug.Log("Chest Opened");
        GetComponent<LootManager>().SpawnLoot();
        DisableActivateable();
    }
}
