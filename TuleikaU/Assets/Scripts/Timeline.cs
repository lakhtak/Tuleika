using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeline : MonoBehaviour 
{

	public Slider slider;
	public float maxTime = 60;
	public float timeScaleRate;
	public float currentTime;
	// Use this for initialization
	void Start () 
	{
		slider = GetComponent<Slider> ();
		currentTime = maxTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentTime -= (Time.deltaTime+timeScaleRate);
		slider.value = currentTime / maxTime;
		if (currentTime <= 0)
			OnEndGame ();
	}

	void OnEndGame()
	{
	}
	public void setCurrentTime(float t)
	{
		currentTime = Mathf.Clamp (currentTime + t, 0, maxTime);
	}
}
