using UnityEngine;

public class SealChild : MonoBehaviour {

	public Seal MainSeal;
	public int Number;

    private Transform myTransform;

	// Use this for initialization
	void Start () {
        Number = MainSeal.sealChildren.Count;
	}

    void Awake()
    {
        myTransform = transform;
    }

	// Update is called once per frame
	void Update () {
        if (GameState.Paused) return;

        var myPlace = MainSeal.GetChildPlace(Number);
        myTransform.localPosition = myPlace.position;
        myTransform.localRotation = myPlace.rotation;
	}
}