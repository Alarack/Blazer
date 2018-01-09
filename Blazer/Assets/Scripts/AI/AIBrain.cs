using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour {


    /*--This is a setting kicked straight from the AI detection Array that determines which way the AI will move--*/
    public enum TargetDirection {
        None,
        Left,
        Right
    }

    public enum EnemyState {
        None = 0,
        Walking = 1,
        Attacking = 2,
    }

    public EnemyState State { get; set; }
    public LayerMask whatIsEnemy;
    public float meleeCheckRadius;
    public Transform visualCenter;

    /*--Added these variables for obvious reasons--*/
    public TargetDirection moveDir;
    public bool inMeleeRange = false;

    protected BaseEnemyMovement movement;
    protected Entity parentEntity;
    protected NPCAbilityManager abilityManager;


    protected virtual void Awake() {
        movement = GetComponent<BaseEnemyMovement>();
        parentEntity = GetComponent<Entity>();

    }

    public void Initialize() {
        abilityManager = parentEntity.AbilityManager as NPCAbilityManager;
    }


    protected virtual void Update() {
        //Debug.Log(State);

        CheckEnemy();


        switch (State) {
            /*--Added this as a general AI practice--*/
            case EnemyState.None:
                switch (moveDir) {
                    case TargetDirection.None:
                        break;
                    case TargetDirection.Right:
                        if (movement.facingMod != 1) {
                            movement.Flip();
                        }
                        if (!inMeleeRange) {
                            State = EnemyState.Walking;
                        }
                        else {
                            State = EnemyState.Attacking;
                        }
                        break;
                    case TargetDirection.Left:
                        if (movement.facingMod != -1) {
                            movement.Flip();
                        }
                        if (!inMeleeRange) {
                            State = EnemyState.Walking;
                        }
                        else {
                            State = EnemyState.Attacking;
                        }
                        break;
                    default:
                        break;
                }
                break;
            /*--End of added section*/

            case EnemyState.Attacking:
                abilityManager.ActivateAbility();
                State = EnemyState.None;
                break;

            case EnemyState.Walking:
                switch (moveDir) {
                    case TargetDirection.None:
                        break;
                    case TargetDirection.Right:
                        if (movement.facingMod != 1) {
                            movement.Flip();
                        }
                        if (!inMeleeRange) {
                            State = EnemyState.Walking;
                        }
                        else {
                            State = EnemyState.Attacking;
                        }
                        break;
                    case TargetDirection.Left:
                        if (movement.facingMod != -1) {
                            movement.Flip();
                        }
                        if (!inMeleeRange) {
                            State = EnemyState.Walking;
                        }
                        else {
                            State = EnemyState.Attacking;
                        }
                        break;
                    default:
                        break;
                }
                break;

            default:

                break;
        }
    }


    public virtual void CheckEnemy() {

        /*--New Attack checker; needs some tweaking, but works fine and doesn't rely on facing--*/
        if (Physics2D.OverlapCircle(visualCenter.position, meleeCheckRadius, whatIsEnemy)) {
            //Debug.Log("In Attack Range");
            inMeleeRange = true;
        }
        else {
            inMeleeRange = false;
        }





        /*--Took out this for improved attack range detection--*/
        //Vector2 rayDir;

        //switch (parentEntity.Facing) {
        //    case Constants.EntityFacing.Left:
        //        rayDir = Vector2.left;
        //        break;

        //    case Constants.EntityFacing.Right:
        //        rayDir = Vector2.right;
        //        break;

        //    default:
        //        rayDir = Vector2.right;
        //        break;
        //}

        //RaycastHit2D hit = Physics2D.Raycast(visualCenter.position, rayDir, meleeCheckRadius, whatIsEnemy);
        //Debug.DrawRay(visualCenter.position, rayDir * meleeCheckRadius, Color.red);
        //Debug.DrawLine(transform.position, rayDir, Color.red);


        /*--Was interfering with chasing functionality--*/
        //if (hit.collider != null) {
        //    State = EnemyState.Attacking;
        //}
        //else {
        //    State = EnemyState.Walking;
        //}

    }

}
