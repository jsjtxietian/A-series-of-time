using System;
using Ardunity;
using UnityEngine;
using UnityEngine.UI;

public class Driver2 : MonoBehaviour
{
    public Text Hours;
    public Text Seconds;
    public Text Minutes;
    public Text Months;
    public Text Years;
    public Text Days;

    public GameObject Audio;

    private bool EarphoneIsUp;
    private DateTime startTime;
    private DateTime stopTime;
    private int secondsPassed;

    public AnalogInput SensorInput;
    private float LastInput;
    private float CurrentInput;
    private int testSeconds = 300;


    #region  currentTime and the get/set method
    public int currentMinute
    {
        get { return PlayerPrefs.GetInt("currentMinute2", 10); }
        set { PlayerPrefs.SetInt("currentMinute2",value); PlayerPrefs.Save();}
    }

    public int currentSecond
    {
        get { return PlayerPrefs.GetInt("currentSecond2", 40); }
        set { PlayerPrefs.SetInt("currentSecond2", value); PlayerPrefs.Save(); }
    }
    
    public int currentHour
    {
        get { return PlayerPrefs.GetInt("currentHour2", 13); }
        set { PlayerPrefs.SetInt("currentHour2", value); PlayerPrefs.Save(); }
    }

    public int currentDay
    {
        get { return PlayerPrefs.GetInt("currentDay", 20); }
        set { PlayerPrefs.SetInt("currentDay", value); PlayerPrefs.Save(); }
    }

    public int currentMonth
    {
        get { return PlayerPrefs.GetInt("currentMonth", 5); }
        set { PlayerPrefs.SetInt("currentMonth", value); PlayerPrefs.Save(); }
    }
    public int currentYear
    {
        get { return PlayerPrefs.GetInt("currentYear", 23); }
        set { PlayerPrefs.SetInt("currentYear", value); PlayerPrefs.Save(); }
    }
    #endregion


    // Use this for initialization
    void Start ()
    {
        Hours.text = string.Format("{0:D2}", currentHour);
        Seconds.text = string.Format("{0:D2}", currentSecond);
        Minutes.text = string.Format("{0:D2}", currentMinute);
        Years.text = string.Format("{0:D2}", currentYear);
        Months.text = string.Format("{0:D2}", currentMonth);
        Days.text = string.Format("{0:D2}", currentDay);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (testSeconds != 0)
        {
            testSeconds--;
            if (testSeconds == 1)
            {
                Debug.Log("first");
                LastInput = SensorInput.Value;
            }
            return;
        }
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    HeadsetUp();
        //}
        //   else if (Input.GetKeyDown((KeyCode.B)))
        //{
        //    HeadsetDown();
        //}
        checkSensorAndCall();
        

        if (EarphoneIsUp)
	    {
	        //current time
	        Hours.text = string.Format("{0:D2}",currentHour)  ;
	        Seconds.text = string.Format("{0:D2}",currentSecond) ;
	        Minutes.text = string.Format("{0:D2}",currentMinute);
	        Years.text = string.Format("{0:D2}", currentYear);
	        Months.text = string.Format("{0:D2}", currentMonth);
	        Days.text = string.Format("{0:D2}", currentDay);
        }
    }

    public void HeadsetUp()
    {
        Audio.SendMessage("Play");
        this.InvokeRepeating("ReduceTime",0,1.0f);
        startTime = DateTime.Now;
    }

    public void HeadsetDown()
    {
        Audio.SendMessage("Stop");
        this.CancelInvoke();

        stopTime = DateTime.Now;
        secondsPassed = (int)(stopTime - startTime).TotalSeconds;
        Debug.Log(secondsPassed);
    }

    private void ReduceTime()
    {
        currentSecond--;
        if (currentSecond == -1)
        {
            currentSecond = 59;
            currentMinute--;
        }
        if (currentMinute == -1)
        {
            currentMinute = 59;
            currentHour--;
        }
        if (currentHour == -1)
        {
            currentHour = 23;
            currentDay--;
        }
        //todo month 30 31 28
    }

    public void Reset()
    {
        currentSecond = 40;
        currentHour = 13;
        currentMinute = 10;
        currentDay = 20;
        currentMonth = 5;
        currentYear = 23;
    }

    private void checkSensorAndCall()
    {
        CurrentInput = SensorInput.Value;
        float delta = Math.Abs(CurrentInput - LastInput);
        LastInput = CurrentInput;

        if (delta > 0.9f)
        {
            if (EarphoneIsUp)
            {
                HeadsetDown();
            }
            else
            {
                HeadsetUp();
            }
            EarphoneIsUp = !EarphoneIsUp;
        }
    }
}
