using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TicTakToe
{
	public enum CellState
	{
		None,
		X,
		O
	}
	public class TicTakCell : UIBehaviour, IPointerClickHandler
	{
		[SerializeField] private Image _image;
		[SerializeField] private RectTransform _crossLine;
		[SerializeField] private Sprite _spriteX;
		[SerializeField] private Sprite _spriteO;

		public event Action<TicTakCell> OnClick;

		public int Index { get; set; }
		public CellState State { get; private set; }

		public void OnPointerClick(PointerEventData eventData)
		{
			if (OnClick != null) OnClick(this);
		}

		public bool SetX()
		{
			if (State != CellState.None) return false;

			_image.color = Color.white;
			_image.sprite = _spriteX;
			State = CellState.X;
			return true;
		}

		public bool SetO()
		{
			if (State != CellState.None) return false;

			_image.color = Color.white;
			_image.sprite = _spriteO;
			State = CellState.O;
			return true;
		}

		public void Clear()
		{
			_image.color = new Color(1, 1, 1, 0);
			State = CellState.None;
			SetCrossLine(WinAxis.None);
		}

		public void SetCrossLine(WinAxis winAxis)
		{
			switch (winAxis)
			{
				case WinAxis.Horizontal:
					_crossLine.gameObject.SetActive(true);
					break;
				case WinAxis.Vertical:
					_crossLine.gameObject.SetActive(true);
					_crossLine.eulerAngles = new Vector3(0, 0, 90);
					break;
				case WinAxis.DiagonalUL:
					_crossLine.gameObject.SetActive(true);
					_crossLine.eulerAngles = new Vector3(0, 0, -45);
					_crossLine.localScale = new Vector3(1.5f, 1, 1);
					break;
				case WinAxis.DiagonalDL:
					_crossLine.gameObject.SetActive(true);
					_crossLine.eulerAngles = new Vector3(0, 0, 45);
					_crossLine.localScale = new Vector3(1.5f, 1, 1);
					break;
				case WinAxis.None:
				default:
					_crossLine.gameObject.SetActive(false);
					_crossLine.eulerAngles = new Vector3(0, 0, 1);
					_crossLine.localScale = new Vector3(1, 1, 1);
					break;
			}
		}
	}
}