using UnityEngine;
using System.Collections;

public class GameStats {

	static int score = 0;

	public static void setScore(int newScore)
	{
		score = newScore;
	}

	public static int GetScore()
	{
		return score;
	}

	public static void IncreaseScore(int amount)
	{
		score += amount;
	}
}
