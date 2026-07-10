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
        public static Config Instance = Load();

        private static Config Load()
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



        public string SaveSavestateHotkey { get; set; } = "F5";
        public string LoadSavestateHotkey { get; set; } = "F7";
        public string ProcessName { get; set; } = "Project64";
        public int DefaultSavestateCount { get; set; } = 20;
        public int DefaultInterval { get; set; } = 5;

        public int AButtonID { get; set; } = 0;
        public int BButtonID { get; set; } = 1;
        public int ZButtonID { get; set; } = 2;
        public int StartButtonID { get; set; } = 3;
        public int LButtonID { get; set; } = 4;
        public int RButtonID { get; set; } = 5;
        public int CUpButtonID { get; set; } = 6;
        public int CDownButtonID { get; set; } = 7;
        public int CLeftButtonID { get; set; } = 8;
        public int CRightButtonID { get; set; } = 9;
        public int DPadUpButtonID { get; set; } = 10;
        public int DPadDownButtonID { get; set; } = 11;
        public int DPadLeftButtonID { get; set; } = 12;
        public int DPadRightButtonID { get; set; } = 13;
    }
}
