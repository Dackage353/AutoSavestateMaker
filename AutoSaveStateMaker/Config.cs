using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AutoSavestateMaker
{
    internal class Config
    {
        private const string ConfigFileName = "config.yaml";

        public string SaveSavestateHotkey { get; set; } = "F5";
        public string LoadSavestateHotkey { get; set; } = "F7";
        public string ProcessName { get; set; } = "Project64";
        public int DefaultSavestateCount { get; set; } = 20;
        public int DefaultInterval { get; set; } = 5;

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
