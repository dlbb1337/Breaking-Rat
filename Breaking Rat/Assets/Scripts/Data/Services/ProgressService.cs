using System;
using System.IO;
using UnityEngine;

namespace BreakingRat.Data.Services
{
    public class ProgressService : IProgressService
    {
        private readonly string _filePath;
        private readonly IStaticDataService _staticDataService;
        private Progress _progress = new();
        public Progress Progress => _progress;

        public ProgressService(IStaticDataService staticDataService)
        {
            _filePath = Application.persistentDataPath + "/Save.json";
            _staticDataService = staticDataService;

            Initialize();
        }

        public void Initialize()
        {
            if (!File.Exists(_filePath))
                using (StreamWriter writer = File.AppendText(_filePath)) { }

            LoadProgress();
        }

        public void SaveProgress()
        {
            var json = JsonUtility.ToJson(Progress);
            using (var writer = new StreamWriter(_filePath, false))
            {
                writer.Write(json);
            }
        }

        public void LoadProgress()
        {
            var json = String.Empty;

            using (var reader = new StreamReader(_filePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                    json += line;
            }


            if (String.IsNullOrEmpty(json))
                return;

            _progress = JsonUtility.FromJson<Progress>(json);

        }
    }
}