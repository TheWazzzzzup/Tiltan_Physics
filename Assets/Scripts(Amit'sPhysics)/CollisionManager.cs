using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CollisionManager : MonoBehaviour
{
    private const float minimunMagBounce = .1f;
    public List <RigidAmitComponent> bodies;

    private void FixedUpdate()
    {
        for (int i = 0; i < bodies.Count - 1; i++)
        {
            RigidAmitComponent bodyA = bodies[i];
            for (int j = i + 1; j < bodies.Count; j++)
            {
                RigidAmitComponent bodyB = bodies[j];
                float deep;
                Vector2 passedA, passedB, normal;

                // Checks what happens if two colliders are of type circle
                if (bodyA.shapeType == ShapeType.Circle && bodyB.shapeType == ShapeType.Circle)
                {
                    if (IntersectCircle(bodyA.transform.position, bodyA.circleCollider.Radius, bodyA.GetVelocity(),
                        bodyB.transform.position, bodyB.circleCollider.Radius, bodyB.GetVelocity(),
                        out normal, out deep, out  passedA, out  passedB))
                    {
                        CalculateResulotion(bodyA, bodyB, deep, passedA, passedB, normal);
                    }
                }

                // check what happens if two colliders are of type box
                if (bodyA.shapeType == ShapeType.Box && bodyB.shapeType == ShapeType.Box)
                {
                    // 2 Box collision
                    if (IntersectionBoxes(bodyA.boxCollider.verts, bodyB.boxCollider.verts, bodyA.GetVelocity(), bodyB.GetVelocity()
                        , out  normal, out deep, out  passedA, out  passedB))
                    {
                        CalculateResulotion(bodyA, bodyB, deep, passedA, passedB, normal);
                    }
                }

                if (bodyB.shapeType == ShapeType.Box && bodyA.shapeType == ShapeType.Circle)
                {
                    if (IntersectBoxCircle(bodyA.transform.position, bodyA.circleCollider.Radius, bodyB.boxCollider.verts, bodyA.GetVelocity(),
                        bodyB.GetVelocity(), out normal, out deep, out passedA, out passedB))
                    {
                        CalculateResulotion(bodyA, bodyB, deep, passedA, passedB, normal);
                    }
                }

                else if (bodyA.shapeType == ShapeType.Box && bodyB.shapeType == ShapeType.Circle)
                {
                    if (IntersectBoxCircle(bodyB.transform.position, bodyB.circleCollider.Radius, bodyA.boxCollider.verts, bodyA.GetVelocity(),
                       bodyB.GetVelocity(), out normal, out deep, out passedA, out passedB))
                    {
                        CalculateResulotion(bodyA, bodyB, deep, passedA, passedB, normal);
                    }
                }

            }
        }
    }



    #region Calculators
    public static bool IntersectionBoxes(Vector2[] vertsA, Vector2[] vertsB, Vector2 veloA, Vector2 veloB, out Vector2 normal, out float depth,
        out Vector2 passedA, out Vector2 passedB)
    {
        normal = Vector2.zero;
        depth = float.MaxValue;
        passedA = Vector2.zero;
        passedB = Vector2.zero ;

        for (int i = 0; i < vertsA.Length/2; i++)
        {
            Vector2 va = vertsA[i];
            Vector2 vb = vertsA[(i + 1) % vertsA.Length];

            Vector2 edge = vb - va;
            Vector2 seperationAxis = new Vector2(-edge.y, edge.x);

            ProjectVerts(vertsA, seperationAxis, out float minA, out float maxA);
            ProjectVerts(vertsB, seperationAxis, out float minB, out float maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            float axidDepth = Mathf.Min(maxB - minA, maxA - minB);
            if (axidDepth < depth) { depth = axidDepth; normal = seperationAxis; }

        }

        for (int i = 0; i < vertsB.Length/2; i++)
        {
            Vector2 va = vertsB[i];
            Vector2 vb = vertsB[(i + 1) % vertsB.Length];

            Vector2 edge = vb - va;
            Vector2 seperationAxis = new Vector2(-edge.y, edge.x);

            ProjectVerts(vertsA, seperationAxis, out float minA, out float maxA);
            ProjectVerts(vertsB, seperationAxis, out float minB, out float maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            float axidDepth = Mathf.Min(maxB - minA, maxA - minB);
            if (axidDepth < depth) { depth = axidDepth; normal = seperationAxis; }
        }

        passedA = veloB;
        passedB = veloA;

        depth /= normal.magnitude;
        normal = Normalize(normal);

        Vector2 centerA = GeomarticCenter(vertsA);
        Vector2 centerB = GeomarticCenter(vertsB);

        Vector2 direction = centerB - centerA;

        if (Vector2.Dot(direction, normal) < 0f) normal = -normal;

        return true;
    }

    public static bool IntersectCircle(Vector2 centerA, float radiusA, Vector2 veloA,
        Vector2 centerB, float radiusB, Vector2 veloB, out Vector2 normal, out float strength, out Vector2 passedA, out Vector2 passedB)
    {
        normal = Vector2.zero;
        strength = 0f;
        passedA = Vector2.zero;
        passedB = Vector2.zero;

        float distance = Vector2.Distance(centerA, centerB);
        float radii = radiusA + radiusB;

        if (distance >= radii)
        {
            return false;
        }

        normal = centerB - centerA;
        strength = radii - distance;
        passedA = veloB;
        passedB = veloA;

        return true;
    }

    public static bool IntersectBoxCircle(Vector2 cricleCenter, float circleRadius, Vector2[] verts,
        Vector2 veloA, Vector2 veloB, out Vector2 normal, out float depth, out Vector2 passedA, out Vector2 passedB )
    {
        normal = Vector2.zero;
        passedA = Vector2.zero;
        passedB = Vector2.zero;
        depth = float.MaxValue;

        float axisDepth = 0f;
        float minA, maxA, minB, maxB;
        for (int i = 0; i < verts.Length / 2; i++)
        {
            Vector2 va = verts[i];
            Vector2 vb = verts[(i + 1) % verts.Length];

            Vector2 edge = vb - va;
            Vector2 seperationAxis = new Vector2(-edge.y, edge.x);

            ProjectVerts(verts, seperationAxis, out  minA, out  maxA);
            ProjectCircleToAxis(seperationAxis, cricleCenter, circleRadius, out  minB, out  maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            axisDepth = Mathf.Min(maxB - minA, maxA - minB);
            if (axisDepth < depth) { depth = axisDepth; normal = seperationAxis; }

        }

        int cpIndex = FindClosestPointOnPolygon(cricleCenter, verts);
        Vector2 cp = verts[cpIndex];

        Vector2 axis = cp - cricleCenter;

        ProjectVerts(verts, axis, out  minA, out  maxA);
        ProjectCircleToAxis(axis, cricleCenter, circleRadius, out  minB, out  maxB);

        if (minA >= maxB || minB >= maxA)
        {
            return false;
        }

        axisDepth = Mathf.Min(maxB - minA, maxA - minB);
        if (axisDepth < depth) { depth = axisDepth; normal = axis; }

        passedA = veloB;
        passedB = veloA;

        depth /= normal.magnitude;
        normal = Normalize(normal);

        Vector2 polyCenter = GeomarticCenter(verts);

        Vector2 direction = polyCenter - cricleCenter;

        if (Vector2.Dot(direction, normal) < 0f) normal = -normal;

        return true;
    }

    
    static void CalculateResulotion(RigidAmitComponent bodyA, RigidAmitComponent bodyB, float deep, Vector2 passedA, Vector2 passedB, Vector2 normal)
    {
        if (bodyA.isTrigger || bodyB.isTrigger)
        {
            // Create a onTriggerEnterEvent !
            bodyA.TriggerEvent.Invoke(bodyB);
            bodyB.TriggerEvent.Invoke(bodyA);
            return;
        }

        if (bodyA.isStatic && !bodyB.isStatic)
        {
            bodyB.AddVelocity(normal * deep); 
            float mag = passedA.magnitude;
            if (mag <= 0f) mag = minimunMagBounce * passedA.magnitude;

            Vector2 newNormal = StaticInteractoionNormal(normal,passedA);

            bodyB.AddVelocity(newNormal);
        }

        if (!bodyA.isStatic && bodyB.isStatic)
        {
            bodyA.AddVelocity(normal * deep);
            float mag = passedB.magnitude;
            if (mag <= 0f) mag = minimunMagBounce * passedB.magnitude;

            Vector2 newNormal = StaticInteractoionNormal(normal,passedB);

            bodyA.AddVelocity(newNormal);
        }

        else if (!bodyA.isStatic && !bodyB.isStatic)
        {
            bodyA.AddVelocity(-normal * deep / 2);
            bodyB.AddVelocity(normal * deep / 2);
            float mag = passedA.magnitude;
            if (mag <= 0f) mag = minimunMagBounce * passedB.magnitude;
            bodyA.AddVelocity(-normal * mag);
            mag = passedB.magnitude;
            if (mag <= 0f) mag = minimunMagBounce * passedA.magnitude;
            bodyB.AddVelocity(normal * mag);
        }
    }

    public static Vector2 Normalize(Vector2 v)
    {
        float mag = v.magnitude;
        return new Vector2(v.x / mag, v.y / mag);

    }

    static Vector2 StaticInteractoionNormal(Vector2 normal,Vector2 velo)
    {
        Vector2 value = Vector2.zero;

        if (normal.x > 0f && normal.y > 0f) { Debug.LogWarning($"normal is on an angle in method {nameof(StaticInteractoionNormal)}"); }

        if (Mathf.Abs(normal.x) > 0f)
        {
            value = new Vector2(-velo.x, velo.y);
        }

        else if (Mathf.Abs(normal.y) > 0f)
        {
            value = new Vector2(velo.x, -velo.y);
        }

        return value;
    }

    static int FindClosestPointOnPolygon(Vector2 circleCenter, Vector2[] verts)
    {
        int result = -1;
        float minDistance = float.MaxValue;

        for (int i = 0; i < verts.Length; i++)
        {
            Vector2 v = verts[i];
            float distance = Vector2.Distance(v, circleCenter);

            if (distance < minDistance) { minDistance =  distance; result = i; }
        }

        return result;
    }

    static void ProjectCircleToAxis(Vector2 axis ,Vector2 pos, float radius, out float min, out float max)
    {
        min = float.MaxValue;
        max = float.MinValue;
        Vector2 direction = Normalize(axis);
        Vector2 directionAndRadius = direction * radius;

        Vector2 p1 = pos + directionAndRadius;
        Vector2 p2 = pos - directionAndRadius;

        min = Vector2.Dot(axis, p1);
        max = Vector2.Dot(axis, p2);

        if (min > max)
        {
            float t = min;
            min = max;
            max = t;
        }

    }

    static void ProjectVerts(Vector2[] verts, Vector2 seperationAxis, out float min, out float max)
    {
        min = float.MaxValue;
        max = float.MinValue;

        for (int i = 0; i < verts.Length; i++)
        {
            Vector2 v = verts[i];
            float projection = Vector2.Dot(v, seperationAxis);

            if (projection < min) min = projection;
            if (projection > max) max = projection;
        }
    }
    
    static Vector2 GeomarticCenter(Vector2[] verts)
    {
        float sumX = 0f;
        float sumY = 0f;

        for (int i = 0 ; i < verts.Length; i++)
        {
            Vector2 v = verts[i];
            sumX += v.x;
            sumY += v.y;
        }

        return new Vector2(sumX / (float)verts.Length, sumY / (float)verts.Length); 
    }

    #endregion
}
