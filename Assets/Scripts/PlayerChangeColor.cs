using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeColor : MonoBehaviour
{
    Color initColor;
    Material mat;

    private void Awake()
    {
        mat = GetComponent<Material>();
        initColor = mat.color;   
    }

    void MyTurn(bool isThisPlayerTurn)
    {
        if (isThisPlayerTurn)
        {
            mat.color = initColor;
        }
        else
        {
            mat.color = Color.gray;
        }
    }
}
