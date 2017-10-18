using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity : MonoBehaviour {
    [Header("Basic Info")]
    public string entityName;
    //public EntityData entityData;

    public Transform leftShotOrigin;
    public Transform rightShotOrigin;

    [Header("Entity Stats")]
    public StatCollectionData statTemplate;
    public StatCollection stats;


    public SpriteRenderer SpriteRenderer { get; protected set; }
    public Constants.EntityFacing Facing { get; set; }

    protected AbilityManager abilityManager;
    protected EntityMovement movement;

    void Start() {
        Initialize();
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            Debug.Log(stats.GetStatCurrentValue(Constants.EntityStat.Health));
        }

    }


    public void Initialize() {
        stats = new StatCollection();
        stats.Initialize(this, statTemplate);
        //this.entityData = entityData;
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        abilityManager = GetComponent<AbilityManager>();
        abilityManager.Initialize(this);
        movement = GetComponent<EntityMovement>();
        movement.Initialize();



    }


    public void SetUpEntity() {



    }




}
