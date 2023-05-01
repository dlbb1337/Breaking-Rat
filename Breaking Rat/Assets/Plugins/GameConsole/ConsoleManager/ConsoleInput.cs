using System;
using TMPro;
using UnityEngine;

namespace GameConsole.ConsoleManager
{
    public class ConsoleInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;

        public string Text
        {
            get => _inputField.text;

            set => _inputField.text = value;
        }
        public event Action<string> TextChanged;
        public event Action<string> Submit;

        private void Awake()
        {
            _inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
            _inputField.onSubmit.AddListener(OnInputFieldSubmit);
        }

        private void OnInputFieldValueChanged(string text)
        {
            TextChanged?.Invoke(text);
        }

        private void OnInputFieldSubmit(string text)
        {
            Submit?.Invoke(text);
            _inputField.text = string.Empty;
        }
    }
}

