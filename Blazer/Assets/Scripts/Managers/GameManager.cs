using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    public ItemPools itemPools;
    public SpawnManager spawnManager;
    public StatCollectionData defaultStats;
    public StatCollectionData defaultProjectileStats;
    public GameDifficulty gameDifficulty;

    public static bool GamePaused { get; set; }

    private static List<Entity> allEntities = new List<Entity>();

    private void Awake() {

        if (gameManager == null)
            gameManager = this;
        else {
            Destroy(gameObject);
        }
        
    }

    private void Start() {
        if (spawnManager != null)
            spawnManager.Initialize();

        if (gameDifficulty != null)
            gameDifficulty.Initialize();
    }

    private void Update() {
        if (gameDifficulty != null)
            gameDifficulty.ManagedUpdate();
    }


    public static StatCollectionData GetDefaultStatCollection() {
        return gameManager.defaultStats;
    }

    public static StatCollectionData GetDefaultProjectileStats() {
        return gameManager.defaultProjectileStats;
    }

    public static ItemPools GetItemPools() {
        return gameManager.itemPools;
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

    public static Entity GetEntityByID(int id) {
        int count = allEntities.Count;

        for(int i = 0; i < count; i++) {
            if(allEntities[i].SessionID == id) {
                return allEntities[i];
            }
        }

        return null;
    }

    public static List<DifficultyData.DifficultyEntry> GetDifficultyEntries() {
        return gameManager.gameDifficulty.difficultyData.difficultyEntries;
    }


}
