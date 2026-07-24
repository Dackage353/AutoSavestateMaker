using AutoSavestateMaker.Input;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AutoSavestateMaker
{
    internal class Config
    {
        private const string ConfigFileName = "config.yaml";
        public static Config Instance { get; private set; } = Load();

        public static void SaveInstance()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            string yaml = serializer.Serialize(Instance);
            File.WriteAllText(ConfigFileName, yaml);
        }


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
        public int SavestateSlotCount { get; set; } = 20;
        public int Interval { get; set; } = 5;
        public int RewindAtLeastBySeconds { get; set; } = 2;
        public int ExtraPauseSecondsOnLoad { get; set; } = 5;

        public InputInfo FocusGame { get; set; } = new InputInfo(InputType.Button, 0);
        public InputInfo Shift { get; set; } = new InputInfo(InputType.Button, 4);
        public InputInfo StartStop { get; set; } = new InputInfo(InputType.DPad, 1);
        public InputInfo LoadSavestate { get; set; } = new InputInfo(InputType.DPad, 8);
        public InputInfo SlotLeft { get; set; } = new InputInfo(InputType.DPad, 2);
        public InputInfo SlotRight { get; set; } = new InputInfo(InputType.DPad, 4);
    }
}
