using BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services;
using BreakingRat.Assets.Scripts.Core.Domain.Entities;
using System;
using System.IO;
using UnityEngine;

namespace BreakingRat.Assets.Scripts.Infrastructure.Persistence.Services
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
            var json = string.Empty;

            using (var reader = new StreamReader(_filePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                    json += line;
            }


            if (string.IsNullOrEmpty(json))
                return;

            _progress = JsonUtility.FromJson<Progress>(json);

        }
    }
}