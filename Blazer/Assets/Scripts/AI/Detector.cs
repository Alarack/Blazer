using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Detector : MonoBehaviour {

    protected Collider2D myCollider;
    public List<int> detectedLayers;

    protected virtual void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }
    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(whatIsDetected.value);
        if (detectedLayers.Contains(collision.gameObject.layer))
        {
            Debug.Log("Enter Detector Function Activated" + gameObject.name);
            EnterDetectorFunction(collision);
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.layer);
        if (detectedLayers.Contains(collision.gameObject.layer))
        {
            Debug.Log("Stay Detector Function Activated" + gameObject.name);
            StayDetectorFunction(collision);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (detectedLayers.Contains(collision.gameObject.layer))
        {
            Debug.Log("ExitDetector Function Activated" + gameObject.name);
            ExitDetectorFunction(collision);
        }
    }

    protected virtual void EnterDetectorFunction(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    protected virtual void StayDetectorFunction(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    protected virtual void ExitDetectorFunction(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
