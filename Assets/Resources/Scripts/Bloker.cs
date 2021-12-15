using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bloker : MonoBehaviour
{
	private static Bloker _instance;
	public static Bloker Instance { get { return _instance; } }

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

	}

	[SerializeField] private Image _image;
	[SerializeField] private GameObject _messageHolder;
	[SerializeField] private TextMeshProUGUI _text;


	public void Blok()
	{
		_image.enabled = true;
	}

	public void UnBlok()
	{
		_image.enabled = false;
	}

	public void Message(string message)
	{
		_messageHolder.SetActive(true);
		_text.text = message;
	}

	public void RemoveMessage()
	{
		_messageHolder.SetActive(false);
	}
}
