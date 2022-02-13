using UnityEngine;

namespace TicTakToe
{
	public class TicTakData
	{
		private static TicTakData _instance;
		public static TicTakData Instance
		{
			get 
			{
				if (_instance == null)
					_instance = new TicTakData();
				return _instance;
			}
		}

		private TicTakData()
		{
			GetDataFromPlayerPrefs();
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
			Score = PlayerPrefs.GetInt("PlayerScore", 0);
		}
	}
}
