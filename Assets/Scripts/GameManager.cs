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
    [SerializeField] BallInstancer ballInstancer;

    [Header("UI Element")]
    [SerializeField] TMP_Text currentPlayerInfo;
    [SerializeField] TMP_Text currentStripeInfo;
    [SerializeField] TMP_Text currentFilledInfo;

    GameStage myStage = GameStage.Rack;



    private void Start()
    {
        ballManager = new BallManager(15);
        ballInstancer.InjectBallManager(ballManager);

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

    public void ChangeTurn()
    {
        turnManager.PassTurn();
        UpdateUI();
    }

    void UpdateUI()
    {
        currentPlayerInfo.text = turnManager.GetCurrentPlayer().name;
        ballManager.BallSorter();
        currentFilledInfo.text = "Filled: " + ballManager.NumberOfFilled.ToString();
        currentStripeInfo.text = "Stripes: " + ballManager.NumberOfStripes.ToString();
    }

    public void OnStrikerFoul()
    {
        turnManager.PassTurn();
    }

}

public enum GameStage
{
    Rack,
    BallDesignation,
    BallPot,
    BlackBall
}