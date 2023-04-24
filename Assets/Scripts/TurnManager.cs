using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public TurnTakerSet playerSet;

    int index = 0;

    private void Start()
    {
        TurnHandler();
    }

    [ContextMenu("Pass Turn")]
    public void PassTurn()
    {
        TurnPasser();
    }
    
    void TurnPasser()
    {
        if (index + 1 < playerSet.GetList().Count)
        {
            index++;
        }
        else index = 0;
        TurnHandler();
    }
    
    void TurnHandler()
    {
        switch (index)
        {
            case 0:
                playerSet.GetListMember(1).EndTurn();
                playerSet.GetListMember(index).StartTurn();
                break;
            case 1:
                playerSet.GetListMember(0).EndTurn();
                playerSet.GetListMember(index).StartTurn();
                break;

        }
    }
}
