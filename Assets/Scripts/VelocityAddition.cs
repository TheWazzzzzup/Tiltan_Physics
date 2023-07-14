using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VelocityAddition : MonoBehaviour
{
    // Public


    // Serialized
    
    [SerializeField] GameObject stickPrefab;
    [SerializeField] GameEvent turnPlayed;

    // Private

    RigidAmitComponent ra;
    
    StickPlacer stickPlacer;

    Vector2 mouseLocToWorld;
    Vector2 shootingTowards;

    Vector3 pullAnchorPos;

    float ballToStickAngle;
    float velocityMultiply = .01f;
    float dist;

    bool changeVel = false;

    
    void Start()
    {
        ra = GetComponent<RigidAmitComponent>();
        StickInit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) { ChangeVel();  }

        mouseLocToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (stickPlacer.CurrentStatus == StickStatus.Retreating) return;
            stickPlacer.ChangeCurrentStatus(StickStatus.Striking);
            pullAnchorPos = mouseLocToWorld;
            shootingTowards = VectorCalculator.CalculateCartesianVector2(stickPlacer.pointingTowards, 1);
        }

        if (Input.GetMouseButtonUp(0) && stickPlacer.CurrentStatus == StickStatus.Striking) {
            dist = Vector3.Distance(pullAnchorPos, mouseLocToWorld);
            ChangeVel();
        }

        if (stickPlacer.CurrentStatus == StickStatus.Aiming) {
            ballToStickAngle = VectorCalculator.Angle((Vector2)transform.position, mouseLocToWorld);
            stickPlacer.CueAiming(transform.position, ballToStickAngle);
        }

        if (stickPlacer.CurrentStatus == StickStatus.Retreating) { 
            // Enter stick pickup animation


        }

    }

    private void FixedUpdate()
    {
        if (changeVel)
        {
            AddVel();
        }

        if (ra.GetVelocity() == Vector2.zero && stickPlacer.CurrentStatus == StickStatus.Retreating)
        {
            stickPlacer.StartingPosition(transform.position);
            turnPlayed.Raise();
        }

    }

    public void ChangeVel() => changeVel = true;

    public void AddVel()
    {
        stickPlacer.ChangeCurrentStatus(StickStatus.Retreating);
        float x = Mathf.InverseLerp(0, 100, dist);
        x = Mathf.Lerp(1, 2.5f, x);
        ra.AddVelocity(shootingTowards * x);
        velocityMultiply = 30f;  
        changeVel = false;
    }

    private void StickInit()
    {
        GameObject stickClone = Instantiate(stickPrefab);
        stickPlacer = stickClone.GetComponent<StickPlacer>();
        stickPlacer.StartingPosition(transform.position);

    }

}


public static class VectorCalculator
{
    static float angle;

    public static Vector2 CalculateCartesianVector2(float angle, float magnitued)
    {
        float angleInRad = angle * Mathf.Deg2Rad;

        float x = magnitued * Mathf.Cos(angleInRad);
        float y = magnitued * Mathf.Sin(angleInRad);

        return new Vector2(x, y);
    }

    public static Vector3 CalculateCartesianVector(float angle, float magnitued)
    {
        float angleInRad = angle * Mathf.Deg2Rad;

        float x = magnitued * Mathf.Cos(angleInRad);
        float y = magnitued * Mathf.Sin(angleInRad);

        return new Vector3(x, y, 0);
    }

    public static float Angle(Vector2 point1, Vector2 point2)
    {
        angle = Mathf.Atan2(point1.y - point2.y, point1.x - point2.x) * Mathf.Rad2Deg;

        if (angle < 0) angle += 360f;

        return angle;

    }
} 