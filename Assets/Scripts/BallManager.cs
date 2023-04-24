using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    Ball[] balls = new Ball[15];

    void BallInitializer()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i] = new Ball(i + 1);
        }
    }

    private void Start()
    {
        BallInitializer();

        foreach (var ball in balls)
        {
            Debug.Log(ball.GetBallType().ToString());
        }
    }

}
