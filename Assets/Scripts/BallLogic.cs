using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    Ball ballIndentity;

    private void Start()
    {
        RigidAmitComponent rigidAmit = GetComponent<RigidAmitComponent>();
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
            ballIndentity.ChangeBallStatus(BallStatus.Potted);
        }
        ChangeBallStatus();
        Debug.Log($"{gameObject.name} ball status is {ballIndentity.MyStatus}");
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Pot"))
    //    {
    //        ballIndentity.ChangeBallStatus(BallStatus.Potted);
    //    }
    //    ChangeBallStatus();
    //}

}
