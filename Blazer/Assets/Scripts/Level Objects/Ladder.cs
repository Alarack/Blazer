using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    public float climbSpeed;
    public float descendSpeed;

    public List<string> validUserTags = new List<string>();

    private Entity currentUser;
    private float currentSpeed;
    private bool isClimbing;
    private Rigidbody2D currentBody;

    private void Update() {
        if (Input.GetAxisRaw("Vertical") >= 0){
            currentSpeed = Input.GetAxisRaw("Vertical") * climbSpeed;
        }
        else if (Input.GetAxisRaw("Vertical") <= 0){
            currentSpeed = Input.GetAxisRaw("Vertical") * descendSpeed;
        }

            if (currentUser != null && !isClimbing) {
            if(currentSpeed!= 0f) {
                isClimbing = true;
                currentBody.gravityScale = 0f;
                
            }
        }



    }


    private void FixedUpdate() {
        if(currentUser != null && isClimbing) {
            currentBody.velocity = new Vector2(currentBody.velocity.x, currentSpeed);
            //currentUser.transform.position = new Vector3(transform.position.x, currentUser.transform.position.y, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (validUserTags.Contains(other.gameObject.tag)) {
            currentUser = other.gameObject.GetComponent<Entity>();
            if (currentUser != null)
                currentBody = currentUser.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (validUserTags.Contains(other.gameObject.tag)) {
            if (currentUser != null) {
                isClimbing = false;
                currentBody.gravityScale = 1f;
                currentUser = null;
                
            }

        }

    }

}
