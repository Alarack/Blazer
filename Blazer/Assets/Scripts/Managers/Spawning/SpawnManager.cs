using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [Header("Spawn Info")]
    public float spawnInterval;
    public int maxSpawn;
    [Header("Spawn Points")]
    //public List<Transform> spawnPoints = new List<Transform>();
    public SpawnZone spawnZone; // There will be multiple zones later. this is just for the demo.
    [Header("Spawns")]
    public List<GameObject> spawns = new List<GameObject>();

    private Timer spawnTimer;
    private List<Entity> currentSpawns = new List<Entity>();
    private int spawnCount;

    public void Initialize() {
        spawnTimer = new Timer("Spawn Timer", spawnInterval, true, Spawn);

        Grid.EventManager.RegisterListener(Constants.GameEvent.EntityDied, OnEntityDeath);
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

    private void Spawn() {

        if (spawnCount >= maxSpawn)
            return;

        int randomSpawnIndex = Random.Range(0, spawns.Count);
        //int randomLocIndex = Random.Range(0, spawnPoints.Count);

        GameObject activeSpawn = Instantiate(spawns[randomSpawnIndex], spawnZone.GetSpawnLocation(), Quaternion.identity) as GameObject;

        Entity activeEntity = activeSpawn.GetComponent<Entity>();

        currentSpawns.Add(activeEntity);
        spawnCount++;

    }

}
