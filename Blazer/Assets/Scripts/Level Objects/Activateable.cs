using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activateable : MonoBehaviour {

    public enum ActivateableType
    {
        Chest,
        Door,
        Shrine,
        Shop,
        LevelExit,
    }
    protected bool canActivate = false;
    public ActivateableType type;

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canActivate)
        {
            ActivationFunction();
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("can be activated by player");
            canActivate = true;
            /*--Insert ActivationFunction reference and event hooks here--*/
      
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("can no longer be activated by player");
        canActivate = false;
    }

    public virtual void ActivationFunction()
    {
        /*--Insert function & DisableActivateable here--*/
    }
     public virtual void DisableActivateable()
        {
        switch (type)
        {
            default:
                Debug.Log("Fell out of activateable switch");
                break;
            case ActivateableType.Chest:

                break;


        }
        /*--If it is one time use--*/
        Destroy(GetComponent<Activateable>());

        /*--If it is NOT one time use, or some other method of handling things.--*/

    }
}
