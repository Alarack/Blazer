using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour {

    public enum EnemyState {
        None = 0,
        Walking = 1,
        Attacking = 2,
    }

    public EnemyState State { get; set; }
    public LayerMask whatIsEnemy;
    public float meleeRange;
    public Transform visualCenter;

    protected EntityMovement movement;
    protected Entity parentEntity;
    protected NPCAbilityManager abilityManager;


    protected virtual void Awake() {
        movement = GetComponent<EntityMovement>();
        parentEntity = GetComponent<Entity>();
        
    }

    public void Initialize() {
        abilityManager = parentEntity.AbilityManager as NPCAbilityManager;
    }


    protected virtual void Update() {
        CheckEnemy();


        switch (State) {
            case EnemyState.Attacking:
                abilityManager.ActivateAbility();
                break;

            case EnemyState.Walking:

                break;

            default:

                break;
        }
    }


    public virtual void CheckEnemy() {

        Vector2 rayDir;

        switch (parentEntity.Facing) {
            case Constants.EntityFacing.Left:
                rayDir = Vector2.left;
                break;

            case Constants.EntityFacing.Right:
                rayDir = Vector2.right;
                break;

            default:
                rayDir = Vector2.right;
                break;
        }

        RaycastHit2D hit = Physics2D.Raycast(visualCenter.position, rayDir, meleeRange, whatIsEnemy);
        Debug.DrawRay(visualCenter.position, rayDir * meleeRange, Color.red);
        //Debug.DrawLine(transform.position, rayDir, Color.red);

        if (hit.collider != null) {
            Debug.Log("Hit " + hit.collider.gameObject.name);
            State = EnemyState.Attacking;
        }
        else {
            State = EnemyState.Walking;
        }

    }

}
