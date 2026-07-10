using SharpDX;
using SharpDX.DirectInput;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoSavestateMaker
{
    internal class InputHandler
    {
        public Joystick joystick;

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

        private readonly System.Windows.Forms.Timer _timer = new();

        public void SetUpJoystick()
        {
            _timer.Interval = 6;
            _timer.Tick += ProcessJoystick;
            _timer.Start();

            try
            {
                var directInput = new DirectInput();
                var joystickGuid = Guid.Empty;

                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                            DeviceEnumerationFlags.AllDevices))
                {
                    joystickGuid = deviceInstance.InstanceGuid;
                }

                if (joystickGuid == Guid.Empty)
                {
                    foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                            DeviceEnumerationFlags.AllDevices))
                    {
                        joystickGuid = deviceInstance.InstanceGuid;
                    }
                }

                if (joystickGuid == Guid.Empty)
                {
                    MessageBox.Show("No joystick/Gamepad found.");
                }
                else
                {
                    joystick = new Joystick(directInput, joystickGuid);

                    joystick.Properties.BufferSize = 128;
                    joystick.Acquire();
                }

            }
            catch (SharpDXException)
            {
                Debug.WriteLine("sharpdx exception");
            }
        }

        public void ProcessJoystick(object sender, EventArgs e)
        {
            if (joystick == null) return;

            try
            {
                joystick.Poll();
                var datas = joystick.GetBufferedData();
                oldButtons = activeButtons;

                foreach (var state in datas)
                {
                    bool pressed = state.Value != 0;

                    switch (state.Offset)
                    {
                        case JoystickOffset.Buttons0: activeButtons.A = pressed; break;
                        case JoystickOffset.Buttons1: activeButtons.B = pressed; break;
                        case JoystickOffset.Buttons2: activeButtons.Z = pressed; break;
                        case JoystickOffset.Buttons3: activeButtons.Start = pressed; break;
                        case JoystickOffset.Buttons4: activeButtons.L = pressed; break;
                        case JoystickOffset.Buttons5: activeButtons.R = pressed; break;
                        case JoystickOffset.Buttons6: activeButtons.CUp = pressed; break;
                        case JoystickOffset.Buttons7: activeButtons.CDown = pressed; break;
                        case JoystickOffset.Buttons8: activeButtons.CLeft = pressed; break;
                        case JoystickOffset.Buttons9: activeButtons.CRight = pressed; break;
                        case JoystickOffset.Buttons10: activeButtons.DPadUp = pressed; break;
                        case JoystickOffset.Buttons11: activeButtons.DPadDown = pressed; break;
                        case JoystickOffset.Buttons12: activeButtons.DPadLeft = pressed;  break;
                        case JoystickOffset.Buttons13: activeButtons.DPadRight = pressed; break;
                    }
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
