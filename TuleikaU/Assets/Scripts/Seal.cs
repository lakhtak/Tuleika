using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Seal : MonoBehaviour {

	public float Speed;
	public float RotationSpeed;
	public int ChildDistance;
	public int MaxChildren;
	public KeyCode LeftKey;
	public KeyCode RightKey;
    public GameObject SealChildPrefab;
    public GameObject LonelyChildPrefab;
    public Text ScoreText;
    public string WinScene;
    public int PlayerNumber;

    public AudioClip Crunch1;
    public AudioClip Crunch2;
    public AudioClip Crunch3;
    public AudioClip SealHit;
    public AudioClip Escape;

    private int score;
	
	public List<GameObject> sealChildren = new List<GameObject>();
    
    private readonly List<Place> lastPositions = new List<Place>();
    private AudioSource audioSource;
    private AudioClip[] crunches;

    private Transform myTransform;

	// Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	    crunches = new[] {Crunch1, Crunch2, Crunch3};
	}

    void Awake()
    {
        myTransform = transform;
    }

	// Update is called once per frame
	void Update ()
	{
        if (GameState.Paused) return;

	    if (score >= MaxChildren)
	    {
	        Win();
	    }

        myTransform.Translate(Vector2.right * Speed);

		if(Input.GetKey(RightKey))
		{
            myTransform.Rotate(Vector3.back * RotationSpeed);
		}
		if(Input.GetKey(LeftKey))
		{
            myTransform.Rotate(Vector3.back * -1 * RotationSpeed);
        }
	    if (Input.GetKey(KeyCode.Escape))
	    {
            audioSource.PlayOneShot(Escape);
	        SceneManager.LoadScene("Menu");
	    }

        lastPositions.Insert(0, new Place { position = myTransform.localPosition, rotation = myTransform.localRotation });
		if (lastPositions.Count > ChildDistance * MaxChildren) {
            lastPositions.RemoveAt(lastPositions.Count - 1);
		}
	}

    private void Win()
    {
        GameState.PlayerNumber = PlayerNumber;
        GameState.Score = score;
        GameState.Time = TimeUpdater.CurrentTime;
        SceneManager.LoadScene(WinScene);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Food"))
		{
		    audioSource.PlayOneShot(crunches[Random.Range(0, crunches.Length - 1)]);
			AddChild ();
			GameObject.FindGameObjectWithTag("Timeline").GetComponent<Timeline> ().setCurrentTime (10);
			Destroy (other.gameObject);
		} 
		else if (other.CompareTag ("HorizontalBound")) 
		{
            myTransform.position = new Vector2(myTransform.position.x, myTransform.position.y * -1);
		}
        else if (other.CompareTag("VerticalBound"))
        {
            myTransform.position = new Vector2(myTransform.position.x * -1, myTransform.position.y);
        }
        else if (other.CompareTag("Child") && sealChildren.Contains(other.gameObject))
        {
            audioSource.PlayOneShot(SealHit);

            SealChild sealChild = other.GetComponent<SealChild>();
            int childrenToRemove = sealChildren.Count - sealChild.Number + 1;
            
            score = score - childrenToRemove;
            ScoreText.text = score.ToString(CultureInfo.InvariantCulture);

            for (int i = 0; i < childrenToRemove; i++)
            {
                var currentChild = sealChildren.Last();
                Instantiate(LonelyChildPrefab, currentChild.transform.position, currentChild.transform.rotation);
                Destroy(currentChild);
                sealChildren.RemoveAt(sealChildren.Count - 1);
            }
        }
    }

	public void AddChild()
	{
		score++;
        ScoreText.text = score.ToString(CultureInfo.InvariantCulture);

	    Place newChildPlace = GetChildPlace(sealChildren.Count);
	    GameObject childObject = Instantiate(SealChildPrefab, newChildPlace.position, newChildPlace.rotation);
	    childObject.GetComponent<SealChild>().MainSeal = GetComponent<Seal>();
        sealChildren.Add(childObject);
	}
		
	public Place GetChildPlace(int childNumber)
	{
		if (lastPositions.Count <= childNumber * ChildDistance)
            return lastPositions[lastPositions.Count - 1];

		return lastPositions [childNumber * ChildDistance];
	}
}
