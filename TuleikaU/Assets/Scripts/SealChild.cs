using UnityEngine;

public class SealChild : MonoBehaviour {

	public Seal mainSeal;

	public int number;

	// Use this for initialization
	void Start () {
		mainSeal = GameObject.FindGameObjectWithTag("Seal").GetComponent<Seal>();
		number = mainSeal.sealChildren.Count;
	}
	
	// Update is called once per frame
	void Update () {
        var myPlace = mainSeal.GetChildPlace(number);
		transform.localPosition = myPlace.position;
		transform.localRotation = myPlace.rotation;
	}
}