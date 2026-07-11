using SharpDX;
using SharpDX.DirectInput;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoSavestateMaker
{
    public class InputHandler
    {
        public Action StartStopButtonAction { get; set; } = () => { };
        public Action LoadSavestateButtonAction { get; set; } = () => { };
        public Action SlotLeftButtonAction { get; set; } = () => { };
        public Action SlotRightButtonAction { get; set; } = () => { };
        public Action FocusGameAction { get; set; } = () => { };
        public Action OnTick { get; set; } = () => { };

        private ActiveButtons oldButtons, activeButtons = new();

        public bool HotkeysOn { get; set; } = false;
        public bool RequireShift { get; set; } = false;
        public bool FocusGame { get; set; } = false;

        public List<DeviceInstance> Controllers { get; set; } = [];
        public DeviceInstance SelectedController { get; set; } = null;

        private System.Windows.Forms.Timer _timer = new();
        private DirectInput _directInput = new();
        private Joystick _joystick = null;

        public int LastButtonPressed { get; set; } = -1;


        public InputHandler()
        {
            _timer.Interval = 6;
            _timer.Tick += Tick;
            _timer.Start();
        }

        public void RefreshControllers()
        {
            try
            {
                Controllers.Clear();
                Controllers.AddRange(_directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices));
                Controllers.AddRange(_directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices));
            }
            catch (SharpDXException)
            {
                Debug.WriteLine("sharpdx exception");
            }
        }

        public void ClearController()
        {
            SelectedController = null;

            if (_joystick != null)
            {
                _joystick.Dispose();
                _joystick = null;
            }
        }

        public void SetUpJoystick()
        {
            if (SelectedController == null) return;

            try
            {
                _joystick = new Joystick(_directInput, SelectedController.InstanceGuid);
                _joystick.Properties.BufferSize = 128;
                _joystick.Acquire();

            }
            catch (SharpDXException)
            {
                Debug.WriteLine("sharpdx exception");
            }
        }

        public void Tick(object sender, EventArgs e)
        {
            ProcessJoystick();
            OnTick();
        }


        public void ProcessJoystick()
        {
            if (_joystick == null) return;

            try
            {
                _joystick.Poll();
                var datas = _joystick.GetBufferedData();
                oldButtons = activeButtons;

                foreach (var state in datas)
                {
                    bool pressed = state.Value != 0;

                    int button = (int)state.Offset;
                    if (button == Config.Instance.FocusGameButtonID) activeButtons.FocusGame = pressed;
                    if (button == Config.Instance.StartStopButtonID) activeButtons.StartStop = pressed;
                    if (button == Config.Instance.LoadSavestateButtonID) activeButtons.LoadSavestate = pressed;
                    if (button == Config.Instance.SlotLeftButtonID) activeButtons.SlotLeft = pressed;
                    if (button == Config.Instance.SlotRightButtonID) activeButtons.SlotRight = pressed;
                    if (button == Config.Instance.ShiftButtonID) activeButtons.Shift = pressed;

                    if (pressed) LastButtonPressed = button;
                }

                if (FocusGame && activeButtons.FocusGame && !oldButtons.FocusGame)
                {
                    FocusGameAction();
                }

                bool shiftTest = !RequireShift || activeButtons.Shift;
                if (HotkeysOn && shiftTest)
                {
                    if (activeButtons.StartStop && !oldButtons.StartStop) StartStopButtonAction();
                    if (activeButtons.LoadSavestate && !oldButtons.LoadSavestate) LoadSavestateButtonAction();
                    if (activeButtons.SlotLeft && !oldButtons.SlotLeft) SlotLeftButtonAction();
                    if (activeButtons.SlotRight && !oldButtons.SlotRight) SlotRightButtonAction();
                }
            }
            catch (SharpDXException)
            {
                Debug.WriteLine("sharpdx exception");
            }
        }

        struct ActiveButtons
        {
            public bool FocusGame, StartStop, LoadSavestate, SlotLeft, SlotRight, Shift;
        }
    }
}
