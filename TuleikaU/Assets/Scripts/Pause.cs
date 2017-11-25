using UnityEngine;

public class Pause : MonoBehaviour {

    public KeyCode PauseKey = KeyCode.P;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!Input.GetKeyDown(PauseKey)) return;

        GameState.Paused = !GameState.Paused;

	    Time.timeScale = GameState.Paused ? 0f : 1f;
	}
}
