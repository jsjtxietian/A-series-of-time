using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

public class Printer
{
    #region scene3
    private string hour;
    private string minute;
    private string second;

    public void setPara(int h, int m, int s)
    {
        hour = string.Format("{0:D2}", h);
        minute = string.Format("{0:D2}", m);
        second = string.Format("{0:D2}", s);
    }

    public void setPara(string h, string m, string s)
    {
        hour = h;
        minute = m;
        second = s;
    }

    public void Print3(int secondsPassed)
    {
        StreamWriter sw = new StreamWriter("D:\\OUTPUT3.txt");
        sw.Write("FIRST TIME I\n————————————\n");

        int index = 0;
        string single = hour + ":" + minute + ":" + second + "  ";

        while (secondsPassed != 0)
        {
            secondsPassed--;
            sw.Write(single);

            if (index % 3 == 2)
                sw.Write('\n');

            index++;
        }

        if ((index - 1) % 3 != 2)
            sw.Write('\n');

        sw.Write("————————————\n");
        sw.Write("	        WANG WEIYE\n");
        sw.Write("	                      2018");

        sw.Dispose();

        CallPrint("D:\\OUTPUT3.txt");
    }

    #endregion


    #region Scene1&2

    public List<string> timeSeries = new List<string>();

    public void Print1(int secondsPassed)
    {
        StreamWriter sw = new StreamWriter("D:\\OUTPUT1.txt");
        sw.Write("I MEAN NOTHING\nWITHOUT YOU\n—————————————");

        int index = 0;

        foreach (var single in timeSeries)
        {
            sw.Write(single);
            if (index % 3 == 2)
                sw.Write('\n');

            index++;
        }

        if ((index - 1) % 3 != 2)
            sw.Write('\n');

        sw.Write("—————————————\n");
        sw.Write("	        WANG WEIYE\n");
        sw.Write("	                      2018");

        sw.Dispose();

        CallPrint("D:\\OUTPUT1.txt");
    }

    public void Print2()
    {
        StreamWriter sw = new StreamWriter("D:\\OUTPUT2.txt");
        sw.Write("AWAY FROM 1994\n—————————————");


        foreach (var single in timeSeries)
        {
            sw.Write(single);
            sw.Write('\n');
        }

        sw.Write("—————————————\n");
        sw.Write("	        WANG WEIYE\n");
        sw.Write("	                      2018");

        sw.Dispose();

        CallPrint("D:\\OUTPUT2.txt");
    }

    #endregion

    private void CallPrint(string path)
    {
        Process pr = new Process();
        pr.StartInfo.FileName = path;
        pr.StartInfo.CreateNoWindow = true;
        pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        pr.StartInfo.Verb = "Print";
        pr.Start();
    }

}
