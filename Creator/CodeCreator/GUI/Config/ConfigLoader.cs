using Newtonsoft.Json;
using System;
using System.IO;

namespace GUI.Configuration
{
    public class ConfigLoader
    {


        public static void Save(Config cfg)
        {
            try
            {
                var json = JsonConvert.SerializeObject(cfg);
                File.WriteAllText(Global.ConfigPath, json);
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public static Config Load()
        {
            try
            {
                if (!File.Exists(Global.ConfigPath))
                    Save(new Config());

                var file = File.ReadAllText(Global.ConfigPath);


                return JsonConvert.DeserializeObject<Config>(file) 
                    ?? new Config();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
