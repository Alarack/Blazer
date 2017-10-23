using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectManager : MonoBehaviour {

    public static VisualEffectManager visualEffectManager;



    public static GameObject CreateVisualEffect(GameObject prefab, Vector3 location, Quaternion rotation) {

        return Instantiate(prefab, location, rotation) as GameObject;

    }



    public static void SetParticleEffectLayer(GameObject hitEffect, GameObject target) {


        ParticleSystem[] ps = hitEffect.GetComponentsInChildren<ParticleSystem>();
        SpriteRenderer hitSprite =target.GetComponentInChildren<SpriteRenderer>();

        for (int i = 0; i < ps.Length; i++) {
            ps[i].GetComponent<ParticleSystemRenderer>().sortingOrder = hitSprite.sortingOrder;
        }

    }

}
