using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class FillResults : MonoBehaviour
{
    public Text ScoreText;
    public Text TimeText;
    public Text PlayerNumberText;

	// Use this for initialization
	void Start ()
	{
        ScoreText.text = GameState.Score.ToString(CultureInfo.InvariantCulture);
        TimeText.text = GameState.Time.ToString(CultureInfo.InvariantCulture);
        if (PlayerNumberText)
            PlayerNumberText.text = GameState.PlayerNumber.ToString(CultureInfo.InvariantCulture);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
