using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle2DAmitCollider : MonoBehaviour
{
    public float Radius => transform.localScale.x / 2;

    public bool validateRefresh;


    private void OnValidate()
    {
        Debug.DrawLine(transform.position, new Vector3(transform.position.x + Radius, transform.position.y), Color.green, 1f);
    }
}

