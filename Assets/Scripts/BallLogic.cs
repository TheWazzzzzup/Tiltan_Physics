using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    Ball ballIndentity;

    [SerializeField] GameEvent WhitePotted;

    CustomRigidbody2D rigidBody;
    private void Start()
    {
        rigidBody = GetComponent<CustomRigidbody2D>();
        rigidBody.TriggerEvent.AddListener(CustomTriggerEnter);
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

    void CustomTriggerEnter(CustomRigidbody2D ra)
    {
        
        if (ra.CompareTag("Pot"))
        {
            if (this.gameObject.CompareTag("White"))
            {
                WhitePotted.Raise();
                rigidBody.AddVelocity(Vector2.zero);
                Debug.Log("Potted White");
                return;
            }

            rigidBody.AddVelocity(Vector2.zero);
            ballIndentity.ChangeBallStatus(BallStatus.Potted);
            ChangeBallStatus();
            
        }
        Debug.Log($"{gameObject.name} ball status is {ballIndentity.MyStatus}");
    }

}
