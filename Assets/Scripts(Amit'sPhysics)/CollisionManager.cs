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
                    out Vector2 normal, out Vector2 passedA, out Vector2 passedB))
                {
                    bodyA.AddVelocity(passedA);
                    bodyB.AddVelocity(passedB);
                }
            }
        }
    }

    public static bool IntersectCircle(Vector2 centerA, float radiusA, Vector2 veloA,
        Vector2 centerB, float radiusB, Vector2 veloB, out Vector2 normal, out Vector2 passVeloA, out Vector2 passVeloB)
    {
        normal = Vector2.zero;
        passVeloA = Vector2.zero;
        passVeloB = Vector2.zero;

        float distance = Vector2.Distance(centerA, centerB);
        float radii = radiusA + radiusB;

        if (distance >= radii)
        {
            return false;
        }

        normal = Normalize(centerB - centerA);
        passVeloA = normal * veloB.magnitude; 
        passVeloB = normal * veloA.magnitude; 

        return true;
    }

    public static Vector2 Normalize(Vector2 v)
    {
        float mag = v.magnitude;
        return new Vector2(v.x / mag, v.y / mag);

    }
}
