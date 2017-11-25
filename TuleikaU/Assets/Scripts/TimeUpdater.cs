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
            TimeDisplay.text = ((int)Time.fixedTime).ToString(CultureInfo.InvariantCulture);
        }
    }
}
