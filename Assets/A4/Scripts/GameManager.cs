using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int totalScore = 0;
    public TextMeshProUGUI weaponSelectText;

    // Update is called once per frame
    void Update()
    {

        weaponSelectText.text = "Score: " + totalScore;

    }

    public void increaseScore(int score)
    {

        totalScore += score;

    }

}
