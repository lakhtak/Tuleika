using System;
using System.Globalization;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Seal : MonoBehaviour {

	public float Speed = 10;
	public float RotationSpeed = 300;
	public int ChildDistance = 20;
	public int MaxChildren = 100;
	public KeyCode LeftKey = KeyCode.A;
	public KeyCode RightKey = KeyCode.D;
    public GameObject SealChildPrefab;
    public GameObject LonelyChildPrefab;
    public Text ScoreText;
    public string WinScene;
    public int PlayerNumber;
	
	public List<GameObject> sealChildren = new List<GameObject>();
    
    private readonly List<Place> lastPositions = new List<Place>();
    public int score;

    private Transform myTransform;

	// Use this for initialization
	void Start ()
	{
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
            GameState.PlayerNumber = PlayerNumber;
            GameState.Score = score;
            GameState.Time = (int)Time.fixedTime;
            SceneManager.LoadScene(WinScene);
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
	        SceneManager.LoadScene("Menu");
	    }

        lastPositions.Insert(0, new Place { position = myTransform.localPosition, rotation = myTransform.localRotation });
		if (lastPositions.Count > ChildDistance * MaxChildren) {
            lastPositions.RemoveAt(lastPositions.Count - 1);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Food")) 
		{
			AddChild ();
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
