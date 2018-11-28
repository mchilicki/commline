using Chilicki.Commline.Infrastructure.IO.Options;
using Chilicki.Commline.Infrastructure.Settings;
using Newtonsoft.Json;
using System.IO;

namespace Chilicki.Commline.Infrastructure.IO
{
    public class SettingsSerializer
    {
        public void SaveSettings(CommlineSettings settings)
        {
            File.WriteAllText(SerializationOptions.SETTINGS_SAVE_PATH, 
                JsonConvert.SerializeObject(settings, Formatting.Indented));
        }
    }
}
