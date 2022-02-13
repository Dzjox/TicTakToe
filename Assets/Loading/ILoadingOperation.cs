using System;
using System.Threading.Tasks;

namespace TicTakToe.Loading
{
    public interface ILoadingOperation
    {
        string Description { get; set; }

        Task Load(Action<float> onProgress);
    }
}
