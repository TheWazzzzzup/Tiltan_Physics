using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager
{
    public Ball[] balls { get; private set; }

    public int NumberOfStripes { get; private set; }
    public int NumberOfFilled { get; private set; }
    public bool BlackBallOnDeck { get; private set; }

    /// <summary>
    /// Ball Manager Constructor
    /// </summary>
    /// <param name="ballNumber">Recomended to be 15</param>
    public BallManager(int ballNumber)
    {
        balls = new Ball[ballNumber];
        BallInitializer();
        DebugBalls();
    }

    void BallInitializer()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i] = new Ball(i + 1);
        }
    }

    private void DebugBalls()
    {
        foreach (var ball in balls)
        {
            Debug.Log(ball.MyType.ToString());
        }
    }

    public void ChangeBallInList(BallType potType, int numberOfPotted)
    {
        Debug.Log($"The Player potted {numberOfPotted} {potType} ");
        foreach (var ball in balls)
        {
            if (ball.MyType == potType && numberOfPotted > 0)
            {
                ball.ChangeBallStatus(BallStatus.Potted);
                numberOfPotted--;
            }
            if (numberOfPotted == 0) break;
        }
    }

    public void ChangeBallNumber(BallType potType, int numberOfBallsPotted)
    {
        switch (potType)
        {
            case BallType.Filled:
                ChangeBallInList(potType, numberOfBallsPotted);
                break;
            case BallType.Black:
                ChangeBallInList(potType, numberOfBallsPotted);
                break;
            case BallType.Striped:
                ChangeBallInList(potType, numberOfBallsPotted);
                break;
        }
    }

    public void BallSorter()
    {
        NumberOfFilled = 0;
        NumberOfStripes = 0;
        BlackBallOnDeck = false;
        foreach (var ball in balls)
        {
            switch (ball.MyType)
            {
                case BallType.Unassgined:
                    Debug.Log("Unassgined ball constructor bug!");
                    break;
                case BallType.Filled:
                    if (ball.MyStatus == BallStatus.Unpotted) NumberOfFilled++;
                    break;
                case BallType.Black:
                    if (ball.MyStatus == BallStatus.Unpotted) BlackBallOnDeck = true;
                    break;
                case BallType.Striped:
                    if (ball.MyStatus == BallStatus.Unpotted) NumberOfStripes++;
                    break;
                case BallType.OverTheTop:
                    Debug.Log("Too many balls");
                    break;
            }
        }
    }

}