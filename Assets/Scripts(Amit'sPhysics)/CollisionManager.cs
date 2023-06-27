using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private const float minimunMagBounce = .08f;
    public List <RigidAmitComponent> bodies;

    private void FixedUpdate()
    {
        for (int i = 0; i < bodies.Count - 1; i++)
        {
            RigidAmitComponent bodyA = bodies[i];
            for (int j = i + 1; j < bodies.Count; j++)
            {
                RigidAmitComponent bodyB = bodies[j];

                if (IntersectCircle(bodyA.transform.position, bodyA.circleCollider.Radius, bodyA.GetVelocity(),
                    bodyB.transform.position, bodyB.circleCollider.Radius, bodyB.GetVelocity(),
                    out Vector2 normal, out float depth, out Vector2 passedA, out Vector2 passedB))
                {
                    bodyA.AddVelocity(-normal * depth / 2);
                    bodyB.AddVelocity(normal * depth / 2);
                    float mag = passedA.magnitude;
                    if (mag <= 0f) mag = minimunMagBounce * passedB.magnitude;
                    bodyA.AddVelocity(-normal * mag);
                    mag = passedB.magnitude;
                    if (mag <= 0f) mag = minimunMagBounce * passedA.magnitude;
                    bodyB.AddVelocity(normal * mag) ;
                }
            }
        }
    }

    public static bool IntersectPolygons(Vector2[] vertsA, Vector2[] vertsB)
    {
        for (int i = 0; i < vertsA.Length; i++)
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

        }

        for (int i = 0; i < vertsB.Length; i++)
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

        }

        return true;
    }

    static void ProjectVerts(Vector2[] verts, Vector2 seperationAxis, out float min, out float max)
    {
        min = float.MaxValue;
        max = float.MinValue;

        for (int i = 0; verts.Length > 0; i++)
        {
            Vector2 v = verts[i];
            float projection = Vector2.Dot(v, seperationAxis);

            if (projection < min) min = projection;
            if (projection > max) max = projection;
        }


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

    public static Vector2 Normalize(Vector2 v)
    {
        float mag = v.magnitude;
        return new Vector2(v.x / mag, v.y / mag);

    }
}
