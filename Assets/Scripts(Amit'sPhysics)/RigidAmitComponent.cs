using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class RigidAmitComponent : MonoBehaviour
{
    // Public
    public Circle2DAmitCollider circleCollider;
    public Box2DAmitCollider boxCollider;
    
    public UnityEvent<RigidAmitComponent> TriggerEvent;             // The event that triggers if the rigidbody is taged as trigger
    
    public ShapeType shapeType;                                     // The shape type of the body
    
    public bool isStatic = false;                                   // Is the body static
    public bool isTrigger = false;                                  // Is the rigidbody is a trigger

    // Serializefield
    [SerializeField] Vector2 Velocity;                              // the current velocity of the rigidamit component

    [SerializeField] float Drag;                                    // User represented drag

    // Private
    Transform transformOnLastUpdate;                                // The last location of the rigidamit component in the space

    Vector2 calculationsVector;                                     // Vector2 to use in calculation

    float calculatedDrag => Drag/100;


    #region Unity Methods
    
    private void Awake()
    {
        // the inital location of the object
         transformOnLastUpdate = transform;
    }

    private void FixedUpdate()
    {
        // This Increase the drag when the velocity magnitued is really low, to fake a non liner change
        if (Mathf.Abs(Velocity.magnitude) <= 0.1f) 
        {
            calculationsVector = Velocity * (calculatedDrag * 5);
        }

        // The regular velocity calculations
        else
        {
            calculationsVector = Velocity * calculatedDrag;
        }

        transform.position = transformOnLastUpdate.position + (Vector3)(Velocity - calculationsVector);
        Velocity -= calculationsVector;

        // Stops the object under a certin threshold to fake non liner change in speed 
        if (Mathf.Abs(Velocity.magnitude) <= 0.001f)
        {
            Velocity = Vector2.zero;
        }

        transformOnLastUpdate.position = transform.position;
    }
    
    #endregion

    #region Methods

    // Gets the velocity of the object (made it that way to keep the change possible from the inspector)
    public Vector2 GetVelocity() => Velocity; 

    // Changes the velocity of the object (made it that way to keep the change possbile from the inspector)
    public void AddVelocity(Vector2 velocity)
    {
        this.Velocity = velocity;
    }
    
    #endregion

}
