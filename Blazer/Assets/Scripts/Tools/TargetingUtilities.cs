using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TargetingUtilities {



    public static Quaternion CalculateImpactRotation(Vector2 rayDir) {
        float angle = Mathf.Atan2(-rayDir.x, rayDir.y) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector2.right);

        return rot;
    }




    //public static Vector2 RadianToVector2(float radian) {
    //    return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    //}

    //public static Vector2 DegreeToVector2(float degree) {
    //    return RadianToVector2(degree * Mathf.Rad2Deg);
    //}

    public static Vector2 DegreeToVector2(float degree) {
        return (Vector2)(Quaternion.Euler(0f, 0f, degree) * Vector2.right);
    }





}