using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalculateVector : MonoBehaviour
{
    Vector2 v21;
    Vector2 v22;

    Vector2 userGuessedVector;
    Vector2 equvilentVector;

    [SerializeField] TMP_InputField X;
    [SerializeField] TMP_InputField Y;

    [SerializeField] TextMeshProUGUI FirstVectorText;
    [SerializeField] TextMeshProUGUI SecondVectorText;

    private void Start()
    {
        PopTwoVectors();
        FirstVectorText.text = v21.ToString();
        SecondVectorText.text = v22.ToString();
    }

    public Vector2 GenerateRandomVector()
    {
        float x, y;
        x = (Random.Range(0, 50)/1.2f);
        y = (Random.Range(0, 50) / 1.7f);

        return new Vector2(x, y);
    }

    void PopTwoVectors()
    {
        v21 = GenerateRandomVector();
        v22 = GenerateRandomVector();

        equvilentVector = v21 + v22;
    }

    void GetUserInput()
    {
        userGuessedVector = new Vector2(float.Parse(X.text), float.Parse(Y.text));
    }

    public void CheckUserGuess()
    {
        GetUserInput();
        if (userGuessedVector != null)
        {
            if (userGuessedVector == equvilentVector)
            {
                Debug.Log("You Were Right");
            }
            else
            {
                Debug.Log("Develope a brain you caveman");

                Debug.Log($"You guessed {userGuessedVector} the right one is {equvilentVector}");
            }
        }
    }
}
