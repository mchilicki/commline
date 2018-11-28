using Chilicki.Commline.Infrastructure.IO;
using Chilicki.Commline.Infrastructure.Settings;
using System;

namespace Chilicki.Commline.Application.Managers.Settings
{
    public class SettingsManager
    {
        readonly SettingsSerializer _settingsSerializer;
        readonly SettingsDeserializer _settingsDeserializer;

        public SettingsManager(
            SettingsSerializer settingsSerializer,
            SettingsDeserializer settingsDeserializer)
        {
            _settingsSerializer = settingsSerializer;
            _settingsDeserializer = settingsDeserializer;
        }

        public CommlineSettings GetSettings()
        {
            CommlineSettings settings = new CommlineSettings();
            try
            {
                settings = _settingsDeserializer.ReadSettings();
            }
            catch(Exception ex)
            {
                _settingsSerializer.SaveSettings(settings);
            }
            return settings;
        }

        public void SaveSettings(CommlineSettings settings)
        {
            _settingsSerializer.SaveSettings(settings);
        }

        public void BackToDefaultSettings()
        {
            var defaultSettings = new CommlineSettings();
            SaveSettings(defaultSettings);
        }
    }
}
