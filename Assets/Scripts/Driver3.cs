using System;
using UnityEngine;
using UnityEngine.UI;

public class Driver3 : MonoBehaviour
{
    public Text Hours;
    public Text Seconds;
    public Text Minutes;
    public GameObject Audio;

    private bool isStop;
    private DateTime startTime;
    private DateTime stopTime;
    private int secondsPassed;

    private string originSecond;
    private bool isVanish;

    // Use this for initialization
    void Start ()
    {
        isStop = false;
    }
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKeyDown(KeyCode.A))
	    {
	        HeadsetUp();
	    }else if (Input.GetKeyDown((KeyCode.B)))
	    {
	        HeadsetDown();
	    }

	    if (!isStop)
	    {
	        //current time
	        Hours.text = DateTime.Now.ToString("hh") + " :";
	        Seconds.text = DateTime.Now.ToString("ss");
	        Minutes.text = DateTime.Now.ToString("mm") + " :";
        }
    }

    public void HeadsetUp()
    {
        isStop = !isStop;
        Audio.SendMessage("Play");

        originSecond = Seconds.text;
        isVanish = false;
        InvokeRepeating("Flash",0f,0.5f);

        startTime = DateTime.Now;
    }

    public void HeadsetDown()
    {
        isStop = !isStop;
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
}
