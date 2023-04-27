using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] GameEvent onUpdateUI;

    [SerializeField] GameEvent onSuccessfulRack;
    [SerializeField] GameEvent onUnsuccessfulRack;

    
    [Space]

    [Header("Turn Manager")]
    [SerializeField] TurnTakerSet turnTakerSet;
    TurnManager turnManager;
    [Space]

    [Header("Ball Manager")]
    BallManager ballManager;

    [Header("UI Element")]
    [SerializeField] TMP_Text currentPlayerInfo;
    [SerializeField] TMP_Text currentStripeInfo;
    [SerializeField] TMP_Text currentFilledInfo;

    GameStage myStage = GameStage.Rack;



    private void Start()
    {
        ballManager = new BallManager(15);

        if (turnTakerSet != null)
        {
            turnManager = new(turnTakerSet);
            turnManager.TurnHandler();
        }
        else Debug.Log("turn taker set is null");

        if (currentPlayerInfo != null)
        {
            currentPlayerInfo.text = turnManager.GetCurrentPlayer().name.ToString();
        }

        if (onUpdateUI != null) onUpdateUI.Raise();
        else Debug.LogWarning("onUpdateUI Event cannot be found");
    }


    public void PlayTurnCompute()
    {
        switch (myStage)
        {
            case GameStage.Rack:
                RackPlayTurn();
                break;
            case GameStage.BallDesignation:
                BallDesignationPhase();
                break;
            case GameStage.BallPot:
                BallPotPlayTurn();
                break;
            case GameStage.BlackBall:
                BlackBallPlayTurn();
                break;
        }
        onUpdateUI.Raise();
    }

    void RackPlayTurn()
    {
        int rackChance = Random.Range(0, 6);

        // Succsessful Rack
        if (rackChance > 0)
        {
            onSuccessfulRack.Raise();
        }


        // Unsucssesful Rack
        if (rackChance == 0)
        {
            onUnsuccessfulRack.Raise();
        }

    }

    void BallDesignationPhase()
    {
        int leagalPlay = Random.Range(0, 10);
        int potBall = Random.Range(0, 4);
        bool isPlayLegal = leagalPlay > 0;
        bool didPottedBall = potBall == 0;

        if (isPlayLegal && didPottedBall)
        {
            int rndBallType = Random.Range(0, 2);

            switch (rndBallType)
            {
                // fill
                case 0:
                    turnManager.GetCurrentPlayer().SetDesigntedBallType(BallType.Filled);
                    turnManager.GetOppenent().SetDesigntedBallType(BallType.Striped);
                    ballManager.ChangeBallNumber(BallType.Filled, 1);
                    break;
                // strip
                case 1:
                    turnManager.GetCurrentPlayer().SetDesigntedBallType(BallType.Striped);
                    turnManager.GetOppenent().SetDesigntedBallType(BallType.Filled);
                    ballManager.ChangeBallNumber(BallType.Striped, 1);
                    break;
            }
            onUpdateUI.Raise();
        }
        if (isPlayLegal && !didPottedBall)
        {
            myStage = GameStage.BallPot;
            turnManager.PassTurn();
        }
        if (!isPlayLegal)
        {
            turnManager.PassTurn();
        }
    }

    void BallPotPlayTurn()
    {
        int leagalPlay = Random.Range(0, 10);
        int potBall = Random.Range(0, 4);
        bool isPlayLegal = leagalPlay > 0;
        bool didPottedBall = potBall == 0;

        if (isPlayLegal && didPottedBall)
        {
            var playerBallType = turnManager.GetCurrentPlayer().myDesigntedBallType;
            if (playerBallType != null)
            {
                ballManager.ChangeBallNumber(playerBallType.Value, 1);
                turnManager.PassTurn();
            }

            if (ballManager.NumberOfStripes == 0 || ballManager.NumberOfFilled == 0)
            {
                myStage = GameStage.BlackBall;
            }
            onUpdateUI.Raise();
        }
        
        if (isPlayLegal && !didPottedBall)
        {
            turnManager.PassTurn();
        }

        if (!isPlayLegal)
        {
            // need to add foul logic !
            turnManager.PassTurn();
        }
    }

    void BlackBallPlayTurn()
    {

    }


    public void SuccsessfulRack()
    {
        Debug.Log("Rack succsessful");
        myStage = GameStage.BallDesignation;
        turnManager.PassTurn();
    }

    public void UnsuccsessfulRack()
    {
        Debug.Log("Rack unsuccsessful");
        var currentPlayer = turnManager.GetCurrentPlayer();
        if (currentPlayer != null)
        {
            if (currentPlayer.CanRerackBalls())
            {
                RackPlayTurn();
            }
            else
            {
                turnManager.PassTurn();
            }
        }
        else 
        {
            Debug.LogWarning("current Player Is Null");
        }
    }

    public void UpdateUI()
    {
        ballManager.BallSorter();

        if (currentPlayerInfo != null)
        {
            currentPlayerInfo.text = turnManager.GetCurrentPlayer().name.ToString();
        }
        if (currentFilledInfo != null)
        {
            currentFilledInfo.text = "Filled: " + ballManager.NumberOfFilled.ToString();
        }
        if (currentStripeInfo != null)
        {
            currentStripeInfo.text = "Striped: " + ballManager.NumberOfStripes.ToString();
        }
    }
}

public enum GameStage
{
    Rack,
    BallDesignation,
    BallPot,
    BlackBall
}

    #region TurnpassWithUpdate
    // nts: not the best use (to say the least) preformence wise
    //private void Update()
    //{
    //    // Debug Turn Changer
    //    if (Input.GetKeyDown(KeyCode.F) == true)
    //    {
    //        turnManager.PassTurn();
    //        onUpdateUI.Raise();
    //    }
    //}
    #endregion 