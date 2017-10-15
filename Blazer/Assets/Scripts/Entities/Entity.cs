using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity : MonoBehaviour {

    public string entityName;
    //public EntityData entityData;

    public Transform leftShotOrigin;
    public Transform rightShotOrigin;

    public AbilityManager abilityManager;


    public SpriteRenderer SpriteRenderer { get; protected set; }
    public Constants.EntityFacing Facing { get; set; }

    void Start() {
        Initialize();
    }

    public void Initialize() {
        //this.entityData = entityData;
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        abilityManager = GetComponent<AbilityManager>();
        abilityManager.Initialize(this);

    }


    public void SetUpEntity() {



    }




}
