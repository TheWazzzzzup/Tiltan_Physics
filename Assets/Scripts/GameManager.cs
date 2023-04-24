using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Events")]
    // called when player set to start his turn
    [SerializeField] GameEvent onPlayTurn;
    // called when player hits the ball
    [SerializeField] GameEvent onBallHit;
    // called when player hits the ball but foul
    [SerializeField] GameEvent onFoulAfterHit;
    // called when striker foul on the begging
    [SerializeField] GameEvent onStrikerFoul;
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
    }


    void PlayTurnCompute()
    {
        switch (myStage)
        {
            case GameStage.Rack:

                break;
            case GameStage.BallPot:

                break;
            case GameStage.BlackBall:

                break;
        }
    }

    void RackPlayTurn()
    {

    }

    void BallPotPlayTurn()
    {

    }

    void BlackBallPlayTurn()
    {

    }

    private void Update()
    {
        // Debug Turn Changer
        if (Input.GetKeyDown(KeyCode.F) == true)
        {
            turnManager.PassTurn();
            UpdateUI();
        }
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
