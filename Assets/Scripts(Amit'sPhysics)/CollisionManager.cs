using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public List <RigidAmitComponent> bodies;

    private void FixedUpdate()
    {
        for (int i = 0; i < bodies.Count - 1; i++)
        {
            RigidAmitComponent bodyA = bodies[i];
            for (int j = i + 1; j < bodies.Count; j++)
            {
                RigidAmitComponent bodyB = bodies[j];

                if (IntersectCircle(bodyA.transform.position, bodyA.rbCollider.Radius, bodyA.GetVelocity(),
                    bodyB.transform.position, bodyB.rbCollider.Radius, bodyB.GetVelocity(),
                    out Vector2 normal, out float depth, out Vector2 passedA, out Vector2 passedB))
                {
                    bodyA.AddVelocity(-normal * depth / 2);
                    bodyB.AddVelocity(normal * depth / 2);
                    bodyA.AddVelocity(passedA);
                    bodyB.AddVelocity(passedB);
                }
            }
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
