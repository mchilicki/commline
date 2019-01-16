using System;

namespace Chilicki.Commline.Infrastructure.IO.Options
{
    public class SerializationOptions
    {
        private static readonly string SETTINGS_SAVE_LOCATION =
            AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

        private static readonly string SETTINGS_SAVE_FILE_NAME = "\\commlineSettings.json";

        public static readonly string SETTINGS_SAVE_PATH =
            SETTINGS_SAVE_LOCATION + SETTINGS_SAVE_FILE_NAME;        
    }
}
