using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball
{
    public BallType MyType { get; private set;} = BallType.Unassgined;
    public BallStatus MyStatus { get; private set; } = BallStatus.Unpotted;

    int orderNum;

    public Ball(int num)
    {
        orderNum = num;

        if (orderNum < 8) MyType = BallType.Filled;
        if (orderNum == 8) MyType = BallType.Black;
        if (orderNum > 8) MyType = BallType.Striped;
        if (orderNum > 15)
        {
            Debug.Log("You created more than 15 balls, this extra ball was overTheTop");
            MyType = BallType.OverTheTop;
        }
    }

    public void ChangeBallStatus(BallStatus toStatus)
    {
        MyStatus = toStatus;
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
