using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    Ball ballIndentity;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pot"))
        {
            ballIndentity.ChangeBallStatus(BallStatus.Potted);
        }
        ChangeBallStatus();
    }
}
