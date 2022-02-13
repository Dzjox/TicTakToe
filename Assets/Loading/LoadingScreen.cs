using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;
using System;

namespace TicTakToe.Loading
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

		public async Task Load(Queue<ILoadingOperation> loadingOperations)
		{
			StartCoroutine(FadeOut());
			await Task.Delay(TimeSpan.FromSeconds(_fadeOutSeconds));

			StartCoroutine(UpdateProgressBar());

			foreach (var operation in loadingOperations)
			{
				ResetFill();
				_descriptionText.text = operation.Description;

				await operation.Load(OnProgress);
				await WaitForBarFill();
			}

			StartCoroutine(FadeIn());
			await Task.Delay(TimeSpan.FromSeconds(_fadeInSeconds));
		}

		private void ResetFill()
		{
			_progressSlider.value = 0;
			_progress = 0;
		}

		private void OnProgress(float progress)
		{
			_progress = progress;
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

		private IEnumerator LoadingRoutine(string sceneName, bool useFadeOut = true)
		{
			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
			asyncOperation.allowSceneActivation = false;

			if (useFadeOut) yield return FadeOut();

			asyncOperation.allowSceneActivation = true;

			_screenHolder.SetActive(true);

			while (!asyncOperation.isDone)
			{
				_progressSlider.value = asyncOperation.progress;

				yield return new WaitForFixedUpdate();
			}

			yield return new WaitForSeconds(2); //Delay to see the loading screen

			_screenHolder.SetActive(false);

			yield return FadeIn();
		}

		private IEnumerator FadeOut()
		{
			_fadeImage.enabled = true;
			_fadeImage.color = new Color(0, 0, 0, 0);
			var countDown = Time.time + _fadeOutSeconds;
			float alfa = 0;
			do
			{
				alfa += Time.fixedDeltaTime / _fadeOutSeconds;
				_fadeImage.color = new Color(0, 0, 0, alfa);
				yield return new WaitForFixedUpdate();
			} while (countDown > Time.time);
			_screenHolder.SetActive(true);
		}

		private IEnumerator FadeIn()
		{
			_screenHolder.SetActive(false);
			_fadeImage.color = new Color(0, 0, 0, 1);
			var countDown = Time.time + _fadeInSeconds;
			float alfa = 1;
			do
			{
				alfa -= Time.fixedDeltaTime / _fadeInSeconds;
				_fadeImage.color = new Color(0, 0, 0, alfa);
				yield return new WaitForFixedUpdate();
			} while (countDown > Time.time);
			_fadeImage.enabled = false;
		}
	}
}
