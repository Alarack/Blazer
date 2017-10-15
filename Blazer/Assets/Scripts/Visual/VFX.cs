using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour {

    public float duration;


	void Start () {
		if(duration > 0f) {
            Destroy(gameObject, duration);
        }
	}
	

	void Update () {
		
	}
}
