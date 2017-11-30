﻿using UnityEngine;

public class Pause : MonoBehaviour {

    public KeyCode PauseKey = KeyCode.P;
    public AudioClip PauseSound;
    public AudioClip UnpauseSound;
    public float PauseVolume;
    public AudioSource ParentAudioSource;

    private AudioSource audioSource;

    // Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!Input.GetKeyDown(PauseKey)) return;

	    if (GameState.Paused)
	    {
            ParentAudioSource.volume = GameState.MusicVolume;
            audioSource.PlayOneShot(UnpauseSound);
            Time.timeScale = 1f;
	    }
	    else
	    {
            audioSource.PlayOneShot(PauseSound);
            ParentAudioSource.volume = GameState.MusicVolume * PauseVolume;
            Time.timeScale = 0f;	        
	    }

        GameState.Paused = !GameState.Paused;
    }
}
