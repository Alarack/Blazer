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
    public Transform topShotPoint;

    [Header("Entity Stats")]
    public StatCollectionData statTemplate;
    public StatCollection stats;

    [Header("Inventory")]
    public Inventory inventory;

    public SpriteRenderer SpriteRenderer { get; protected set; }
    public Constants.EntityFacing Facing { get; set; }
    public Animator MyAnimator { get; protected set; }
    public AbilityManager AbilityManager { get; protected set; }
    public int SessionID { get; private set; }

    protected EntityMovement movement;
    protected HealthDeathManager healthDeathManager;

    private void Awake() {
        Initialize();
    }

    void Start() {

    }

    public void Initialize() {
        SessionID = IDFactory.GenerateEntityID();
        GameManager.RegisterEntity(this);

        stats = new StatCollection();
        stats.Initialize(statTemplate);

        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        MyAnimator = GetComponentInChildren<Animator>();
        AbilityManager = GetComponent<AbilityManager>();
        inventory = GetComponent<Inventory>();
        movement = GetComponent<EntityMovement>();
        healthDeathManager = GetComponent<HealthDeathManager>();

        if (inventory != null) 
            inventory.Initialize(this);

        if(AbilityManager != null)
            AbilityManager.Initialize(this);

        if(movement != null)
            movement.Initialize();

        if (healthDeathManager != null)
            healthDeathManager.Initialize(this);


        AIBrain testBrain = GetComponent<AIBrain>();
        if(testBrain != null) {
            testBrain.Initialize();
        }


        
    }

    public void UnregisterListeners() {
        //Grid.EventManager.RemoveMyListeners(this);
        if(movement != null)
            movement.RemoveMyListeners();

        if(healthDeathManager != null)
            healthDeathManager.RemoveListeners();
        //Grid.EventManager.RemoveMyListeners(healthDeathManager);

    }




}
