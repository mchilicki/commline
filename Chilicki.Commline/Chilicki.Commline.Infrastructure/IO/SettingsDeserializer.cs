using Chilicki.Commline.Infrastructure.IO.Options;
using Chilicki.Commline.Infrastructure.Settings;
using Newtonsoft.Json;
using System.IO;

namespace Chilicki.Commline.Infrastructure.IO
{
    public class SettingsDeserializer
    {
        public CommlineSettings ReadSettings()
        {
            return JsonConvert.DeserializeObject<CommlineSettings>
                (File.ReadAllText(SerializationOptions.SETTINGS_SAVE_PATH));
        }        
    }
}
