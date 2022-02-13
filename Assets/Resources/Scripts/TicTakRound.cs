using System;
using System.Collections.Generic;

namespace TicTakToe
{
	public enum WinAxis
	{
		None,
		Horizontal,
		Vertical,
		DiagonalUL,
		DiagonalDL
	}

	public class TicTakRound
	{
		private TicTakGrid _ticTakGrid;	
		private int _rowCountToWin = 3;
		private bool _isItExTurne; //is player turne
		private List<TicTakCell> _winCells;
		private int _columnCount;
		
		public Action<bool> RoundEnd;

		public TicTakRound (TicTakGrid ticTakGrid)
		{
			_ticTakGrid = ticTakGrid;
			_rowCountToWin = TicTakData.Instance.CountToWin;
			_columnCount = TicTakData.Instance.Columns;

			for (int i = 0; i < _ticTakGrid.Cells.Length; i++)
			{
				_ticTakGrid.Cells[i].OnClick += OnCellClicked;
			}

			_isItExTurne = UnityEngine.Random.Range(0, 2) == 1;

			_winCells = new List<TicTakCell>(_rowCountToWin);

			RoundEnd += OnRoundEnd;
		}

		
		private void OnRoundEnd(bool isExWin)
		{
			for (int i = 0; i < _ticTakGrid.Cells.Length; i++)
			{
				_ticTakGrid.Cells[i].OnClick -= OnCellClicked;
			}
			RoundEnd -= OnRoundEnd;
		}

		private void OnCellClicked(TicTakCell cell)
		{
			if (_isItExTurne)
			{
				if (cell.SetX())
				{
					var winAxis = IsItTheEnd(cell);
					if (winAxis != WinAxis.None)
					{
						for (int i = 0; i < _winCells.Count; i++)
						{
							_winCells[i].SetCrossLine(winAxis);
						}
						RoundEnd?.Invoke(_isItExTurne);
					}
					_isItExTurne = !_isItExTurne;
				}
			}
			else
			{
				var AICell = GetAIMove();

				if (AICell.SetO())
				{
					var winAxis = IsItTheEnd(AICell);
					if (winAxis != WinAxis.None)
					{
						for (int i = 0; i < _winCells.Count; i++)
						{
							_winCells[i].SetCrossLine(winAxis);
						}
						RoundEnd?.Invoke(_isItExTurne);
					}
					_isItExTurne = !_isItExTurne;
				}


				//if (cell.SetO())
				//{
				//	var winAxis = IsItTheEnd(cell);
				//	if (winAxis != WinAxis.None)
				//	{
				//		for (int i = 0; i < _winCells.Count; i++)
				//		{
				//			_winCells[i].SetCrossLine(winAxis);
				//		}
				//		if (RoundEnd != null) RoundEnd(_isItExTurne);
				//	}
				//	_isItExTurne = !_isItExTurne;
				//}
			}
		}

		private WinAxis IsItTheEnd(TicTakCell cell)
		{
			_winCells.Clear();

			_winCells.Add(cell);
			AddCellsOnShift(cell, cell.State, -1, true);    // Add cells on Left
			AddCellsOnShift(cell, cell.State, 1, true);     // Add cells on Right
			if (_winCells.Count >= _rowCountToWin) return WinAxis.Horizontal;
			else _winCells.Clear();

			_winCells.Add(cell);
			AddCellsOnShift(cell, cell.State, -_columnCount);    // Add cells on Up
			AddCellsOnShift(cell, cell.State, _columnCount);     // Add cells on Down
			if (_winCells.Count >= _rowCountToWin) return WinAxis.Vertical;
			else _winCells.Clear();

			_winCells.Add(cell);
			AddCellsOnShift(cell, cell.State, -_columnCount - 1);  // Add cells on diagonal UL
			AddCellsOnShift(cell, cell.State, _columnCount + 1);   // Add cells on diagonal DR
			if (_winCells.Count >= _rowCountToWin) return WinAxis.DiagonalUL;
			else _winCells.Clear();

			_winCells.Add(cell);
			AddCellsOnShift(cell, cell.State, _columnCount - 1);   // Add cells on diagonal DL
			AddCellsOnShift(cell, cell.State, -_columnCount + 1);   // Add cells on diagonal UR
			if (_winCells.Count >= _rowCountToWin) return WinAxis.DiagonalDL;
			else _winCells.Clear();


			return WinAxis.None;
		}

		private void AddCellsOnShift(TicTakCell cell, CellState state, int shift, bool isNeedTheSameLine = false)
		{
			var index = cell.Index + shift;

			if (index < 0 || index >= _ticTakGrid.Cells.Length) return;

			if (isNeedTheSameLine != (cell.Index / _columnCount == index / _columnCount)) return;

			//Check for diagonal shift
			if (Math.Abs(cell.Index / _columnCount - index / _columnCount) >= 2) return;

			if (cell.State != _ticTakGrid.Cells[index].State) return;

			_winCells.Add(_ticTakGrid.Cells[index]);
			AddCellsOnShift(_ticTakGrid.Cells[index], state, shift, isNeedTheSameLine);
		}

		private TicTakCell GetAIMove()
		{
			var cellsList = new List<TicTakCell>(_ticTakGrid.Cells.Length);
			for (int i = 0; i < _ticTakGrid.Cells.Length; i++)
			{
				if (_ticTakGrid.Cells[i].State == CellState.None)
					cellsList.Add(_ticTakGrid.Cells[i]);
			}
			return cellsList[UnityEngine.Random.Range(0, cellsList.Count)];
		}
	}
}
