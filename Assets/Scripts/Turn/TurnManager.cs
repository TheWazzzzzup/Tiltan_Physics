using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager
{
    TurnTakerSet playerSet;
    int index;

    public TurnManager(TurnTakerSet turnTakerSet)
    {
        playerSet = turnTakerSet;
        index = 0;
    }

    [ContextMenu("Pass Turn")]
    public void PassTurn()
    {
        TurnPasser();
    }

    public TurnTaker GetCurrentPlayer()
    {
        return playerSet.GetListMember(index);
    }

    public TurnTaker GetOppenent()
    {
        int currentPlayer = index;
        if (index + 1 < playerSet.GetList().Count)
        {
            return playerSet.GetListMember(currentPlayer++);
        }
        else return playerSet.GetListMember(0);
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
    
    public void TurnHandler()
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
