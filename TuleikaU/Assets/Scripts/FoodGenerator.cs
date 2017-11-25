using UnityEngine;
using System.Collections;

public class FoodGenerator : MonoBehaviour {

	public float XSize;
	public float YSize;
	public GameObject foodPrefab;
	public GameObject currentFood;

	void Update()
	{
        if (GameState.Paused || currentFood)
			return;

		var randomPosition = new Vector2(Random.Range(XSize*-1,XSize),Random.Range(YSize*-1,YSize));
		currentFood = Instantiate(foodPrefab,randomPosition,Quaternion.identity);
	}
}
