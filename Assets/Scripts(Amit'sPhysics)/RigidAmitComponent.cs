using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidAmitComponent : MonoBehaviour
{
    // The last location of the rigidamit component in the space
    Transform positionOnLastUpdate;
 
    // the current velocity of the rigidamit component
    [SerializeField] Vector2 Velocity;

    private void Awake()
    {
        // the inital location of the object
         positionOnLastUpdate = transform;
    }

    private void FixedUpdate()
    {
        transform.position = positionOnLastUpdate.position + (Vector3)Velocity;
    }
}
