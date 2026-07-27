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
        public int SavestateSlotCount
        {
            get;
            set
            {
                field = value < 5 ? 5 : value;
            }
        } = 20;

        public int IntervalSeconds
        {
            get;
            set
            {
                field = value < 1 ? 1 : value;
            }
        } = 5;

        public bool FocusGameWithA { get; set; } = true;
        public bool HotkeysOn { get; set; } = false;
        public bool RequireShift { get; set; } = false;

        public int RewindAtLeastBySeconds
        {
            get;
            set
            {
                field = value < 0 ? 0 : value;
            }
        } = 3;
        public int ExtraDelaySecondsOnLoad
        {
            get;
            set
            {
                field = value < 0 ? 0: value;
            }
        } = 5;

        public InputInfo FocusGameInput { get; set; } = new InputInfo(InputType.Button, 0);
        public InputInfo ShiftInput { get; set; } = new InputInfo(InputType.Button, 4);
        public InputInfo StartStopInput { get; set; } = new InputInfo(InputType.DPad, 1);
        public InputInfo LoadSavestateInput { get; set; } = new InputInfo(InputType.DPad, 8);
        public InputInfo SlotLeftInput { get; set; } = new InputInfo(InputType.DPad, 2);
        public InputInfo SlotRightInput { get; set; } = new InputInfo(InputType.DPad, 4);
    }
}
