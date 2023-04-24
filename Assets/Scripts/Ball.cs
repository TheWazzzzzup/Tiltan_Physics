using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball
{
    int orderNum;

    BallType myType = BallType.Unassgined;
    BallStatus myStatus = BallStatus.Unpotted;

    public Ball(int num)
    {
        orderNum = num;

        if (orderNum < 8) myType = BallType.Filled;
        if (orderNum == 8) myType = BallType.Black;
        if (orderNum > 8) myType = BallType.Striped;
        if (orderNum > 15)
        {
            Debug.Log("You created more than 15 balls, this extra ball was overTheTop");
            myType = BallType.OverTheTop;
        }
    }

    public BallType GetBallType()
    {
        return myType;
    }
    
}

public enum BallStatus
{
    Unpotted,
    Potted,
    OffTable
}

public enum BallType
{
    Unassgined,
    Filled,
    Black,
    Striped,
    OverTheTop
}
