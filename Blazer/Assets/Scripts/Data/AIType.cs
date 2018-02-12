using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "AIType")]
[System.Serializable]
public class AIType : ScriptableObject {

    public string AIName;
    public AILocomotionType locomotionType;
    public AIAttackType attackType;
    public List<int> initialTargetLayers = new List<int>();
    public float turnSpeed;
    public bool isCliffJumper;
    public bool isLadderClimber;
    public bool isWallScaler;

    [System.Serializable]
    public enum AILocomotionType
    {
        BaseGrounded,
        BaseFlying,

    }

    [System.Serializable]
    public enum AIAttackType
    {
        BaseMelee,
        BaseRanged,

    }

    [System.Serializable]
    public enum BaseGroundStates
    {
        Alert = 0,
        Walking = 1,
        Attacking = 2,
        Stunned = 3,
        Stopped = 4,
        Idle = 5,
    }

}
