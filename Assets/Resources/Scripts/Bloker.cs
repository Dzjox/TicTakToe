using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTakToe
{
	public class Bloker : MonoBehaviour
	{
		public static Bloker Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
			DontDestroyOnLoad(this);
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
}
