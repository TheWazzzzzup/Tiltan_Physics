using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalculateVectorForth : MonoBehaviour
{
    Vector2 v21;
    Vector2 v22;
    Vector2 v23;

    Vector2 userGuessedVector;
    Vector2 equvilentVector;

    [SerializeField] TMP_InputField X;
    [SerializeField] TMP_InputField Y;

    [SerializeField] TextMeshProUGUI FirstVectorText;
    [SerializeField] TextMeshProUGUI SecondVectorText;
    [SerializeField] TextMeshProUGUI ThirdVectorText;

    private void Start()
    {
        PopVectors();
        FirstVectorText.text = v21.ToString();
        SecondVectorText.text = v22.ToString();
        ThirdVectorText.text = v23.ToString();
    }

    public Vector2 GenerateRandomVector()
    {
        float x, y;
        x = (Random.Range(0, 50)/1.2f);
        y = (Random.Range(0, 50) / 1.7f);

        return new Vector2(x, y);
    }

    public Vector2 GenerateRandomVector(Vector2 VectorsSum)
    {
        float x, y;
        x = (Random.Range(0, VectorsSum.x));
        y = (Random.Range(0, VectorsSum.y));

        return new Vector2(x, y);
    }

    void PopVectors()
    {
        v21 = GenerateRandomVector();
        v22 = GenerateRandomVector();

        equvilentVector = v21 + v22;

        PopVectorToAddTo();
    }

    void PopVectorToAddTo()
    {
        v23 = GenerateRandomVector(equvilentVector);

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
            if (userGuessedVector + v23 == equvilentVector)
            {
                Debug.Log("You Were Right");
            }
            else
            {
                Debug.Log("Develope a brain you caveman");

                Debug.Log($"Your guess {userGuessedVector + v23} the right one is {equvilentVector}");
            }
        }
    }
}
