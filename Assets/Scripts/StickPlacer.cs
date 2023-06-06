using System;
using UnityEngine;

public class StickPlacer : MonoBehaviour
{
    public StickStatus CurrentStatus { get; private set; }

    float magnitued = -3f;


    Vector3 lastBallPos;
    float lastAimDir;

    private void Awake()
    {
        CurrentStatus = StickStatus.Statring;
    }

    public void StartingPosition(Vector3 ballPos) {
     
        transform.position = ballPos + new Vector3(magnitued, 0, 0);
        CurrentStatus = StickStatus.Aiming;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ballPos">The current postion of the ball the sticks rotates around</param>
    /// <param name="aimDirection">The angle of the stick realtive to the ball, 0 - 360 </param>
    public void CueAiming(Vector3 ballPos, float aimDirection) {
        Debug.Log(CalculateCartesianVector(aimDirection) +  " " + aimDirection);
        transform.eulerAngles = new Vector3(0, 0, aimDirection);
        transform.position = ballPos + CalculateCartesianVector(aimDirection);

        lastAimDir = aimDirection;
        lastBallPos = ballPos;
    }

    public void StrikeTension(float clampedDis)
    {
        //TODO: create a pull back effect based on the date from this distance
    }

    public void StrikeInitated(bool isInitated)
    {
        if (isInitated) CurrentStatus = StickStatus.Striking;
        else CurrentStatus = StickStatus.Aiming;
    }

    Vector3 CalculateCartesianVector(float angle)
    {
        float angleInRad = angle * Mathf.Deg2Rad ;

        float x  = magnitued * Mathf.Cos(angleInRad);
        float y  = magnitued * Mathf.Sin(angleInRad);

        return new Vector3 (x, y, 0);
    }
}


public enum StickStatus
{
    Statring,
    Aiming,
    Striking,
    Retreating,
    Resting
}
