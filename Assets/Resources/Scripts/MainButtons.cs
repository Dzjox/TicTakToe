using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainButtons : UIBehaviour
{
	[SerializeField] private string _gameSceneName;

	public void OnNewRoundClicked()
	{
		LoadingScreen.LoadScene(_gameSceneName);
	}

	public void OnExitClicked()
	{
		Application.Quit();
	}
}
