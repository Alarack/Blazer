using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectMovement : Status {

    public enum AffectMovementType {
        Increase,
        Decrease,
        Halt,
        Knockback
    }


    protected BaseMovement targetMovement;
    protected AffectMovementType affectType;
    protected float amount;
    protected Vector2 knockbackVector;

    public override void Initialize(GameObject target, float duration, float interval, Constants.StatusEffectType statusType) {
        base.Initialize(target, duration, interval, statusType);


    }

    public void InitializeAffectMovement(AffectMovementType type, float value, Vector2 knockback) {
        affectType = type;
        amount = value;

        targetMovement = target.GetComponent<BaseMovement>();

        if (targetMovement == null)
            return;

        switch (affectType) {

            case AffectMovementType.Halt:

                if (targetMovement is EntityMovement) {
                    if (((EntityMovement)targetMovement).Grounded)
                        target.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }

                targetMovement.CanMove = false;
                break;

            case AffectMovementType.Knockback:

                //Vector2 direction = TargetingUtilities.DegreeToVector2(value);

                //Vector2 flipped = new Vector2(-direction.x, direction.y);

                //Debug.Log(knockback);

                target.GetComponent<Rigidbody2D>().AddForce(knockback * value);

                break;
        }


    }


    






    protected override void CleanUp() {
        if (targetMovement == null) {
            Destroy(this);
            return;
        }

        switch (affectType) {
            case AffectMovementType.Halt:
                targetMovement.CanMove = true;
                break;
        }


        base.CleanUp();
    }

}
