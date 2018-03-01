using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttemptToClimb : MonoBehaviour {

    public Ladder myLadder;
    public Ladder.Climber myClimber;

    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            myLadder.GrabLadder(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && gameObject.name == "TestClimber")
        {
            transform.Translate(0, 1, 0);
            myLadder.Climb(myClimber, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.name == "TestClimber1")
        {
            transform.Translate(0, 1, 0);
            myLadder.Climb(myClimber, gameObject);
        }
    }
}
