﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TargetingUtilities {



    public static Quaternion CalculateImpactRotation(Vector2 rayDir) {

        //Vector2 rayDir = Vector2.Reflect((hitPoint - shotOrigin).normalized, hitNormal);


        float angle = Mathf.Atan2(-rayDir.x, rayDir.y) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector2.right);

        return rot;
    }


}

