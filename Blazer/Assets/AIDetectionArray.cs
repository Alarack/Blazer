using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetectionArray : MonoBehaviour {

    public AIBrain myBrain;
    public GameObject target = null;
    // Use this for initialization
    void Start () {
        myBrain = GetComponentInParent<AIBrain>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "New Player")
        {
            target = collision.gameObject;
            
        }
        if (target != null)
        {
            if (target.transform.position.x - gameObject.transform.position.x < 0f)
            {
                //Debug.Log("Left");
                myBrain.moveDir = AIBrain.TargetDirection.Left;
            }
            if (target.transform.position.x - gameObject.transform.position.x > 0f)
            {
                //Debug.Log("Right");
                myBrain.moveDir = AIBrain.TargetDirection.Right;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == target)
        {
            Debug.Log("None");
            target = null;
            myBrain.moveDir = AIBrain.TargetDirection.None;
        }
    }
}
