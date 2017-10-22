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

    protected AbilityManager abilityManager;
    protected EntityMovement movement;

    void Start() {
        Initialize();
    }

    public void Initialize() {
        stats = new StatCollection();
        stats.Initialize(statTemplate);
        //this.entityData = entityData;
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        abilityManager = GetComponent<AbilityManager>();
        abilityManager.Initialize(this);
        movement = GetComponent<EntityMovement>();
        movement.Initialize();
        GameManager.RegisterEntity(this);


    }




}
