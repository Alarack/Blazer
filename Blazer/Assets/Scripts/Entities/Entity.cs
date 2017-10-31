using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity : MonoBehaviour {
    [Header("Basic Info")]
    public string entityName;

    [Header("Attack Origin Points")]
    public Transform leftShotOrigin;
    public Transform rightShotOrigin;

    [Header("Entity Stats")]
    public StatCollectionData statTemplate;
    public StatCollection stats;

    public SpriteRenderer SpriteRenderer { get; protected set; }
    public Constants.EntityFacing Facing { get; set; }
    public Animator MyAnimator { get; protected set; }

    protected AbilityManager abilityManager;
    protected EntityMovement movement;
    protected HealthDeathManager healthDeathManager;


    void Start() {
        Initialize();
    }

    public void Initialize() {
        stats = new StatCollection();
        stats.Initialize(statTemplate);

        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        MyAnimator = GetComponentInChildren<Animator>();
        abilityManager = GetComponent<AbilityManager>();

        if(abilityManager != null)
            abilityManager.Initialize(this);

        movement = GetComponent<EntityMovement>();

        if(movement != null)
            movement.Initialize();

        healthDeathManager = GetComponent<HealthDeathManager>();

        if (healthDeathManager != null)
            healthDeathManager.Initialize(this);

        GameManager.RegisterEntity(this);


    }

    public void UnregisterListeners() {
        Grid.EventManager.RemoveMyListeners(movement);
        Grid.EventManager.RemoveMyListeners(healthDeathManager);

    }




}
