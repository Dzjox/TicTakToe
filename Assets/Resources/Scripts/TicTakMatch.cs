using Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TicTakToe
{
	public class TicTakMatch : MonoBehaviour
	{
		[SerializeField] private TicTakGrid _ticTakGrid;

		private TicTakRound _ticTakRound;

		private void Awake()
		{
			_ticTakGrid.Init();
			_ticTakRound = new TicTakRound(_ticTakGrid);
			_ticTakRound.RoundEnd += OnRoundEnd;
		}

		private void OnDestroy()
		{
			_ticTakRound.RoundEnd -= OnRoundEnd;
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
			LoadingScreen.Instance.Load(new LoadMainMenu());
			yield return new WaitForSeconds(0.5f);
			Bloker.Instance.RemoveMessage();
			Bloker.Instance.UnBlok();
		}


	}
}
