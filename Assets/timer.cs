using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class timer : MonoBehaviour
{

    public float startTime = 5;
    float speed;
    public float currentTime;
    bool raceStarted = false;
    public TextMeshProUGUI text;
    public TextMeshProUGUI speedText;
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
            float time = 0f;
            TimeSpan t = TimeSpan.FromSeconds(currentTime);
            if (!checkPoint.raceFinished) text.text = string.Format("{0,1:0}:{1,2:00}", t.Minutes, t.Seconds);//currentTime.ToString("F2");
        }
        speedoMeter(speed);


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
    public void speedoMeter(float speed)
    {
        //if (checkPoint.raceFinished == true)
        //{
        //        speed = GetComponent<Rigidbody>().velocity.magnitude * 2.237f;
        speed =  Mathf.Abs( transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity).z * 2.237f);
        speedText.text = "Your speed: " + speed.ToString("F1");

        //}
    }
}
