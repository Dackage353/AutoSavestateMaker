using SharpDX;
using SharpDX.DirectInput;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoSaveStateMaker
{
    internal class InputHandler
    {
        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr point);

        public Joystick joystick;
        public int ACount, BCount, ZCount, StartCount,
            LCount, RCount, CUpCount, CDownCount, CLeftCount,
            CRightCount, DPadUpCount, DPadDownCount, DPadLeftCount, DPadRightCount;

        public Action AAction = () => { },
            BAction = () => { },
            ZAction = () => { },
            StartAction = () => { },
            dPadUpAction = () => { },
            dPadDownAction = () => { },
            dPadLeftAction = () => { },
            dPadRightAction = () => { };


        private ActiveButtons oldButtons, activeButtons = new();

        public bool HotkeysOn { get; set; } = false;
        public bool RequireR { get; set; } = false;

        private readonly System.Windows.Forms.Timer timer = new();

        public void SetUpJoystick()
        {
            timer.Interval = 6;
            timer.Tick += ProcessJoystick;
            timer.Start();

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
            catch (SharpDXException e)
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
                        case JoystickOffset.Buttons0: activeButtons.A = pressed; ACount++; break;
                        case JoystickOffset.Buttons1: activeButtons.B = pressed; BCount++; break;
                        case JoystickOffset.Buttons2: activeButtons.Z = pressed; ZCount++; break;
                        case JoystickOffset.Buttons3: activeButtons.Start = pressed; StartCount++; break;
                        case JoystickOffset.Buttons4: activeButtons.L = pressed; LCount++; break;
                        case JoystickOffset.Buttons5: activeButtons.R = pressed; RCount++; break;
                        case JoystickOffset.Buttons6: activeButtons.CUp = pressed; CUpCount++; break;
                        case JoystickOffset.Buttons7: activeButtons.CDown = pressed; CDownCount++; break;
                        case JoystickOffset.Buttons8: activeButtons.CLeft = pressed; CLeftCount++; break;
                        case JoystickOffset.Buttons9: activeButtons.CRight = pressed; CRightCount++; break;
                        case JoystickOffset.Buttons10: activeButtons.DPadUp = pressed; DPadUpCount++; break;
                        case JoystickOffset.Buttons11: activeButtons.DPadDown = pressed; DPadDownCount++; break;
                        case JoystickOffset.Buttons12: activeButtons.DPadLeft = pressed; DPadLeftCount++; break;
                        case JoystickOffset.Buttons13: activeButtons.DPadRight = pressed; DPadRightCount++; break;
                    }
                }

                bool rTest = !RequireR || activeButtons.R;
                if (HotkeysOn && rTest)
                {
                    if (activeButtons.DPadUp && !oldButtons.DPadUp) dPadUpAction();
                    if (activeButtons.DPadDown && !oldButtons.DPadDown) dPadDownAction();
                    if (activeButtons.DPadLeft && !oldButtons.DPadLeft) dPadLeftAction();
                    if (activeButtons.DPadRight && !oldButtons.DPadRight) dPadRightAction();
                    if (activeButtons.A && !oldButtons.A) AAction();
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
