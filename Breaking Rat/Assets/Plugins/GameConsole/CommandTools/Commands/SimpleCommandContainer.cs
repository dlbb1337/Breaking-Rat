using UnityEngine;

namespace GameConsole.CommandTools.Commands
{
    public class SimpleCommandContainer : ICommandContainer
    {
        public void Print(string text)
        {
            Debug.Log(text);
        }

        public void Quit()
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
