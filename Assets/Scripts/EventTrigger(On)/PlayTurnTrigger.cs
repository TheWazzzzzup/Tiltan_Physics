using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTurnTrigger : MonoBehaviour
{
    [SerializeField] GameEvent onPlayTurn;

    private void Awake()
    {
        var playTurnButton = GetComponent<Button>();
        if (playTurnButton != null)
        {
            Debug.Log("Play turn button found");
            playTurnButton.onClick.AddListener(onPlayTurn.Raise);
        }
    }
}
