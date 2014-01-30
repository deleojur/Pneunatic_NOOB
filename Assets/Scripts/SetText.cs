using UnityEngine;
using System.Collections;

public class SetText : MonoBehaviour {

	private int currentScore = 0;

	public void SetScore (int score)
	{
		string scoreString = score.ToString();
		transform.GetComponent<TextMesh>().text = "SCORE: " + scoreString;
	}

	void Update()
	{
		if(GameStats.GetScore() > currentScore)
		{
			SetScore(GameStats.GetScore());
		}
	}
}
