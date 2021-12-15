using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
	private static LoadingScreen _instance;
	public static LoadingScreen Instance { get { return _instance; } }

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

		StartCoroutine(LoadingRoutine(_startSceneName, false));
	}

	[SerializeField] private Text _text;
    [SerializeField] private Slider _slider;
	[SerializeField] private Image _fadeImage;
	[SerializeField] private GameObject _screenHolder;
	[Space]
	[SerializeField] private string _startSceneName = "MainMenu";
	[SerializeField] private float _fadeOutSeconds = 1;
	[SerializeField] private float _fadeInSeconds = 1;

	public static void LoadScene (string sceneName)
	{
		Instance.StartCoroutine(Instance.LoadingRoutine(sceneName));
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
			_slider.value = asyncOperation.progress;

			yield return new WaitForFixedUpdate();
		}

		yield return new WaitForSeconds(2); //Delay to see the loading screen

		_screenHolder.SetActive(false);

		yield return FadeIn();
	}

	private IEnumerator FadeOut()
	{
		_fadeImage.gameObject.SetActive(true);
		_fadeImage.color = new Color(0, 0, 0, 0);
		var countDown = Time.time + _fadeOutSeconds;
		float alfa = 0;
		do
		{
			alfa += Time.fixedDeltaTime / _fadeOutSeconds;
			_fadeImage.color = new Color(0, 0, 0, alfa);
			yield return new WaitForFixedUpdate();
		} while (countDown> Time.time);
	}

	private IEnumerator FadeIn()
	{
		_fadeImage.gameObject.SetActive(true);
		_fadeImage.color = new Color(0, 0, 0, 1);
		var countDown = Time.time + _fadeOutSeconds;
		float alfa = 1;
		do
		{
			alfa -= Time.fixedDeltaTime / _fadeInSeconds;
			_fadeImage.color = new Color(0, 0, 0, alfa);
			yield return new WaitForFixedUpdate();
		} while (countDown > Time.time);
		_fadeImage.gameObject.SetActive(false);
	}
}
