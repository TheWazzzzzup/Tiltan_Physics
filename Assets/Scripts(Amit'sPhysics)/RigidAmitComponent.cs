using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RigidAmitComponent : MonoBehaviour
{
    // The last location of the rigidamit component in the space
    Transform positionOnLastUpdate;
 
    // the current velocity of the rigidamit component
    [SerializeField] Vector2 Velocity;

    [SerializeField] float Drag;

    Vector2 calculationsVector;
    
    float calculatedDrag => Drag/100;

    private void Awake()
    {
        // the inital location of the object
         positionOnLastUpdate = transform;
    }

    // make this much more flexable !
    private void FixedUpdate()
    {
       // TODO: Fix the stop bounce this method creates // When close to a stop, this make the object come to a stop faster
        if (Mathf.Abs(Velocity.magnitude) <= 0.1f)
        {
            calculationsVector = Velocity * (calculatedDrag * 5);
        }

        // This is a thershold that will stop the object from moving if reached

        else
        {
            calculationsVector = Velocity * calculatedDrag;
        }

        transform.position = positionOnLastUpdate.position + (Vector3)(Velocity - calculationsVector);
        Velocity -= calculationsVector;

        if (Mathf.Abs(Velocity.magnitude) <= 0.001f)
        {
            Velocity = Vector2.zero;
        }

        positionOnLastUpdate.position = transform.position;
    }
}
