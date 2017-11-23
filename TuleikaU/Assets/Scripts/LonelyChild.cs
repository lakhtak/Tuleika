using UnityEngine;

public class LonelyChild : MonoBehaviour {

    public float Speed = 10;
    
    // Use this for initialization
	void Start () {
        transform.Rotate(Vector3.forward, Random.Range(0, 359));
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HorizontalBound") || other.CompareTag("VerticalBound"))
        {
            Destroy(gameObject);
        }
    }
}
