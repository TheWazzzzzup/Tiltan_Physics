using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartGameRaiser : MonoBehaviour
{
    [SerializeField] GameEvent onGameStart;

    private void Awake()
    {
        var startButton = GetComponent<Button>();
        if (startButton != null)
        {
            Debug.Log("Start Button Found");
            startButton.onClick.AddListener(onGameStart.Raise);
        }
    }
}
