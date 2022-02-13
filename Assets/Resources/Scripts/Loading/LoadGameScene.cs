using Loading;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

namespace TicTakToe
{
    public class LoadGameScene : ILoadingOperation
	{
		public string Description => "Game loading...";

		public async Task Load(Action<float> onProgress)
		{
			onProgress?.Invoke(0.5f);
			var loadOp = SceneManager.LoadSceneAsync("TicTakGame", LoadSceneMode.Additive);
			while (loadOp.isDone == false)
			{
				await Task.Delay(1);
			}
			onProgress?.Invoke(1f);
		}
	}
}
