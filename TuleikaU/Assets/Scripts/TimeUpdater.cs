using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpdater : MonoBehaviour
{
    public Text TimeDisplay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    TimeDisplay.text = Time.fixedTime.ToString(CultureInfo.InvariantCulture);
	}
}
