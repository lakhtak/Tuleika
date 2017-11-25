using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpdater : MonoBehaviour
{
    public Text TimeDisplay;

    public static int CurrentTime;

	// Use this for initialization
	void Start ()
	{
	    CurrentTime = 0;
        StartCoroutine(UpdateTimer());		
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

    IEnumerator UpdateTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            CurrentTime++;
            TimeDisplay.text = CurrentTime.ToString(CultureInfo.InvariantCulture);
        }
    }
}
