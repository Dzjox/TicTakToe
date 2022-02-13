using Loading;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

namespace TicTakToe
{
	public class LoadMainMenu : ILoadingOperation
	{
		public string Description => "Main menu loading...";

		public async Task Load(Action<float> onProgress)
		{
			onProgress?.Invoke(0.5f);
			var loadOp = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
			while (loadOp.isDone == false)
			{
				await Task.Delay(1);
			}
			onProgress?.Invoke(1f);
		}
	}

}
