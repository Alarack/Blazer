using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


    public static List<Transform> FindAllTargets(Vector2 starPosition, float targetRadius, LayerMask mask) {
        Collider2D[] nearTargets = Physics2D.OverlapCircleAll(starPosition, targetRadius, mask);
        List<Transform> targetPositions = new List<Transform>();

        for(int i = 0; i < nearTargets.Length; i++) {
            targetPositions.Add(nearTargets[i].transform);
        }

        return targetPositions;
    }


    public static Transform FindNearestTarget(Vector2 myPosition, List<Transform> targets) {

        if (targets == null)
            return null;

        if (targets.Count < 1)
            return null;

        float leastDistance = GetDistances(targets, myPosition).Min();

        for (int i = 0; i < targets.Count; i++) {
            if (targets[i] == null)
                continue;

            if(Vector2.Distance(targets[i].position, myPosition) <= leastDistance) {
                return targets[i];
            }

        }

        return null;
    }

    public static List<float> GetDistances(List<Transform> targets, Vector2 myPosition) {
        List<float> tempDistances = new List<float>();

        for(int i = 0; i < targets.Count; i++) {
            if (targets[i] == null)
                continue;

            float distance = Vector2.Distance(targets[i].position, myPosition);
            tempDistances.Add(distance);
        }


        return tempDistances;
    }

    public static Quaternion SmoothRotation(Vector3 targetPos, Transform myTransform, float rotateSpeed, float error = 0f) {
        Quaternion newRotation = new Quaternion();

        Vector2 direction = (targetPos - myTransform.position);

        //Debug.Log(error + " is the error");

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f + error;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        newRotation = Quaternion.Slerp(myTransform.rotation, q, Time.deltaTime * rotateSpeed);

        return newRotation;
    }





}