using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VelocityAddition : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField] GameObject stickPrefab;

    bool changeVel = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (changeVel)
        {
            AddVel();
        }

        if (rb2d.velocity == Vector2.zero)
        {
            TurnEndPicker();
        }
    }

     
    void TurnEndPicker()
    {
        // TODO: Add delay for this


    }

    public void ChangeVel()
    {
        changeVel = true;
    }

    // Addes velocity to the script holder game object
    public void AddVel()
    {
        rb2d.velocity = Vector2.right * 30f;
        changeVel = false;
    }

    [ContextMenu("StopVel")]
    public void StopVel()
    {
        rb2d.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) { ChangeVel();  }
    }
}
