using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MainOptions : UIBehaviour
{
	[SerializeField] private TMP_InputField _columns;
	[SerializeField] private TMP_InputField _rows;
	[SerializeField] private TMP_InputField _countToWin;
	[SerializeField] private ScoreCounter _scoreCounter;

	protected override void Awake()
	{
		_columns.text = PlayerPrefs.GetInt("ColumsCount", 3).ToString();
		_rows.text = PlayerPrefs.GetInt("RowsCount", 3).ToString();
		_countToWin.text = PlayerPrefs.GetInt("CountToWin", 3).ToString();
	}

	public void OnColumnValueChange(string value)
	{
		int.TryParse(value, out int count);
		if (count < 3) count = 3;

		TicTakData.Instance.Columns = count;
		PlayerPrefs.SetInt("ColumsCount", count);
	}

	public void OnRowValueChange(string value)
	{
		int.TryParse(value, out int count);
		if (count < 3) count = 3;

		TicTakData.Instance.Rows = count;
		PlayerPrefs.SetInt("RowsCount", count);
	}

	public void OnCTWValueChange(string value)
	{
		int.TryParse(value, out int count);
		if (count < 3) count = 3;

		TicTakData.Instance.CountToWin = count;
		PlayerPrefs.SetInt("CountToWin", count);
	}

	public void OnResetButtonClicked ()
	{
		_scoreCounter.ResetScore();
	}
}
