using Loading;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace TicTakToe
{
	public class MainButtons : UIBehaviour
	{
		[SerializeField] private string _gameSceneName;

		public void OnNewRoundClicked()
		{
			LoadingScreen.Instance.Load(new LoadGameScene());
		}

		public void OnExitClicked()
		{
			Application.Quit();
		}
	}
}
