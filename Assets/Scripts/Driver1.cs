using System;
using UnityEngine;
using UnityEngine.UI;

public class Driver1 : MonoBehaviour
{
    public Text Hours;
    public Text Seconds;
    public Text Minutes;
    public GameObject Audio;

    private bool isStop;
    private DateTime startTime;
    private DateTime stopTime;
    private int secondsPassed;

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

    // Use this for initialization
    void Start ()
    {
        Hours.text = string.Format("{0:D3}", currentHour) + " :";
        Seconds.text = string.Format("{0:D2}", currentSecond);
        Minutes.text = string.Format("{0:D2}", currentMinute) + " :";
    }
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKeyDown(KeyCode.A))
	    {
	        HeadsetUp();
	    }
        else if (Input.GetKeyDown((KeyCode.B)))
	    {
	        HeadsetDown();
	    }

	    if (isStop)
	    {
	        //current time
	        Hours.text = string.Format("{0:D3}",currentHour) + " :" ;
	        Seconds.text = string.Format("{0:D2}",currentSecond) ;
	        Minutes.text = string.Format("{0:D2}",currentMinute) +  " :";
	    }
    }

    public void HeadsetUp()
    {
        isStop = !isStop;
        Audio.SendMessage("Play");
        this.InvokeRepeating("AddTime",0,1.0f);
        startTime = DateTime.Now;
    }

    public void HeadsetDown()
    {
        isStop = !isStop;
        Audio.SendMessage("Stop");
        this.CancelInvoke();

        stopTime = DateTime.Now;
        secondsPassed = (int)(stopTime - startTime).TotalSeconds;
        Debug.Log(secondsPassed);
    }

    private void AddTime()
    {
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
    }

    public void Reset()
    {
        currentSecond = 0;
        currentHour = 0;
        currentHour = 0;
    }
}
