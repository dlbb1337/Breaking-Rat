using System;

namespace BreakingRat.Application.Abstractions.IServices
{
    public interface ISceneLoaderService
    {
        void LoadScene(string sceneName, Action onLoaded);
    }
}
