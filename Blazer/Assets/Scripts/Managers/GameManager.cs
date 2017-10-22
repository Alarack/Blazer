using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    public StatCollectionData defaultStats;
    public StatCollectionData defaultProjectileStats;
    private static List<Entity> allEntities = new List<Entity>();

    private void Awake() {

        if (gameManager == null)
            gameManager = this;
        else {
            Destroy(gameObject);
        }
        
    }


    public static StatCollectionData GetDefaultStatCollection() {
        return gameManager.defaultStats;
    }

    public static StatCollectionData GetDefaultProjectileStats() {
        return gameManager.defaultProjectileStats;
    }

    public static void RegisterEntity(Entity target) {
        if (!allEntities.Contains(target))
            allEntities.Add(target);
        else {
            Debug.LogError(target.entityName + " is already registered");
        }
    }

    public static void UnregisterEntity(Entity target) {
        if (allEntities.Contains(target))
            allEntities.Remove(target);
        else {
            Debug.LogError(target.entityName + " is not registered");
        }
    }


}
