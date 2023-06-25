using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxCollider2Amit : MonoBehaviour
{
    GameObject gameObjectAttachedTo;

    [SerializeField] bool continues; // ? checks collision in update : checks collision on fixed update

    [SerializeField] Vector2 Offset;
    [SerializeField] Vector2 Scale;

    Vector2 size;

    Vector2 TopLeft;
    Vector2 TopRight;
    Vector2 BottomLeft;
    Vector2 BottomRight;

    private void Awake()
    {
        gameObjectAttachedTo = GetComponent<GameObject>();

    }

    private void OnValidate()
    {
        UpdateBoxColliderPoints();
        UpdateBoxColliderDebugLines();
    }

    private void Update()
    {
        if (continues)
        {
            CheckForCollisions();
        }
    }

    private void FixedUpdate()
    {
        if (!continues)
        {
            CheckForCollisions();
        }
    }

    void CheckForCollisions()
    {
        
    }

    /// <summary>
    /// Update the world locations of the bounds of the collider
    /// </summary>
    void UpdateBoxColliderPoints()
    {
        TopLeft = new Vector2(transform.position.x - (Scale.x / 2) + Offset.x, transform.position.y + Scale.y / 2 + Offset.y); 
        TopRight = new Vector2(transform.position.x + (Scale.x / 2) + Offset.x, transform.position.y + Scale.y / 2 + Offset.y); 
        BottomLeft = new Vector2(transform.position.x - (Scale.x / 2) + Offset.x, transform.position.y - Scale.y / 2 + Offset.y);
        BottomRight = new Vector2(transform.position.x + (Scale.x / 2) + Offset.x, transform.position.y - Scale.y / 2 + Offset.y);

    }

    /// <summary>
    /// Updates the debug lines
    /// </summary>
    void UpdateBoxColliderDebugLines()
    {
        Debug.DrawLine(TopLeft, TopRight, Color.green, 1f);
        Debug.DrawLine(TopRight, BottomRight, Color.green, 1f);
        Debug.DrawLine(BottomRight, BottomLeft, Color.green, 1f);
        Debug.DrawLine(BottomLeft, TopLeft, Color.green, 1f);
    }

}
