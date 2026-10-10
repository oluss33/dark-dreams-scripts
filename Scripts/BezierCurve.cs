using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BezierCurve
{
   // public Transform pointA, center, pointB;
   public Transform pointA, pointB, pointC, pointD;
    private float duration = 5;
    private float elapsedTime = 0;
    private float t = 0;

    public float speed = 1.5f;

    AnimationCurve curve;

    public Transform obj;

    public bool reached = false;

    public BezierCurve(Transform obj, Transform pointA, Transform pointB, Transform pointC, Transform pointD, AnimationCurve curve)
    {
        this.obj = obj;
        this.pointA = pointA;
        this.pointB = pointB;
        this.pointC = pointC;
        this.pointD = pointD;
        this.curve = curve;

    }
    
    public void QuadraticCurve()
    {

        t = elapsedTime / duration;
        elapsedTime += Time.deltaTime;

        if (t >= 1f)
        {
            t = 1f;
            reached = true;
        }

        t = curve.Evaluate(t);
        t *= speed;

        Vector3 A = Vector3.Lerp(pointA.position, pointB.position, t);
        Vector3 B = Vector3.Lerp(pointB.position, pointC.position, t);
        Vector3 C = Vector3.Lerp(pointC.position, pointD.position, t);
        
        Vector3 D = Vector3.Lerp(A, B, t);
        Vector3 E = Vector3.Lerp(B, C, t);

        Vector3 P = Vector3.Lerp(D, E, t);

        obj.position = Vector3.Lerp(obj.position, P, t);
       
    }
}
