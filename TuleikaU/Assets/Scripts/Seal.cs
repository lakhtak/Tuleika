using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	
    public List<Place> lastPositions = new List<Place>();
	public List<GameObject> sealChildren = new List<GameObject>();

	public Text ScoreText;
	public int Score = 0;

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * Speed);

		if(Input.GetKey(RightKey))
		{
            transform.Rotate(Vector3.back * RotationSpeed);
		}
		if(Input.GetKey(LeftKey))
		{
            transform.Rotate (Vector3.back * -1 * RotationSpeed);
        }

		lastPositions.Insert (0, new Place { position = transform.localPosition, rotation = transform.localRotation });
		if (lastPositions.Count > ChildDistance * MaxChildren) {
			lastPositions.Remove (lastPositions.Last ());
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
            transform.position = new Vector2(transform.position.x, transform.position.y * -1);
		}
        else if (other.CompareTag("VerticalBound"))
        {
            transform.position = new Vector2(transform.position.x * -1, transform.position.y);
        }
        else if (other.CompareTag("Child"))
        {
            var sealChild = other.GetComponent<SealChild>();
            var childrenToRemove = sealChildren.Count - sealChild.number;
            for (var i = 0; i <= childrenToRemove; i++)
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
		Score++;

		var newChildPlace = GetChildPlace (Score);
		sealChildren.Add (Instantiate (SealChildPrefab, newChildPlace.position, newChildPlace.rotation));
	}
		
	public Place GetChildPlace(int childNumber)
	{
		if (lastPositions.Count <= childNumber * ChildDistance)
			return lastPositions.Last ();

		return lastPositions [childNumber * ChildDistance];
	}
}
