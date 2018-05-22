using Ardunity;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Driver3 : MonoBehaviour ,Driver
{
    public Text Hours;
    public Text Seconds;
    public Text Minutes;
    public GameObject Audio;
    public float Threshold;

    private bool EarphoneIsUp;
    private DateTime startTime;
    private DateTime stopTime;
    private int secondsPassed;

    private string originSecond;
    private bool isVanish;
    private Printer printer;
    public AnalogInput SensorInput;
    private int LastInput;
    private int CurrentInput;
    private int testSeconds = 300;

    // Use this for initialization
    void Start ()
    {
        EarphoneIsUp = false;

        printer = new Printer();

        UpdateText();

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
	    else if(Input.GetKeyDown((KeyCode.Escape)))
	    {
	        Application.Quit();
	    }

        checkSensorAndCall();

        if (!EarphoneIsUp)
        {
            UpdateText();
        }
    }

    public void HeadsetUp()
    {
        Audio.SendMessage("PlayMusic");

        printer.setPara(Hours.text, Minutes.text, Seconds.text);

        originSecond = Seconds.text;
        isVanish = false;
        InvokeRepeating("Flash",0f,0.5f);

        startTime = DateTime.Now;
    }

    public void HeadsetDown()
    {
        Audio.SendMessage("StopMusic");
        this.CancelInvoke();
        Seconds.text = originSecond;
        isVanish = false;

        stopTime = DateTime.Now;
        secondsPassed = (int)(stopTime - startTime).TotalSeconds + 1;
        
        printer.Print3(secondsPassed);
    }

    private void Flash()
    {
        if (isVanish)
        {
            Seconds.text = "";
        }
        else
        {
            Seconds.text = originSecond;
        }

        isVanish = !isVanish;
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
            else if(!EarphoneIsUp)
            {
                HeadsetUp();
            }
            EarphoneIsUp = !EarphoneIsUp;
        }
    }

    public void Reset()
    {
        
    }

    private void UpdateText()
    {
        Hours.text = DateTime.Now.ToString("HH");
        Seconds.text = DateTime.Now.ToString("ss");
        Minutes.text = DateTime.Now.ToString("mm");
    }
}
