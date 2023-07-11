using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLoopManager : MonoBehaviour
{
    [Header("TMP")]
    [SerializeField] private TextMeshProUGUI FilledText;
    [SerializeField] private TextMeshProUGUI StripeText;
    [Space]

    [SerializeField] BallInstancer ballInstancer;

    public BallManager ballManager { get; private set; }

    private void Awake()
    {
        ballManager = new(15);
        ballInstancer.InjectBallManager(ballManager);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) RefreshUI();
    }

    void RefreshUI()
    {
        
    }
}
