using Ardunity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Driver3 : MonoBehaviour
{
    public Text Hours;
    public Text Seconds;
    public Text Minutes;
    public GameObject Audio;

    private bool EarphoneIsUp;
    private DateTime startTime;
    private DateTime stopTime;
    private int secondsPassed;

    private string originSecond;
    private bool isVanish;

    public AnalogInput SensorInput;
    private float LastInput;
    private float CurrentInput;
    private int testSeconds = 300;

    // Use this for initialization
    void Start ()
    {
        EarphoneIsUp = false;
    }
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    HeadsetUp();
        //}else if (Input.GetKeyDown((KeyCode.B)))
        //{
        //    HeadsetDown();
	    //}
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
        checkSensorAndCall();

        if (!EarphoneIsUp)
	    {
            //current time
            //Hours.text = DateTime.Now.ToString("HH") + " :";
	        Hours.text = DateTime.Now.ToString("HH");
            //Seconds.text = ": "+DateTime.Now.ToString("ss");
            Seconds.text = DateTime.Now.ToString("ss");
            Minutes.text = DateTime.Now.ToString("mm");
        }
    }

    public void HeadsetUp()
    {
        Audio.SendMessage("Play");

        originSecond = Seconds.text;
        isVanish = false;
        InvokeRepeating("Flash",0f,0.5f);

        startTime = DateTime.Now;
    }

    public void HeadsetDown()
    {
        Audio.SendMessage("Stop");
        this.CancelInvoke();
        Seconds.text = originSecond;
        isVanish = false;

        stopTime = DateTime.Now;
        secondsPassed = (int)(stopTime - startTime).TotalSeconds + 1;
        Debug.Log(secondsPassed);
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
