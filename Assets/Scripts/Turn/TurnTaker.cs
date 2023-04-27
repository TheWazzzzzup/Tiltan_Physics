using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTaker : MonoBehaviour
{
    public BallType? myDesigntedBallType { get; private set; }
    

    bool myTurn = false;

    public bool qualfiedForRerack { get; private set; } = true;

    Color intendetColor;
    Renderer ren;

    private void OnBecameVisible()
    {
        ren = GetComponent<Renderer>();
        if (ren != null)
        {
            intendetColor = ren.material.color;
        }
    }

    public void StartTurn() => myTurn = true;
    public void EndTurn() => myTurn = false;

    public void SetDesigntedBallType(BallType deigntedType) => myDesigntedBallType = deigntedType;

    public bool CanRerackBalls()
    {
        if (qualfiedForRerack)
        {
            qualfiedForRerack = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    // ?Maybe add an TurnChangedEvent?
    // Needs to be changed from fixed update asap!

    // ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !
    private void FixedUpdate()
    {
        if (ren != null)
        {
            if (myTurn) ren.material.color = intendetColor;
            else ren.material.color = Color.gray;
        }
    }
    // ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !
}
