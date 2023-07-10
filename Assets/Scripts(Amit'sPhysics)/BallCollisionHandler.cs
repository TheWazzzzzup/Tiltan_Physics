using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RigidAmitComponent))]
public class BallCollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RigidAmitComponent rigidAmit = GetComponent<RigidAmitComponent>();
        rigidAmit.TriggerEvent.AddListener(CustomTriggerEnter);
    }

    void CustomTriggerEnter(RigidAmitComponent ra)
    {
        Debug.Log(transform.position + " " + gameObject.name);
    }
}
