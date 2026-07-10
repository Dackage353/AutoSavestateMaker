using SharpDX;
using SharpDX.DirectInput;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoSavestateMaker
{
    internal class InputHandler
    {
        private const int ButtonOffset = 48;

        public Action AAction { get; set; } = () => { };
        public Action BAction { get; set; } = () => { };
        public Action ZAction { get; set; } = () => { };
        public Action StartAction { get; set; } = () => { };
        public Action LAction { get; set; } = () => { };
        public Action RAction { get; set; } = () => { };
        public Action CUpAction { get; set; } = () => { };
        public Action CDownAction { get; set; } = () => { };
        public Action CLeftAction { get; set; } = () => { };
        public Action CRightAction { get; set; } = () => { };
        public Action DPadUpAction { get; set; } = () => { };
        public Action DPadDownAction { get; set; } = () => { };
        public Action DPadLeftAction { get; set; } = () => { };
        public Action DPadRightAction { get; set; } = () => { };
        public Action FocusGameWithAAction { get; set; } = () => { };

        private ActiveButtons oldButtons, activeButtons = new();

        public bool HotkeysOn { get; set; } = false;
        public bool RequireR { get; set; } = false;
        public bool FocusGameWithA { get; set; } = false;

        public List<DeviceInstance> Controllers { get; set; } = [];
        public DeviceInstance SelectedController { get; set; } = null;

        private System.Windows.Forms.Timer _timer = new();
        private DirectInput _directInput = new DirectInput();
        private Joystick _joystick = null;


        public InputHandler()
        {
            _timer.Interval = 6;
            _timer.Tick += ProcessJoystick;
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

        public void ProcessJoystick(object sender, EventArgs e)
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

                    int button = (int)state.Offset - ButtonOffset;
                    if (button == Config.Instance.AButtonID) activeButtons.A = pressed;
                    if (button == Config.Instance.BButtonID) activeButtons.B = pressed;
                    if (button == Config.Instance.ZButtonID) activeButtons.Z = pressed;
                    if (button == Config.Instance.StartButtonID) activeButtons.Start = pressed;
                    if (button == Config.Instance.LButtonID) activeButtons.L = pressed;
                    if (button == Config.Instance.RButtonID) activeButtons.R = pressed;
                    if (button == Config.Instance.CUpButtonID) activeButtons.CUp = pressed;
                    if (button == Config.Instance.CDownButtonID) activeButtons.CDown = pressed;
                    if (button == Config.Instance.CLeftButtonID) activeButtons.CLeft = pressed;
                    if (button == Config.Instance.CRightButtonID) activeButtons.CRight = pressed;
                    if (button == Config.Instance.DPadUpButtonID) activeButtons.DPadUp = pressed;
                    if (button == Config.Instance.DPadDownButtonID) activeButtons.DPadDown = pressed;
                    if (button == Config.Instance.DPadLeftButtonID) activeButtons.DPadLeft = pressed;
                    if (button == Config.Instance.DPadRightButtonID) activeButtons.DPadRight = pressed;
                }

                if (FocusGameWithA && activeButtons.A && !oldButtons.A)
                {
                    FocusGameWithAAction();
                }

                bool rTest = !RequireR || activeButtons.R;
                if (HotkeysOn && rTest)
                {
                    if (activeButtons.A && !oldButtons.A) AAction();
                    if (activeButtons.B && !oldButtons.B) BAction();
                    if (activeButtons.Z && !oldButtons.Z) ZAction();
                    if (activeButtons.Start && !oldButtons.Start) StartAction();
                    if (activeButtons.L && !oldButtons.L) LAction();
                    if (activeButtons.R && !oldButtons.R) RAction();
                    if (activeButtons.CUp && !oldButtons.CUp) CUpAction();
                    if (activeButtons.CDown && !oldButtons.CDown) CDownAction();
                    if (activeButtons.CLeft && !oldButtons.CLeft) CLeftAction();
                    if (activeButtons.CRight && !oldButtons.CRight) CRightAction();
                    if (activeButtons.DPadUp && !oldButtons.DPadUp) DPadUpAction();
                    if (activeButtons.DPadDown && !oldButtons.DPadDown) DPadDownAction();
                    if (activeButtons.DPadLeft && !oldButtons.DPadLeft) DPadLeftAction();
                    if (activeButtons.DPadRight && !oldButtons.DPadRight) DPadRightAction();
                }
            }
            catch (SharpDXException)
            {
                Debug.WriteLine("sharpdx exception");
            }
        }

        struct ActiveButtons
        {
            public bool A, B, Z, Start, L, R, CUp, CDown, CLeft, CRight, DPadUp, DPadDown, DPadLeft, DPadRight;
        }
    }
}
