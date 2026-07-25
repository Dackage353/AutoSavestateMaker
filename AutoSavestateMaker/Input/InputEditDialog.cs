using SDL;

namespace AutoSavestateMaker.Input
{
    internal partial class InputEditDialog : Form
    {
        private InputHandler _inputHandler;

        public InputEditDialog(InputHandler inputHandler)
        {
            InitializeComponent();

            _inputHandler = inputHandler;
            _inputHandler.InEditMode = true;

            focusGameHotkeyValue_Label.Text = Config.Instance.FocusGameInput.ToString();
            startStopHotkeyValue_Label.Text = Config.Instance.StartStopInput.ToString();
            loadSavestateHotkeyValue_Label.Text = Config.Instance.LoadSavestateInput.ToString();
            slotLeftHotkeyValue_Label.Text = Config.Instance.SlotLeftInput.ToString();
            slotRightHotkeyValue_Label.Text = Config.Instance.SlotRightInput.ToString();
            shiftHotkeyValue_Label.Text = Config.Instance.ShiftInput.ToString();

            FormClosed += (a, b) =>
            {
                _inputHandler.InEditMode = false;
                Config.SaveInstance();
            };
        }

        private void focusGameHotkeySet_Button_Click(object sender, EventArgs e)
        {
            focusGameHotkeyValue_Label.Text = "waiting...";

            _inputHandler.AssignWaitingAction = (e) =>
            {
                if (IsValidEvent(e))
                {
                    Config.Instance.FocusGameInput = GetInputInfo(e);
                    focusGameHotkeyValue_Label.Text = Config.Instance.FocusGameInput.ToString();
                    return true;
                }

                return false;
            };
        }

        private void startStopHotkeySet_Button_Click(object sender, EventArgs e)
        {
            startStopHotkeyValue_Label.Text = "waiting...";

            _inputHandler.AssignWaitingAction = (e) =>
            {
                if (IsValidEvent(e))
                {
                    Config.Instance.StartStopInput = GetInputInfo(e);
                    startStopHotkeyValue_Label.Text = Config.Instance.StartStopInput.ToString();
                    return true;
                }

                return false;
            };
        }

        private void loadSavestateHotkeySet_Button_Click(object sender, EventArgs e)
        {
            loadSavestateHotkeyValue_Label.Text = "waiting...";

            _inputHandler.AssignWaitingAction = (e) =>
            {
                if (IsValidEvent(e))
                {
                    Config.Instance.LoadSavestateInput = GetInputInfo(e);
                    loadSavestateHotkeyValue_Label.Text = Config.Instance.LoadSavestateInput.ToString();
                    return true;
                }

                return false;
            };
        }

        private void slotLeftHotkeySet_Button_Click(object sender, EventArgs e)
        {
            slotLeftHotkeyValue_Label.Text = "waiting...";

            _inputHandler.AssignWaitingAction = (e) =>
            {
                if (IsValidEvent(e))
                {
                    Config.Instance.SlotLeftInput = GetInputInfo(e);
                    slotLeftHotkeyValue_Label.Text = Config.Instance.SlotLeftInput.ToString();
                    return true;
                }

                return false;
            };
        }

        private void slotRightHotkeySet_Button_Click(object sender, EventArgs e)
        {
            slotRightHotkeyValue_Label.Text = "waiting...";

            _inputHandler.AssignWaitingAction = (e) =>
            {
                if (IsValidEvent(e))
                {
                    Config.Instance.SlotRightInput = GetInputInfo(e);
                    slotRightHotkeyValue_Label.Text = Config.Instance.SlotRightInput.ToString();
                    return true;
                }

                return false;
            };
        }

        private void shiftHotkeySet_Button_Click(object sender, EventArgs e)
        {
            shiftHotkeyValue_Label.Text = "waiting...";

            _inputHandler.AssignWaitingAction = (e) =>
            {
                if (IsValidEvent(e))
                {
                    Config.Instance.ShiftInput = GetInputInfo(e);
                    shiftHotkeyValue_Label.Text = Config.Instance.ShiftInput.ToString();
                    return true;
                }

                return false;
            };
        }

        private InputInfo GetInputInfo(SDL_Event e)
        {
            switch ((SDL_EventType)e.type)
            {
                case SDL_EventType.SDL_EVENT_JOYSTICK_BUTTON_DOWN:
                    return new InputInfo(InputType.Button, e.jbutton.button);
                case SDL_EventType.SDL_EVENT_JOYSTICK_HAT_MOTION:
                    return new InputInfo(InputType.DPad, e.jhat.hat) { DPadValue = e.jhat.value };
                case SDL_EventType.SDL_EVENT_JOYSTICK_AXIS_MOTION:
                    return new InputInfo(InputType.Axis, e.jaxis.axis) { AxisDirection = e.jaxis.value > 0 ? 1 : -1 };
            }

            return null;
        }

        private bool IsValidEvent(SDL_Event e)
        {
            SDL_EventType type = (SDL_EventType)e.type;

            if (type == SDL_EventType.SDL_EVENT_JOYSTICK_HAT_MOTION)
            {
                return e.jhat.value == SDL3.SDL_HAT_LEFT || e.jhat.value == SDL3.SDL_HAT_RIGHT || e.jhat.value == SDL3.SDL_HAT_UP || e.jhat.value == SDL3.SDL_HAT_DOWN;
            }

            return type == SDL_EventType.SDL_EVENT_JOYSTICK_BUTTON_DOWN
                || type == SDL_EventType.SDL_EVENT_JOYSTICK_AXIS_MOTION
                || type == SDL_EventType.SDL_EVENT_JOYSTICK_HAT_MOTION;
        }

        private void ok_Button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
