using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class printScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        scoreText.text = "Score: " + score;
    }
}
