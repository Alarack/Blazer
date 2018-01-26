using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {



    public List<string> validUserTags = new List<string>();



    private void Update() {


    }





    private void FixedUpdate() {

    }

    private void OnTriggerStay2D(Collider2D other) {
        if (validUserTags.Contains(other.gameObject.tag)) {
            other.gameObject.GetComponent<EntityMovement>().canClimb = true;
            other.gameObject.GetComponent<EntityMovement>().currentLadder = this.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (validUserTags.Contains(other.gameObject.tag)) {

            other.gameObject.GetComponent<EntityMovement>().ClimbEnd();
            other.gameObject.GetComponent<EntityMovement>().canClimb = false;
            other.gameObject.GetComponent<EntityMovement>().currentLadder = null;
            other.gameObject.GetComponent<EntityMovement>().ClimbEnd();
        }
    }

}
