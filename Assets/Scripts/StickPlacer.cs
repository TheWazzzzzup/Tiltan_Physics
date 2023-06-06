using UnityEngine;

public class StickPlacer : MonoBehaviour
{
    StickStatus currentStatus;

    private void Update()
    {
        switch (currentStatus)
        {
            case StickStatus.Aiming:
                // Should Take a method(Vector3) to know how to rotate base on that
                break;
            case StickStatus.Striking:
                // should be represented with a float of the strike force and decided how far back to go based on that
                break;
            case StickStatus.Retreating:
                // fixed "animation" happens right after the stick hits the ball, retarting the cue stick and then make is disapper
                break;
            case StickStatus.Resting:
                // called right on the disappering of the stick , the idle state of the game object makeing the colliders none present
                break;
        }
    }

}


public enum StickStatus
{
    Aiming,
    Striking,
    Retreating,
    Resting
}
