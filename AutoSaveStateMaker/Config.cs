using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AutoSaveStateMaker
{
    internal class Config
    {
        private const string ConfigFileName = "config.yaml";

        public string SaveSavestateHotkey = "F5";
        public string LoadSavestateHotkey = "F7";
        public string ProcessName = "Project64";

        public static Config Load()
        {
            if (File.Exists(ConfigFileName))
            {
                string yaml = File.ReadAllText(ConfigFileName);

                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();

                return deserializer.Deserialize<Config>(yaml);
            }
            else
            {
                var config = new Config();
                var serializer = new SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                string yaml = serializer.Serialize(config);
                File.WriteAllText(ConfigFileName, yaml);

                return config;
            }
        }
    }
}
