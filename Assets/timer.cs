using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{

    public float startTime = 5;
    public float currentTime;
    bool raceStarted = false;
    public TextMeshProUGUI text;
    public checkPoints checkPoint;
    float highScore = Mathf.Infinity;

    private void Start()
    {
        ///text = GetComponent<TMPro.TextMeshProUGUI>();
        highScore = PlayerPrefs.GetFloat("HIGH SCORE", 100000);
        print("Time to beat: " + highScore.ToString("F3"));
        currentTime = startTime;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            print("Clear high score");
            highScore = 100000;
            PlayerPrefs.DeleteKey("HIGH SCORE");
        }

        if (!raceStarted)
        {
            currentTime -= Time.deltaTime;
            text.text = "RACE STARTS IN " + Mathf.Ceil(currentTime).ToString();
            if (currentTime <= 0)
            {
                currentTime = 0;
                raceStarted = true;
            }
        }
        else
        {
            currentTime += Time.deltaTime;
            if (!checkPoint.raceFinished) text.text = currentTime.ToString("F2");
        }


    }
    //float countdown()
    public void FinishRace(float finishTime)
    {
        //if (checkPoint.raceFinished == true)
        //{
        text.text = "Race Finished in " + finishTime.ToString("F2");

        if (finishTime < highScore)
        {
            highScore = finishTime;
            PlayerPrefs.SetFloat("HIGH SCORE", finishTime);
            print("You got high score: " + highScore.ToString("F3"));
        }
        //}
    }
}
