using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffDetector : Detector {

    private AIBrain myBrain;

    protected override void Awake()
    {
        base.Awake();
        detectedLayers = new List<int>(17);
        myBrain = GetComponentInParent<AIBrain>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);        
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }

    //protected override void EnterDetectorFunction(Collider2D collision)
    //{
    //    base.EnterDetectorFunction(collision);
    //    if (collision.transform.position.x - gameObject.transform.position.x < 0f)
    //    {
    //        //Debug.Log("Left");
    //        switch (collision.gameObject.tag)
    //        {
    //            case "TallCliff":
    //                myBrain.CliffAvoidCheck(true, AIBrain.Direction.Left);
    //                break;
    //            case "SmallCliff":
    //                myBrain.CliffAvoidCheck(false, AIBrain.Direction.Left);
    //                break;
    //        }
    //    }
    //    if (collision.transform.position.x - gameObject.transform.position.x > 0f)
    //    {
    //        //Debug.Log("Right");
    //        switch (collision.gameObject.tag)
    //        {
    //            case "TallCliff":
    //                myBrain.CliffAvoidCheck(true, AIBrain.Direction.Right);
    //                break;
    //            case "SmallCliff":
    //                myBrain.CliffAvoidCheck(false, AIBrain.Direction.Right);
    //                break;
    //        }
    //    }
    //}
    protected override void StayDetectorFunction(Collider2D collision)
    {
        base.StayDetectorFunction(collision);
    }
    protected override void ExitDetectorFunction(Collider2D collision)
    {
        base.ExitDetectorFunction(collision);

    }
}
