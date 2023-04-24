using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnTakerComponentSet : MonoBehaviour
{
    public TurnTakerSet turnTakerSet;

    TurnTaker turnTaker;
    private void Awake()
    {
        turnTaker = GetComponent<TurnTaker>();
    }

    private void OnEnable()
    {
        if (turnTaker != null)
        {
            turnTakerSet.Add(turnTaker);
        }

        else Debug.Log("Gameobject doesnot have TurnTaker");
    }

    private void OnDisable()
    {
        if (turnTaker != null)
        {
            turnTakerSet.Remove(turnTaker);
        }

        else Debug.Log("Gameobject doesnot have TurnTaker");
    }
}
