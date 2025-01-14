﻿using Newtonsoft.Json;
using System;
using System.IO;
using ZumoController.WinForms.Utils;

namespace ZumoController.WinForms.Services
{
    public class SettingsManager<T> where T : new()
    {
        public SettingsManager()
        {

        }

        private const string _defaultFileName = "zumo-robot-settings.json";
        private const string _defaultFileLocation = "../../";

        //Load the settings from json file
        public static T Load(string fileName = _defaultFileName)
        {
            T? t = default(T);
            if (File.Exists(_defaultFileLocation + fileName))
            {
                var fileContents = File.ReadAllText(_defaultFileLocation + fileName);
                t = JsonConvert.DeserializeObject<T>(fileContents);
            }
            return t ?? new T();//get a new settings model with defaults if not able to read from JSON
        }

        public void Save(string fileName = _defaultFileName)
        {
            File.WriteAllText(_defaultFileLocation + fileName, this.ToJSON());
        }

        public static void Save(T settings, string fileName = _defaultFileName)
        {
            if (settings == null)
                return;
            File.WriteAllText(_defaultFileLocation + fileName, settings.ToJSON());
        }
    }

    public class Settings : SettingsManager<Settings>
    {
        public Settings() 
        {
            //Save();
        }
        public string PortName { get; set; } = "COM4";
        public int BaudRate { get; set; } = 9600;
        public int Speed { get; set; } = 0;
        public int Turn { get; set; } = 0;

        public bool IsValid() => (!string.IsNullOrWhiteSpace(PortName)) && BaudRate > 0;
        
    }
}
