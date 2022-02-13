using System.Collections.Generic;
using UnityEngine;
using Loading;

namespace TicTakToe
{
    public class TicTakStart : MonoBehaviour
    {
		private void Start()
		{
			var loadingQueue = new Queue<ILoadingOperation>();
			//loadingQueue.Enqueue(new FakeLoadingScene());
			//loadingQueue.Enqueue(new FakeLoadingTexture());
			loadingQueue.Enqueue(new LoadMainMenu());
			LoadingScreen.Instance.Load(loadingQueue,false);
		}
	}
}
