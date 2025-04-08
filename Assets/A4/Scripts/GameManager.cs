using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int totalScore = 0; //The player score.
    public TextMeshProUGUI totalScoreText; //The text for the score tracker.

    // Update is called once per frame
    void Update()
    {

        totalScoreText.text = "Score: " + totalScore; //Updates the score tracker text.

    }

    public void increaseScore(int score)
    {

        totalScore += score; //Increases the score by the value provided. Usually called by the targets when they are destroyed.

    }

}
