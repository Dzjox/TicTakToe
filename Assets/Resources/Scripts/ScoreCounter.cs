using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoreCounter : UIBehaviour
{
	[SerializeField] private TextMeshProUGUI _score;
	[SerializeField] private int _scoreForOneWin = 100;


	private void Awake()
	{
		CheckMatchResult();
		_score.text = "Score: " + PlayerPrefs.GetInt("PlayerScore", 0).ToString();
	}

	public void ResetScore()
	{
		_score.text = "Score: 0";

		TicTakData.Instance.Score = 0;
		PlayerPrefs.SetInt("CountToWin", 0);
	}

	private void CheckMatchResult()
	{
		if (TicTakData.Instance.MatchPlayed)
		{
			var shift = TicTakData.Instance.IsPlayerWin ? _scoreForOneWin : -_scoreForOneWin;

			TicTakData.Instance.Score += shift;
			PlayerPrefs.SetInt("PlayerScore", TicTakData.Instance.Score);
			_score.text = "Score: " + TicTakData.Instance.Score.ToString();

			TicTakData.Instance.MatchPlayed = false;
		}
	}
}
