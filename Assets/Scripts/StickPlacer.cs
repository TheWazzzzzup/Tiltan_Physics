using System;
using UnityEngine;

public class StickPlacer : MonoBehaviour
{
    public StickStatus CurrentStatus { get; private set; }

    public float pointingTowards { get; private set; }

    float magnitued = -3f;
    
    Vector3 lastBallPos;

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
        //Debug.Log(VectorCalculator.CalculateCartesianVector(aimDirection,magnitued) +  " " + aimDirection);
        transform.eulerAngles = new Vector3(0, 0, aimDirection);
        transform.position = ballPos + VectorCalculator.CalculateCartesianVector(aimDirection, magnitued);

        pointingTowards = aimDirection;
        lastBallPos = ballPos;
    }

    public void StrikeTension(float clampedDis)
    {
        //TODO: create a pull back effect based on the date from this distance
    }

    public void ChangeCurrentStatus(StickStatus status) {
        CurrentStatus = status;
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
