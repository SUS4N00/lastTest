using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    public int currentScore;
    public TextMeshProUGUI scoreTxt;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = "score: " + currentScore;
        if(GameObject.Find("player").GetComponent<player>().heal <= 0){
            PlayerPrefs.SetInt("Score", currentScore);
            PlayerPrefs.Save();
        }
    }
}
