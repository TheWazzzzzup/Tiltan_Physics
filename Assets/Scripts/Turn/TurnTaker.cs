using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTaker : MonoBehaviour
{
    

    bool myTurn = false;

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
