using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Loading
{
	public class LoadingScreen : MonoBehaviour
	{
		public static LoadingScreen Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}

		[SerializeField] private Image _fadeImage;
		[SerializeField] private GameObject _screenHolder;
		[SerializeField] private TextMeshProUGUI  _descriptionText;
		[SerializeField] private Slider _progressSlider;
		[SerializeField] private float _barSpeed;
		[Space]
		[SerializeField] private float _fadeOutSeconds = 1;
		[SerializeField] private float _fadeInSeconds = 1;

		private float _progress;

		public async Task Load(Queue<ILoadingOperation> loadingOperations, bool fadeOut = true)
		{
			if (fadeOut)
			{
				var taskFadeOut = FadeOut();
				await taskFadeOut;
				while (!taskFadeOut.IsCompleted) { await Task.Delay(1); }
			}
			else
			{
				_screenHolder.SetActive(true);
			}

			StartCoroutine(UpdateProgressBar());

			foreach (var operation in loadingOperations)
			{
				ResetFill();
				_descriptionText.text = operation.Description;

				await operation.Load(OnProgress);
				await WaitForBarFill();
			}

			var taskFadeIn = FadeIn();
			await taskFadeIn;
			while (!taskFadeIn.IsCompleted) { await Task.Delay(1);}
		}

		public async Task Load( ILoadingOperation loadingOperations, bool fadeOut = true)
		{
			var loadingQueue = new Queue<ILoadingOperation>();
			loadingQueue.Enqueue(loadingOperations);
			await Load(loadingQueue,fadeOut);
		}

		private void ResetFill()
		{
			_progressSlider.value = 0;
			_progress = 0;
		}

		private void OnProgress(float progress)
		{
			_progress = progress < 1 ? progress : 1;
		}

		private async Task WaitForBarFill()
		{
			while (_progressSlider.value < _progress)
			{
				await Task.Delay(1);
			}
			await Task.Delay(TimeSpan.FromSeconds(0.15f));
		}

		private IEnumerator UpdateProgressBar()
		{
			while (_screenHolder.activeSelf)
			{
				if (_progressSlider.value < _progress)
					_progressSlider.value += Time.deltaTime * _barSpeed;
				yield return null;
			}
		}

		private async Task FadeOut()
		{
			_fadeImage.enabled = true;
			_fadeImage.color = new Color(0, 0, 0, 0);
			var startTime = Time.time;
			var endTime = Time.time + _fadeOutSeconds;
			float alfa = 0;
			do
			{
				alfa = Mathf.InverseLerp(startTime, endTime, Time.time);
				_fadeImage.color = new Color(0, 0, 0, alfa);
				await Task.Delay(1);
			} while (endTime > Time.time);
			_screenHolder.SetActive(true);
		}

		private async Task FadeIn()
		{
			_screenHolder.SetActive(false);
			_fadeImage.color = new Color(0, 0, 0, 1);
			var startTime = Time.time;
			var endTime = Time.time + _fadeInSeconds;
			float alfa = 1;
			do
			{
				alfa = Mathf.InverseLerp(endTime, startTime, Time.time);
				_fadeImage.color = new Color(0, 0, 0, alfa);
				await Task.Delay(1);
			} while (endTime > Time.time);
			_fadeImage.enabled = false;
		}
	}
}
