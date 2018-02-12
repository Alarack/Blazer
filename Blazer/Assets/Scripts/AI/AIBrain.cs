using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIBrain : MonoBehaviour {

    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
    public class TargetDirection
    {
        public Direction verticalDirection;
        public Direction horizontalDirection;
        
        public TargetDirection(Direction vert, Direction hori)
        {
            verticalDirection = vert;
            horizontalDirection = hori;
        }
    }

    public TargetDirection targetDirection = new TargetDirection(Direction.None, Direction.None);

    public AIType myType;
    public List<int> targetLayers;
    [HideInInspector]
    public AIStateMachine myStateMachine;
    [HideInInspector]
    public Animator myAnimator;
    public NPCAbilityManager abilityManager;

    public void Awake()
    {
        targetLayers = myType.initialTargetLayers;
        for (int i = 0; i < targetLayers.Count; i++)
        {
            Debug.Log(i);
        }
        myAnimator = GetComponentInChildren<Animator>();
        //Debug.Log(myType.locomotionType);
        switch (myType.locomotionType)
        {
            case AIType.AILocomotionType.BaseGrounded:
                gameObject.AddComponent<TestStateMachine>();
                break;
            case AIType.AILocomotionType.BaseFlying:
                gameObject.AddComponent<Test2StateMachine>();
                break;
            default:
                break;
        }
        myStateMachine = GetComponent<AIStateMachine>();
    }
}
