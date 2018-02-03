using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnActivator : MonoBehaviour {

    public CircleCollider2D myCollider;

    // Update is called once per frame
    void Awake () {
        myCollider = GetComponent<CircleCollider2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Spawner")
        {
            if (collision.GetComponent<SpawnZone>().isActive == false && collision.GetComponent<SpawnZone>().isRevealed == true)
            {
                collision.GetComponent<SpawnZone>().isActive = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Spawner")
        {
            if (collision.GetComponent<SpawnZone>().isActive == true)
            {
                collision.GetComponent<SpawnZone>().isActive = false;
            }
        }
    }
}
