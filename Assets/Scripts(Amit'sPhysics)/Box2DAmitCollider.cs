using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box2DAmitCollider : MonoBehaviour
{
    // Public
    public Vector2[] Verts => UpdateColliderVerts();    // The verts of the box
    public Vector2 Size;                                // The size of the box collider
    public Vector2 Offset;                              // The offest of the box collider

    public bool AutoScale;                              // Scales automatical accordingly to the transform sacle
    public bool ValidateRefresh;                        // Makes the debug refresh

    // Private
    Vector2 scale;

    int vertIndex = 0;


    void OnValidate()
    {
        UpdateColliderVerts();
        UpdateBoxColliderDebugLines();
    }

    /// <summary>
    /// Draws the box collider using debug draw line 
    /// </summary>
    void UpdateBoxColliderDebugLines()
    {
        Debug.DrawLine(Verts[0], Verts[1], Color.green, 1f);
        Debug.DrawLine(Verts[1], Verts[2], Color.green, 1f);
        Debug.DrawLine(Verts[2], Verts[3], Color.green, 1f);
        Debug.DrawLine(Verts[3], Verts[0], Color.green, 1f);
    }

    /// <summary>
    /// In charge of updaing the verts of on a box collider
    /// </summary>
    /// <returns></returns>
    Vector2[] UpdateColliderVerts()
    {
        Vector2[] newVerts = new Vector2[4]; 

        if (AutoScale)
        {
            scale = transform.localScale;
            Size = scale;
        }
        else scale = Size;

        vertIndex = 0;
        newVerts[vertIndex] = new Vector2(transform.position.x - (scale.x / 2) + Offset.x, transform.position.y + scale.y / 2 + Offset.y);
        vertIndex++;
        newVerts[vertIndex] = new Vector2(transform.position.x + (scale.x / 2) + Offset.x, transform.position.y + scale.y / 2 + Offset.y);
        vertIndex++;
        newVerts[vertIndex] = new Vector2(transform.position.x + (scale.x / 2) + Offset.x, transform.position.y - scale.y / 2 + Offset.y);
        vertIndex++;
        newVerts[vertIndex] = new Vector2(transform.position.x - (scale.x / 2) + Offset.x, transform.position.y - scale.y / 2 + Offset.y);
        vertIndex++;
        return newVerts;
    }
}
