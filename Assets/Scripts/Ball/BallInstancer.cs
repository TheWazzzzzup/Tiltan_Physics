using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

/// <summary>
/// Monobehavior implementation of the ball manager
/// </summary>
/// Maybe inherht form ball manager in the furure
public class BallInstancer : MonoBehaviour
{
    [Header("BallPrefabs")]
    [SerializeField] private GameObject whiteBall;
    [SerializeField] private GameObject fillBallNumOne;
    [SerializeField] private GameObject fillBallNumTwo;
    [SerializeField] private GameObject fillBallNumThree;
    [SerializeField] private GameObject fillBallNumFour;
    [SerializeField] private GameObject fillBallNumFive;
    [SerializeField] private GameObject fillBallNumSix;
    [SerializeField] private GameObject fillBallNumSeven;
    [SerializeField] private GameObject eightBall;
    [SerializeField] private GameObject stripeBallNumOne;
    [SerializeField] private GameObject stripeBallNumTwo;
    [SerializeField] private GameObject stripeBallNumThree;
    [SerializeField] private GameObject stripeBallNumFour;
    [SerializeField] private GameObject stripeBallNumFive;
    [SerializeField] private GameObject stripeBallNumSix;
    [SerializeField] private GameObject stripeBallNumSeven;
    [Space]

    [SerializeField] Transform triangleStartingTransform;
    [SerializeField] Transform whiteStatingTransform;
    [SerializeField] CollisionManager collisionManager; 


    GameObject ball;
    GameObject whiteBallGameObject;


    BallManager ballManager;                               // * Maybe inherht form ball manager in the furure *

    float  instanceOffsetY = 1.5f;
    float instanceOffsetX = 2f;

    public void InjectBallManager(BallManager ballManager)
    {
        if (ballManager != null)
        {
            this.ballManager = ballManager;

            ball = Instantiate(whiteBall, whiteStatingTransform);
            whiteBallGameObject = ball;
            collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());

            InstanceBalls();
        }
    }

    public void ResetWhite()
    {
        whiteBallGameObject.transform.position = whiteStatingTransform.position;
    }

    void InstanceBalls()
    {
        int prymidPhase = 1;
        int currentBallInsidePhase = 0;
        int lastXSpaceing = 1;

        Vector3 triV3;

        for (int i = 0; i < ballManager.balls.Length; i++)
        {
            triV3 = triangleStartingTransform.position;
            
            if (currentBallInsidePhase < prymidPhase)
            {
                InstaniateByOrderNum(i, triangleStartingTransform);
                currentBallInsidePhase++;
            }

            if (currentBallInsidePhase == prymidPhase)
            {
                triangleStartingTransform.transform.position = triV3 + new Vector3(0, -1 *(lastXSpaceing * instanceOffsetY), 0);
                triV3 = triangleStartingTransform.transform.position;
                lastXSpaceing += 2;
                prymidPhase++;
                currentBallInsidePhase = 0;
                // ofset X axis
                triangleStartingTransform.transform.position = triV3 + new Vector3 (instanceOffsetX, 0, 0);
                continue;
            }
            triangleStartingTransform.transform.position = triV3 + new Vector3(0, 2 * instanceOffsetY, 0);
        }
    }

    void InstaniateByOrderNum(int numInOrder, Transform spawnLocation)
    {
        switch(numInOrder)
        {
            case 0:
                ball = Instantiate(fillBallNumOne, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 1:
                ball = Instantiate(fillBallNumTwo, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 2:
                ball = Instantiate(fillBallNumThree, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 3:
                ball = Instantiate(fillBallNumFour, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 4:
                ball = Instantiate(fillBallNumFive, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 5:
                ball = Instantiate(fillBallNumSix, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 6:
                ball = Instantiate(fillBallNumSeven, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 7:
                ball = Instantiate(eightBall, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 8:
                ball = Instantiate(stripeBallNumOne, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 9:
                ball = Instantiate(stripeBallNumTwo, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 10:
                ball = Instantiate(stripeBallNumThree, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 11:
                ball = Instantiate(stripeBallNumFour, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 12:
                ball = Instantiate(stripeBallNumFive, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 13:
                ball = Instantiate(stripeBallNumSix, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            case 14:
                ball = Instantiate(stripeBallNumSeven, spawnLocation.transform.position, spawnLocation.transform.rotation);
                ball.GetComponent<BallLogic>().PassBallIndentity(ballManager.balls[numInOrder]);
                collisionManager.bodies.Add(ball.GetComponent<RigidAmitComponent>());
                break;
            default:
                Debug.LogWarning($"To Many Balls in {this.name} script, instaniate by number inserted non logic number");
                break;

        }

    }

}
