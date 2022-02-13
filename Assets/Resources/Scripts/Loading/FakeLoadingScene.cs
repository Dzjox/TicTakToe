using Loading;
using System;
using System.Threading.Tasks;

namespace TicTakToe
{
    public class FakeLoadingScene : ILoadingOperation
	{
		public string Description => "Clearing field...";

		public async Task Load(Action<float> onProgress)
		{
			float progress = 0f;
			do
			{
				progress += 0.1f;
				onProgress?.Invoke(progress);
				await Task.Delay(TimeSpan.FromSeconds(0.2f));
			} while (progress < 1);
		}
	}
}
