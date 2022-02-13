using Loading;
using System;
using System.Threading.Tasks;

namespace TicTakToe
{
    public class FakeLoadingTexture : ILoadingOperation
    {
		public string Description => "Drawing new pictures...";

		public async Task Load(Action<float> onProgress)
		{
			float progress = 0f;
			do
			{
				progress += 0.1f;
				onProgress?.Invoke(progress);
				await Task.Delay(TimeSpan.FromSeconds(0.1f));
			} while (progress < 1);
		}
	}
}
