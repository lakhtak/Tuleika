using UnityEngine;

public class LonelyChild : MonoBehaviour {

    public float Speed = 0.25f;

    private Transform myTransform;

    // Use this for initialization
	void Start () {
        transform.Rotate(Vector3.forward, Random.Range(0, 359));
	}

    void Awake()
    {
        myTransform = transform;
    }

	// Update is called once per frame
	void Update () {
        if (GameState.Paused) return;

        myTransform.Translate(Vector2.right * Speed);		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HorizontalBound") || other.CompareTag("VerticalBound"))
        {
            Destroy(gameObject);
        }
    }
}
