using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    public bool IsMusic;

    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (GameState.Paused) return;

	    audioSource.volume = IsMusic ? GameState.MusicVolume : GameState.SoundVolume;
	}
}
