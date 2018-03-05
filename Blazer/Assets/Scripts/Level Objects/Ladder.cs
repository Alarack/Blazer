using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    public GameObject ladderTopObj;
    public GameObject ladderBotObj;

    public float ladderTop;
    public float ladderBot;

    private List<Climber> myClimbers = new List<Climber>();

    public class Climber
    {
        public GameObject climbObject;
        public Vector2 climberPoint;

        public Climber(GameObject climbObject, Vector2 climberPoint)
        {
            this.climbObject = climbObject;
            this.climberPoint = climberPoint;
        }
    }
    private void Start()
    {
        ladderBot = ladderBotObj.transform.position.y;
        ladderTop = ladderTopObj.transform.position.y;
    }
    private void FixedUpdate()
    {
    }
    public void GrabLadder(GameObject attemptedToGrab)
    {
        Debug.Log("Grabbed Ladder" + attemptedToGrab.GetComponent<Rigidbody2D>().velocity);
        attemptedToGrab.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Climber tempClimb = new Climber(attemptedToGrab, attemptedToGrab.GetComponent<EntityMovement>().climbPoint.position);
        //Debug.Log(attemptedToGrab + "attempted to grab ladder");
        myClimbers.Add(tempClimb);
        attemptedToGrab.GetComponent<Rigidbody2D>().gravityScale = 0;
        attemptedToGrab.GetComponent<EntityMovement>().myClimber = tempClimb;
        attemptedToGrab.GetComponent<EntityMovement>().isClimbing = true;
    }


    public void ClimbUp(Climber myClimber)
    {
        //Debug.Log(myClimber.climberPoint.y + " " + ladderBotEnd.position.y + " " + ladderTopEnd.position.y);
        if (myClimber.climberPoint.y <= ladderTop && myClimber.climberPoint.y >= ladderBot)
        {
            myClimber.climbObject.transform.position = new Vector2(transform.position.x, myClimber.climbObject.transform.position.y);
        }
        else if (myClimber.climberPoint.y > ladderTop)
        {
            LetGoLadder(myClimber.climbObject);
        }
        myClimber.climberPoint = myClimber.climbObject.GetComponent<EntityMovement>().climbPoint.position;
    }
    public void ClimbDown(Climber myClimber)
    {
        //Debug.Log(myClimber.climberPoint.y + " " + ladderBotEnd.position.y + " " + ladderTopEnd.position.y);
        if (myClimber.climberPoint.y <= ladderTop && myClimber.climberPoint.y >= ladderBot)
        {
            //myClimber.climbObject.GetComponent<Rigidbody2D>().position = new Vector2(transform.position.x, myClimber.climbObject.transform.position.y);
        }
        else if (myClimber.climberPoint.y < ladderBot)
        {
            LetGoLadder(myClimber.climbObject);
        }
        myClimber.climberPoint = myClimber.climbObject.GetComponent<EntityMovement>().climbPoint.position;
    }
    public void ClimbHold(Climber myClimber)
    {
        myClimber.climbObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        myClimber.climberPoint = myClimber.climbObject.GetComponent<EntityMovement>().climbPoint.position;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<EntityMovement>() != null && other.gameObject.GetComponent<EntityMovement>().climbPoss)
        {
            if (other.gameObject.transform.position.y < ladderTop && other.gameObject.transform.position.y >= ladderBot)
            {
                other.gameObject.GetComponent<EntityMovement>().myLadder = this;
                other.gameObject.GetComponent<EntityMovement>().canClimb = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EntityMovement>() != null)
        {
            if (other.gameObject.GetComponent<EntityMovement>().climbPoss)
            {
                other.gameObject.GetComponent<EntityMovement>().myLadder = this;
                other.gameObject.GetComponent<EntityMovement>().canClimb = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EntityMovement>() != null)
        {
            if(other.gameObject.GetComponent<EntityMovement>().isClimbing == true)
            {
                LetGoLadder(other.gameObject);
            }
            other.gameObject.GetComponent<EntityMovement>().myLadder = null;
            other.gameObject.GetComponent<EntityMovement>().canClimb = false;

        }
    }
    public void LetGoLadder(GameObject attemptedToLetGo)
    {
        attemptedToLetGo.GetComponent<EntityMovement>().isClimbing = false;
        attemptedToLetGo.GetComponent<Rigidbody2D>().gravityScale = 1f;
        attemptedToLetGo.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Debug.Log("A climber has let go of ladder");
    }
}