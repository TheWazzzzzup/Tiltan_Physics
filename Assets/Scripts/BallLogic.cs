using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    Ball ballIndentity;

    [SerializeField] GameEvent WhitePotted;

    RigidAmitComponent rigidAmit;
    private void Start()
    {
        rigidAmit = GetComponent<RigidAmitComponent>();
        rigidAmit.TriggerEvent.AddListener(CustomTriggerEnter);
    }

    public void PassBallIndentity(Ball currentBall)
    {
        ballIndentity = currentBall;
    }

    private void ChangeBallStatus()
    {
        if (ballIndentity.MyStatus == BallStatus.Potted)
        {
            gameObject.SetActive(false);
        }
    }

    void CustomTriggerEnter(RigidAmitComponent ra)
    {
        
        if (ra.CompareTag("Pot"))
        {
            if (this.gameObject.CompareTag("White"))
            {
                WhitePotted.Raise();
                rigidAmit.AddVelocity(Vector2.zero);
                Debug.Log("Potted White");
                return;
            }

            rigidAmit.AddVelocity(Vector2.zero);
            ballIndentity.ChangeBallStatus(BallStatus.Potted);
            ChangeBallStatus();
            
        }
        Debug.Log($"{gameObject.name} ball status is {ballIndentity.MyStatus}");
    }

}
