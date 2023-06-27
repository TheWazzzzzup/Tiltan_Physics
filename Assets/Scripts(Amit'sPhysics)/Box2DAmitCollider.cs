using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box2DAmitCollider : MonoBehaviour
{

    public Vector2 Size;
    public Vector2 Offset;

    public bool AutoScale;

    public bool validateRefresh;



    Vector2 scale;
    
    public Vector2[] verts { get; protected set; } = new Vector2[4];

    int vertIndex = 0;


    private void OnValidate()
    {
        UpdateColliderVerts();
        UpdateBoxColliderDebugLines();
    }

    void UpdateBoxColliderDebugLines()
    {
        Debug.DrawLine(verts[0], verts[1], Color.green, 1f);
        Debug.DrawLine(verts[1], verts[2], Color.green, 1f);
        Debug.DrawLine(verts[2], verts[3], Color.green, 1f);
        Debug.DrawLine(verts[3], verts[0], Color.green, 1f);
    }

    void UpdateColliderVerts()
    {
        if (AutoScale)
        {
            scale = transform.localScale;
            Size = scale;
        }
        else scale = Size;
        vertIndex = 0;
        verts[vertIndex] = new Vector2(transform.position.x - (scale.x / 2) + Offset.x, transform.position.y + scale.y / 2 + Offset.y);
        vertIndex++;
        verts[vertIndex] = new Vector2(transform.position.x + (scale.x / 2) + Offset.x, transform.position.y + scale.y / 2 + Offset.y);
        vertIndex++;
        verts[vertIndex] = new Vector2(transform.position.x + (scale.x / 2) + Offset.x, transform.position.y - scale.y / 2 + Offset.y);
        vertIndex++;
        verts[vertIndex] = new Vector2(transform.position.x - (scale.x / 2) + Offset.x, transform.position.y - scale.y / 2 + Offset.y);
        vertIndex++;
    }
}
