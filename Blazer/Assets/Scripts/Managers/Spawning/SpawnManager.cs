using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [Header("Spawn Info")]
    public float spawnInterval;
    public int maxSpawn;
    [Header("Spawn Points")]
    //public List<Transform> spawnPoints = new List<Transform>();

    /*--I'm implementing multiple spawn zones now so they can be used in level creation in the interim--*/
    //public SpawnZone spawnZone; // There will be multiple zones later. this is just for the demo.
    public List<SpawnZone> spawnZones;
    [Header("Spawns")]
    public List<GameObject> spawns = new List<GameObject>();

    private Timer spawnTimer;
    private List<Entity> currentSpawns = new List<Entity>();
    private int spawnCount;

    public void Initialize() {
        spawnTimer = new Timer("Spawn Timer", spawnInterval, true, Spawn);

        Grid.EventManager.RegisterListener(Constants.GameEvent.EntityDied, OnEntityDeath);
        Grid.EventManager.RegisterListener(Constants.GameEvent.DifficultyChange, OnDifficultyChange);
    }

    private void Update() {
        if (spawnTimer != null)
            spawnTimer.UpdateClock();
    }


    private void OnEntityDeath(EventData data) {
        Entity target = data.GetMonoBehaviour("Target") as Entity;

        if (!currentSpawns.Contains(target))
            return;

        currentSpawns.Remove(target);
        spawnCount--;
        

    }

    private void OnDifficultyChange(EventData data) {
        GameDifficulty.DifficultyLevel recievedDifficulty = (GameDifficulty.DifficultyLevel)data.GetInt("DifficultyValue");

        float spawnRateIncrease = GameManager.gameManager.gameDifficulty.GetDifficultySpawnValue();

        spawnTimer.ModifyDuration(-spawnRateIncrease);
        Debug.Log(spawnTimer.Duration + " is the new Spawn Interval");



    }

    private void Spawn() {

        if (spawnCount >= maxSpawn)
            return;

        int randomSpawnIndex = Random.Range(0, spawns.Count);
        int randomLocIndex = Random.Range(0, spawnZones.Count);

        GameObject activeSpawn = Instantiate(spawns[randomSpawnIndex], spawnZones[randomLocIndex].GetSpawnLocation(), Quaternion.identity) as GameObject;
        Entity activeEntity = activeSpawn.GetComponent<Entity>();

        currentSpawns.Add(activeEntity);
        spawnCount++;

    }

}
