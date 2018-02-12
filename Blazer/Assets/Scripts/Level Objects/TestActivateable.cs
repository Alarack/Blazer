using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestActivateable : Activateable {

    public Animator myAnim;
    public Image myImage;
    protected override void Update()
    {
        base.Update();
        if (canActivate)
        {
            //Debug.Log("Wut");
            myImage.enabled = true;
        }
        
        if(!canActivate)
        {
            myImage.enabled = false;
        }
    }
    public override void ActivationFunction()
    {
        Debug.Log("Chest Opened");
        myAnim.SetTrigger("Open");
        GetComponent<LootManager>().SpawnLoot();
        myImage.enabled = false;
        DisableActivateable();
    }
}
