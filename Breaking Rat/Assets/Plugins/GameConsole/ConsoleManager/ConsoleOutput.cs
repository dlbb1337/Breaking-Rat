using GameConsole.LogManager;
using TMPro;
using UnityEngine;

namespace GameConsole.ConsoleManager
{
    public class ConsoleOutput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _outputField;

        public void DisplayLog(Log log)
        {
            _outputField.text += log.ToString();
        }
    }
}
