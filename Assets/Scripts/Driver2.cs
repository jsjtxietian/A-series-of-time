using System;
using System.IO;
using System.Security.AccessControl;
using Ardunity;
using UnityEngine;
using UnityEngine.UI;

public class Driver2 : MonoBehaviour , Driver
{
    public Text Hours;
    public Text Seconds;
    public Text Minutes;
    public Text Months;
    public Text Years;
    public Text Days;

    public GameObject Audio;

    private Printer printer;
    private bool EarphoneIsUp;

    public AnalogInput SensorInput;
    private int LastInput;
    private int CurrentInput;
    private int testSeconds = 300;
    public float Threshold;


    #region  currentTime and the get/set method

    public int currentMinute
    {
        get { return PlayerPrefs.GetInt("currentMinute2", 10); }
        set
        {
            PlayerPrefs.SetInt("currentMinute2", value);
            PlayerPrefs.Save();
        }
    }

    public int currentSecond
    {
        get { return PlayerPrefs.GetInt("currentSecond2", 40); }
        set
        {
            PlayerPrefs.SetInt("currentSecond2", value);
            PlayerPrefs.Save();
        }
    }

    public int currentHour
    {
        get { return PlayerPrefs.GetInt("currentHour2", 13); }
        set
        {
            PlayerPrefs.SetInt("currentHour2", value);
            PlayerPrefs.Save();
        }
    }

    public int currentDay
    {
        get { return PlayerPrefs.GetInt("currentDay", 20); }
        set
        {
            PlayerPrefs.SetInt("currentDay", value);
            PlayerPrefs.Save();
        }
    }

    public int currentMonth
    {
        get { return PlayerPrefs.GetInt("currentMonth", 5); }
        set
        {
            PlayerPrefs.SetInt("currentMonth", value);
            PlayerPrefs.Save();
        }
    }

    public int currentYear
    {
        get { return PlayerPrefs.GetInt("currentYear", 23); }
        set
        {
            PlayerPrefs.SetInt("currentYear", value);
            PlayerPrefs.Save();
        }
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        UpdateText();

        printer = new Printer();

        StreamReader sr = new StreamReader("D:\\Threshold.txt");
        string s = sr.ReadLine();
        Threshold = float.Parse(s);
    }

    // Update is called once per frame
    void Update()
    {
        if (testSeconds != 0)
        {
            testSeconds--;
            if (testSeconds == 1)
            {
                Debug.Log("first");
                LastInput = SensorInput.Value > Threshold ? 1 : 0;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            HeadsetUp();
            EarphoneIsUp = !EarphoneIsUp;
        }
        else if (Input.GetKeyDown((KeyCode.B)))
        {
            HeadsetDown();
            EarphoneIsUp = !EarphoneIsUp;
        }
        else if (Input.GetKeyDown((KeyCode.Escape)))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Reset();
        }

        checkSensorAndCall();


        if (EarphoneIsUp)
        {
            //current time
            UpdateText();
        }
    }

    public void HeadsetUp()
    {
        Audio.SendMessage("PlayMusic");
        this.InvokeRepeating("ReduceTime", 0, 1.0f);

        printer.timeSeries.Clear();
    }

    public void HeadsetDown()
    {
        Audio.SendMessage("StopMusic");
        this.CancelInvoke();

        printer.timeSeries.Add(Years.text + "Y" + Months.text
                               + "M" + Days.text + "D " + Hours.text + ":" +
                               Minutes.text + ":" + Seconds.text);

        printer.Print2();
    }

    private void ReduceTime()
    {
        Audio.SendMessage("PlayTick");
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

        printer.timeSeries.Add(Years.text + "Y" + Months.text
                               + "M" + Days.text + "D " + Hours.text + ":" +
                               Minutes.text + ":" + Seconds.text);
    }

    public void Reset()
    {
        currentSecond = 40;
        currentHour = 13;
        currentMinute = 10;
        currentDay = 20;
        currentMonth = 5;
        currentYear = 23;
        UpdateText();
    }

    private void checkSensorAndCall()
    {
        CurrentInput = SensorInput.Value > Threshold ? 1 : 0;
        int delta = CurrentInput - LastInput;
        LastInput = CurrentInput;
        if (Math.Abs(delta) != 0)
        {
            if (EarphoneIsUp)
            {
                HeadsetDown();
            }
            else if (!EarphoneIsUp)
            {
                HeadsetUp();
            }
            EarphoneIsUp = !EarphoneIsUp;
        }
    }

    private void UpdateText()
    {
        Hours.text = string.Format("{0:D2}", currentHour);
        Seconds.text = string.Format("{0:D2}", currentSecond);
        Minutes.text = string.Format("{0:D2}", currentMinute);
        Years.text = string.Format("{0:D2}", currentYear);
        Months.text = string.Format("{0:D2}", currentMonth);
        Days.text = string.Format("{0:D2}", currentDay);
    }
}