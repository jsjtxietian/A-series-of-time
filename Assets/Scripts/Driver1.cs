using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Ardunity;

public class Driver1 : MonoBehaviour,Driver
{
    public Text Hours;
    public Text Seconds;
    public Text Minutes;
    public GameObject Music;
    public AnalogInput SensorInput;

    private Printer printer;
    private int LastInput;
    private int CurrentInput;
    private bool EarphoneIsUp;
    private DateTime startTime;
    private DateTime stopTime;
    private int secondsPassed;
    private int testSeconds = 300;
    private float Threshold;

#region getset
    public int currentMinute
    {
        get { return PlayerPrefs.GetInt("currentMinute1", 0); }
        set { PlayerPrefs.SetInt("currentMinute1",value); PlayerPrefs.Save();}
    }

    public int currentSecond
    {
        get { return PlayerPrefs.GetInt("currentSecond1", 0); }
        set { PlayerPrefs.SetInt("currentSecond1", value); PlayerPrefs.Save(); }
    }

    public int currentHour
    {
        get { return PlayerPrefs.GetInt("currentHour1", 0); }
        set { PlayerPrefs.SetInt("currentHour1", value); PlayerPrefs.Save(); }
    }
#endregion
    // Use this for initialization
    void Start ()
    {
        UpdateText();
        EarphoneIsUp = false;

        printer = new Printer();
        StreamReader sr = new StreamReader("D:\\Threshold.txt");
        string s = sr.ReadLine();
        Threshold = float.Parse(s);
    }
	
	// Update is called once per frame
	void Update () {
	    if (testSeconds != 0)
	    {
	        testSeconds--;
	        if (testSeconds == 1)
	        {
	            LastInput = SensorInput.Value > Threshold ? 1 : 0;
                Debug.Log("first");
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
            UpdateText();
        }
    }

    public void HeadsetUp()
    {
        Music.SendMessage("PlayMusic");
        this.InvokeRepeating("AddTime",0,1.0f);
        startTime = DateTime.Now;
        printer.timeSeries.Clear();
    }

    public void HeadsetDown()
    {
        Music.SendMessage("StopMusic");
        this.CancelInvoke();

        stopTime = DateTime.Now;
        secondsPassed = (int)(stopTime - startTime).TotalSeconds;

        printer.timeSeries.Add(Hours.text + ":" + Minutes.text + ":" + Seconds.text + " ");
        printer.Print1(secondsPassed);
    }

    private void AddTime()
    {
        Music.SendMessage("PlayTick");
        currentSecond++;
        if (currentSecond == 60)
        {
            currentSecond = 0;
            currentMinute++;
        }
        if (currentMinute == 60)
        {
            currentMinute = 0;
            currentHour++;
        }

        printer.timeSeries.Add(Hours.text + ":" + Minutes.text + ":" + Seconds.text + " ");
    }

    public void Reset()
    {
        currentSecond = 0;
        currentHour = 0;
        currentMinute = 0;
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
        Hours.text = string.Format("{0:D3}", currentHour);
        Seconds.text = string.Format("{0:D2}", currentSecond);
        Minutes.text = string.Format("{0:D2}", currentMinute);
    }
}
