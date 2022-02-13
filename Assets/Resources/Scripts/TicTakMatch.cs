using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TicTakToe
{
	public class TicTakMatch : MonoBehaviour
	{
		[SerializeField] private string _loadSceneAfterMatch = "MainMenu";

		private void Awake()
		{
			TicTakRound.RoundEnd += OnRoundEnd;
		}

		private void OnDestroy()
		{
			TicTakRound.RoundEnd -= OnRoundEnd;
		}

		private void OnRoundEnd(bool isPlayerWin)
		{
			StartCoroutine(EndMatchRoutine(isPlayerWin));
			TicTakData.Instance.MatchPlayed = true;
			TicTakData.Instance.IsPlayerWin = isPlayerWin;
		}

		private IEnumerator EndMatchRoutine(bool isPlayerWin)
		{
			Bloker.Instance.Blok();
			yield return new WaitForSeconds(2);
			var message = isPlayerWin ? "You won! Hey!" : "You lose. Next time.";
			Bloker.Instance.Message(message);
			yield return new WaitForSeconds(2);
			Bloker.Instance.RemoveMessage();
			yield return new WaitForSeconds(2);
			//LoadingScreen.LoadScene(_loadSceneAfterMatch);
		}


	}
}
