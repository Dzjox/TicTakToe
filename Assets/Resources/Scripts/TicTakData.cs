using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTakToe
{
	public class TicTakData : MonoBehaviour
	{
		private static TicTakData _instance;
		public static TicTakData Instance { get { return _instance; } }

		private void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(this.gameObject);
			}
			else
			{
				_instance = this;
			}

			GetDataFromPlayerPrefs();
			DontDestroyOnLoad(this.gameObject);
		}

		public int Columns { get; set; }
		public int Rows { get; set; }
		public int CountToWin { get; set; }
		public int Score { get; set; }
		public bool MatchPlayed { get; set; }
		public bool IsPlayerWin { get; set; }

		private void GetDataFromPlayerPrefs()
		{
			Columns = PlayerPrefs.GetInt("ColumsCount", 3);
			Rows = PlayerPrefs.GetInt("RowsCount", 3);
			CountToWin = PlayerPrefs.GetInt("CountToWin", 3);
			Score = PlayerPrefs.GetInt("PlayerScore", 3);
		}
	}
}
