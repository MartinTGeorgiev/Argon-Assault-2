using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score;

    void Start()
    {
        score = 0;
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score = score + amountToIncrease;
        Debug.Log($"Score is: {score}");
    }
}
