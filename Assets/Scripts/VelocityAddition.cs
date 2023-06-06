using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class VelocityAddition : MonoBehaviour
{

    // Public


    // Serialized
    [SerializeField] GameObject stickPrefab;


    // Private
    Rigidbody2D rb2d;
    StickPlacer stickPlacer;

    Vector2 mouseLocToWorld;

    Vector3 pullAnchorPos;

    float ballToStickAngle;
    float velocityMultiply = 30f;

    bool changeVel = false;

    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StickInit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) { ChangeVel();  }

        mouseLocToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            stickPlacer.StrikeInitated(true);
            pullAnchorPos = mouseLocToWorld;
        }  

        if (Input.GetMouseButtonUp(0) && stickPlacer.CurrentStatus == StickStatus.Striking) {
            float dist = Vector3.Distance(pullAnchorPos, mouseLocToWorld);
            velocityMultiply *= dist;
            ChangeVel();

            

        }

        if (stickPlacer.CurrentStatus == StickStatus.Aiming) {
            ballToStickAngle = Angle((Vector2)transform.position, mouseLocToWorld);
            stickPlacer.CueAiming(transform.position, ballToStickAngle);
        }


    }

    private void FixedUpdate()
    {
        if (changeVel)
        {
            AddVel();
        }

    }

    public void ChangeVel()
    {
        changeVel = true;
    }

    // Addes velocity to the script holder game object
    public void AddVel()
    {
        rb2d.velocity = Vector2.right * velocityMultiply;
        velocityMultiply = 30f;
        changeVel = false;
    }

    // returns the angle for two points
    float Angle(Vector2 point1, Vector2 point2)
    {
        float angle = Mathf.Atan2(point1.y - point2.y, point1.x - point2.x) * Mathf.Rad2Deg;

        if (angle < 0) angle += 360f;
        
        return angle;

    }

    private void StickInit()
    {
        GameObject stickClone = Instantiate(stickPrefab);
        stickPlacer = stickClone.GetComponent<StickPlacer>();
        stickPlacer.StartingPosition(transform.position);
    }
}
