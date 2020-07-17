using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyUniverse
{
    public static Vector2 CalculateGravity(Vector2 body1Pos, float body1Mass, Vector2 body2Pos, float body2Mass) {
        
        Vector2 difference = body1Pos - body2Pos;
        Vector2 direction = difference.normalized;
        float distance = difference.magnitude;

        float gravityForce = (body1Mass * body2Mass) / (distance * distance);


        Vector2 acceleration = direction * gravityForce;

        return acceleration;
    }
}
