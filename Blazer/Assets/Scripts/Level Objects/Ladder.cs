using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    public float ladderLength;

    private List<Climber> myClimbers = new List<Climber>();



    public class Climber

    {

        public GameObject climbObject;

        public float climberLocation;



        public Climber(GameObject climbObject, float climberLocation)

        {

            this.climbObject = climbObject;

            this.climberLocation = climberLocation;

        }

    }



    private void Start()

    {



    }





    private void FixedUpdate()

    {

        //Debug.Log(myClimbers.Count + " is the number of current climbers");

        //Debug.Log(Vector2.Distance(transform.position, climber.transform.position));

        foreach (Climber climber in myClimbers)

        {

            //Debug.Log(climber.climbObject.name + " " + climber.climberLocation);

        }

    }



    public void GrabLadder(GameObject attemptedToGrab)

    {

        Climber tempClimb = new Climber(attemptedToGrab, TransformToLadderLocation(attemptedToGrab));

        //Debug.Log(attemptedToGrab + "attempted to grab ladder");

        myClimbers.Add(tempClimb);

        attemptedToGrab.GetComponent<AttemptToClimb>().myClimber = tempClimb;

    }



    public void Climb(Climber myClimber, GameObject me)

    {

        if (TransformToLadderLocation(me) <= ladderLength && TransformToLadderLocation(me) >= 0)

        {

            myClimber.climberLocation = TransformToLadderLocation(me);

        }

        else if (TransformToLadderLocation(me) > ladderLength)

        {

            LetGoLadder();

        }

        else if (TransformToLadderLocation(me) < 0)

        {

            LetGoLadder();

        }

        //Debug.Log(climbObject + "should be climbing");

    }



    public float TransformToLadderLocation(GameObject climbObject)

    {

        float transformLocation = Vector2.Distance(climbObject.transform.localPosition, transform.position);

        float ladderLocation = 0;



        //Debug.Log(climbObject + "" + transformLocation);

        if (transformLocation != 0)

        {

            if (Mathf.Min(climbObject.transform.position.y, transform.position.y) == climbObject.transform.position.y)

            {

                ladderLocation = (ladderLength / 2f) - transformLocation;

                //Debug.Log(ladderLocation);

            }

            else if (Mathf.Min(climbObject.transform.position.y, transform.position.y) == transform.position.y)

            {

                ladderLocation = transformLocation + (ladderLength / 2f);

                //Debug.Log(ladderLocation);



            }

        }

        else

        {

            ladderLocation = (ladderLength / 2f);

            //Debug.Log(ladderLocation);

        }



        return ladderLocation;

    }



    //private void OnTriggerStay2D(Collider2D other)

    //{

    //    if (validUserTags.Contains(other.gameObject.tag))

    //    {

    //        other.gameObject.GetComponent<EntityMovement>().canClimb = true;

    //        other.gameObject.GetComponent<EntityMovement>().currentLadder = this.gameObject;

    //    }

    //}



    //private void OnTriggerExit2D(Collider2D other)

    //{

    //    if (validUserTags.Contains(other.gameObject.tag))

    //    {

    //        other.gameObject.GetComponent<EntityMovement>().ClimbEnd();

    //        other.gameObject.GetComponent<EntityMovement>().canClimb = false;

    //        other.gameObject.GetComponent<EntityMovement>().currentLadder = null;

    //        other.gameObject.GetComponent<EntityMovement>().ClimbEnd();

    //    }

    //}



    public void LetGoLadder()

    {

        Debug.Log("A climber has let go of ladder");

    }

}

