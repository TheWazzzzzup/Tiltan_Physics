using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityAddition : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    [ContextMenu("VelcityAdd")]
    public void AddVel()
    {
        rb.velocity = Vector2.right * 2;
    }

    [ContextMenu("StopVel")]
    public void StopVel()
    {
        rb.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
