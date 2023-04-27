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


    void PlayTurnCompute()
    {
        switch (myStage)
        {
            case GameStage.Rack:
                RackPlayTurn();
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

    void BallPotPlayTurn()
    {

    }

    void BlackBallPlayTurn()
    {

    }


    [ContextMenu("Refresh")]
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