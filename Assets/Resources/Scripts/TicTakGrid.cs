using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TicTakToe
{
	public class TicTakGrid : MonoBehaviour
	{
		[SerializeField] private GameObject _cellPrefab;
		[SerializeField] private GameObject _linePrefab;
		[SerializeField] private RectTransform _backgroundTransform;
		[SerializeField] private RectTransform _gridTransform;
		[SerializeField] private HorizontalLayoutGroup _backgroundHorizontal;
		[SerializeField] private VerticalLayoutGroup _backgroundVertical;
		[SerializeField] private GridLayoutGroup _gridLayoutGroup;

		[Space]
		[SerializeField] private float _lineThickness = 15;
		[SerializeField] private int _rowCount = 3;
		[SerializeField] private int _columnCount = 3;

		public TicTakCell[] Cells { get; private set; }

		public void Init()
		{
			_rowCount = TicTakData.Instance.Rows;
			_columnCount = TicTakData.Instance.Columns;

			var height = _gridTransform.rect.height;
			var width = _gridTransform.rect.width;

			var cellSize = (int)Mathf.Min(
				(height - _lineThickness * (_rowCount - 1)) / _rowCount,
				(width - _lineThickness * (_columnCount - 1)) / _columnCount);

			_gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
			_gridLayoutGroup.spacing = new Vector2(_lineThickness, _lineThickness);
			_gridLayoutGroup.constraintCount = _columnCount;

			height = (cellSize + _lineThickness) * _rowCount - _lineThickness;
			width = (cellSize + _lineThickness) * _columnCount - _lineThickness;

			_backgroundTransform.sizeDelta = new Vector2(width, height);
			_backgroundVertical.padding = new RectOffset(0, 0, cellSize, cellSize);
			_backgroundVertical.spacing = cellSize;
			_backgroundHorizontal.padding = new RectOffset(cellSize, cellSize, 0, 0);
			_backgroundHorizontal.spacing = cellSize;


			Cells = new TicTakCell[_rowCount * _columnCount];
			for (int i = 0; i < _rowCount * _columnCount; i++)
			{
				Cells[i] = Instantiate(_cellPrefab, transform).GetComponent<TicTakCell>();
				Cells[i].Index = i;
			}

			for (int i = 0; i < (_rowCount - 1); i++)
			{
				var line = Instantiate(_linePrefab, _backgroundVertical.transform);
				line.SetActive(true);
				line.GetComponent<RectTransform>().sizeDelta = new Vector2(_lineThickness, _lineThickness);
			}

			for (int i = 0; i < (_columnCount - 1); i++)
			{
				var line = Instantiate(_linePrefab, _backgroundHorizontal.transform);
				line.SetActive(true);
				line.GetComponent<RectTransform>().sizeDelta = new Vector2(_lineThickness, _lineThickness);
			}

		}

	}
}