using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    public AudioClip SelectSound;
    public AudioClip ClickSound;
    
    private AudioSource audioSource;
    
	// Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(SelectSound);
    }

    // Update is called once per frame
	void Update () {
		
	}
}
