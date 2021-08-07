﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConformU
{
    [DefaultMember("Settings")]
    public class ConformConfiguration
    {
        private const string FOLDER_NAME = "conform";
        private const string FOLDER_SETTINGS_FILE = "conform.settings";

        private readonly ConformLogger TL;
        Settings settings;

        /// <summary>
        /// Create a Configuration mangement instance and load the current settings
        /// </summary>
        /// <param name="logger">Data logger instance.</param>
        public ConformConfiguration(ConformLogger logger)
        {
            TL = logger;
            try
            {
                settings = new();

                string folderName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FOLDER_NAME);
                string fileSettingsFileName = Path.Combine(folderName, FOLDER_SETTINGS_FILE);
                TL.LogDebug("ConformConfiguration", $"Settings folder: {folderName}, Settings file: {fileSettingsFileName}");

                if (File.Exists(fileSettingsFileName))
                {
                    TL.LogDebug("ConformConfiguration", "File exists and read OK");
                    string serialisedSettings = File.ReadAllText(fileSettingsFileName);
                    TL.LogDebug("ConformConfiguration", $"Serialised settings: {serialisedSettings}");

                    settings = JsonSerializer.Deserialize<Settings>(serialisedSettings);
                    Status = "Settings read successfully";
                }
                else
                {
                    TL.LogDebug("ConformConfiguration", "File does not exist, creating file...");
                    PersistSettings(settings);
                    Status = "Settings set to defaults on first time use.";
                }
            }
            catch (JsonException ex)
            {
                TL.LogDebug("ConformConfiguration", $"Error parsing Conform settings file: {ex.Message}");
                Status = "Settings file corrupted, please reset to default values";
            }
            catch (Exception ex)
            {
                TL.LogDebug("ConformConfiguration", ex.ToString());
                Status = "Exception reading settings, default values are in use.";
            }
        }

        /// <summary>
        /// Validate the settings values
        /// </summary>
        public string Validate()
        {
            if (settings.CurrentDeviceName == ConformConstants.NO_DEVICE_SELECTED) return "No device has been selected.";
            if ((settings.CurrentDeviceTechnology != ConformConstants.TECHNOLOGY_ALPACA) & (settings.CurrentDeviceTechnology != ConformConstants.TECHNOLOGY_COM)) return $"Technology type is not Alpaca or COM: '{settings.CurrentDeviceTechnology}'";

            switch (settings.CurrentDeviceTechnology)
            {
                case ConformConstants.TECHNOLOGY_ALPACA:
                    break;

                case ConformConstants.TECHNOLOGY_COM:
                    if (settings.CurrentComDevice.ProgId == "") return "CurrentComDevice.ProgId  is empty.";

                    break;
            }



            // All OK so return empty string
            return "";
        }

        /// <summary>
        /// Persist current settings
        /// </summary>
        public void Save()
        {
            TL.LogDebug("Save", "persisting settings to settings file");
            PersistSettings(settings);
            Status = $"Settings saved at {DateTime.Now:HH:mm:ss.f}.";

            RaiseConfigurationChnagedEvent();
        }

        private void RaiseConfigurationChnagedEvent()
        {
            if (ConfigurationChanged is not null)
            {
                EventArgs args = new();
                TL.LogDebug("RaiseConfigurationChnagedEvent", "About to call configuration changed event handler");
                ConfigurationChanged(this, args);
                TL.LogDebug("RaiseConfigurationChnagedEvent", "Returned from configuration changed event handler");
            }
        }

        public void Reset()
        {
            TL.LogDebug("Reset", "Resetting settings file to default values");
            settings = new();
            PersistSettings(settings);
            Status = $"Settings reset at {DateTime.Now:HH:mm:ss.f}.";
            RaiseConfigurationChnagedEvent();

        }

        public delegate void MessageEventHandler(object sender, MessageEventArgs e);

        public event EventHandler ConfigurationChanged;

        public Settings Settings
        {
            get { return settings; }
        }

        public string Status { get; private set; }

        private void PersistSettings(Settings settingsToPersist)
        {
            try
            {
                if (settingsToPersist is null)
                {
                    throw new ArgumentNullException(nameof(settingsToPersist));
                }

                string folderName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FOLDER_NAME);
                string fileSettingsFileName = Path.Combine(folderName, FOLDER_SETTINGS_FILE);

                TL.LogDebug("PersistSettings", $"Settings folder: {folderName}, Settings file: {fileSettingsFileName}");

                JsonSerializerOptions options = new()
                {
                    WriteIndented = true
                };
                string serialisedSettings = JsonSerializer.Serialize<Settings>(settingsToPersist, options);

                Directory.CreateDirectory(folderName);
                File.WriteAllText(fileSettingsFileName, serialisedSettings);
            }
            catch (Exception ex)
            {
                TL.LogMessage("PersistSettings", ex.ToString());
            }

        }
    }
}