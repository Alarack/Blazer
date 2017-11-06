using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

    public enum LookState {
        None = 0,
        AquiringTarget = 1,
        Aiming = 2,
        Targetless = 3,
    }


    [Header("Targeting Information")]
    public LayerMask targetLayer;
    public float detectionRadius;
    public float inaccuracy;
    [Header("Pre-Tracked Wiggles")]
    public float wiggleDuration;
    public float wiggleInterval;
    public float wiggleAmount = 100f;
    [Header("Post-Tracking Stat Changes")]
    public float speedModifier = 1f;
    public float rotationModifier = 1f;
    [Header("Timed Tracking")]
    public bool limitedTimeTracking;
    public float trackingDuration;

    public Transform CurrentTarget { get; private set; }
    public LookState State { get; private set; }

    private List<Transform> alltargets = new List<Transform>();
    private float error;
    private float rotateSpeed;

    private Timer wiggleIntervalTimer;
    private Timer wiggleDurationTimer;
    private Timer trackingDurationTimer;

    private Vector2 randomDirection;

    private Projectile projectile;

    public void Initialize(Entity entity) {
        rotateSpeed = entity.stats.GetStatModifiedValue(Constants.BaseStatType.RotateSpeed);
        Initialize();
    }

    public void Initialize(Projectile projectile) {
        this.projectile = projectile;
        rotateSpeed = projectile.stats.GetStatModifiedValue(Constants.BaseStatType.RotateSpeed);
        targetLayer = projectile.LayerMask;
        Initialize();
    }

    private void Initialize() {
        UpdateError();
        State = LookState.Targetless;

        RandomizeLookDirection();
        wiggleIntervalTimer = new Timer("Wiggle Timer", wiggleInterval, true, RandomizeLookDirection);
        wiggleDurationTimer = new Timer("Wiggle Duration", wiggleDuration, false, OnTrackDelayFinish);
        trackingDurationTimer = new Timer("Tracking Duration", trackingDuration, false, OnTrackingFinished);
    }

    public void ManagedUpdate() {

        switch (State) {
            case LookState.None:
                break;

            case LookState.Targetless:
                wiggleDurationTimer.UpdateClock();
                wiggleIntervalTimer.UpdateClock();

                TargetDelay();
                break;

            case LookState.AquiringTarget:
                AquireTarget();
                break;

            case LookState.Aiming:
                if (limitedTimeTracking) {
                    trackingDurationTimer.UpdateClock();
                }

                Aim();
                break;
        }
    }

    private void OnTrackDelayFinish() {
        ModifyStats();
        State = LookState.AquiringTarget;
    }

    private void OnTrackingFinished() {
        ModifyStats();
        State = LookState.None;
    }

    private void Aim() {
        if (CurrentTarget == null) {
            State = LookState.AquiringTarget;
            return;
        }
        transform.rotation = TargetingUtilities.SmoothRotation(CurrentTarget.position, transform, rotateSpeed, error);
    }

    public void TargetDelay() {
        transform.rotation = TargetingUtilities.SmoothRotation(randomDirection, transform, rotateSpeed, error);
    }

    private void AquireTarget() {
        alltargets = TargetingUtilities.FindAllTargets(transform.position, detectionRadius, targetLayer);
        CurrentTarget = TargetingUtilities.FindNearestTarget(transform.position, alltargets);

        if (CurrentTarget != null)
            State = LookState.Aiming;
    }

    public void UpdateError() {
        error = Random.Range(-inaccuracy, inaccuracy);
    }

    public void ReaquireTarget() {
        AquireTarget();
    }

    public void RandomizeLookDirection() {
        if (wiggleAmount > 0f)
            randomDirection = Random.insideUnitCircle * wiggleAmount;
        else
            randomDirection = new Vector2(transform.position.x, transform.position.y + 10f);
    }


    private void ModifyStats() {
        projectile.stats.ApplyUntrackedMod(Constants.BaseStatType.MoveSpeed, speedModifier, null, StatCollection.StatModificationType.Multiplicative);
        projectile.stats.ApplyUntrackedMod(Constants.BaseStatType.RotateSpeed, rotationModifier, null, StatCollection.StatModificationType.Multiplicative);

        projectile.ProjectileMovement.UpdateBaseStats();
        rotateSpeed = projectile.stats.GetStatModifiedValue(Constants.BaseStatType.RotateSpeed);
    }
}
