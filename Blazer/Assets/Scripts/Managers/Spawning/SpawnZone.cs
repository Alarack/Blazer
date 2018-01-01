using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour {

    public BoxCollider2D zoneCollider;
    public int numSpawns;

    private void Awake() {
        zoneCollider = GetComponent<BoxCollider2D>();
    }


    private void Update() {

    }


    public Vector2 GetSpawnLocation() {
        Vector2 result = Vector2.zero;

        float xCord = zoneCollider.bounds.extents.x;
        //float yCord = zoneCollider.bounds.extents.y;

        float randomX = Random.Range(-xCord, xCord);
        //float randomY = Random.Range(-yCord, yCord);

        result = new Vector2(randomX, 0/*yCord*/);

        result += (Vector2)transform.position;

        return result;
    }

}
