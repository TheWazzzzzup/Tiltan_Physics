using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityAddition : MonoBehaviour
{
    Rigidbody2D rb;

    bool changeVel = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (changeVel)
        {
            AddVel();
        }
    }

    [ContextMenu("Change")]
    public void ChangeVel()
    {
        changeVel = true;
    }

    public void AddVel()
    {
        rb.velocity = Vector2.right * 7.5f;
        changeVel = false;
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
