using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectManager : MonoBehaviour {

    public static VisualEffectManager visualEffectManager;



    public static GameObject CreateVisualEffect(GameObject prefab, Vector3 location, Quaternion rotation) {

        return Instantiate(prefab, location, rotation) as GameObject;

    }

}
