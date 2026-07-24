using SDL;

namespace AutoSavestateMaker.Input
{
    internal unsafe class InputHandler
    {
        public Action StartStopButtonAction { get; set; } = () => { };
        public Action LoadSavestateButtonAction { get; set; } = () => { };
        public Action SlotLeftButtonAction { get; set; } = () => { };
        public Action SlotRightButtonAction { get; set; } = () => { };
        public Action FocusGameAction { get; set; } = () => { };
        public Action OnTick { get; set; } = () => { };

        public bool HotkeysOn { get; set; } = false;
        public bool RequireShift { get; set; } = false;
        public bool FocusGame { get; set; } = false;

        public bool InEditMode { get; set; } = false;

        private System.Windows.Forms.Timer _timer = new();

        public List<JoystickHandler> Joysticks { get; private set; } = [];
        public JoystickHandler SelectedJoystick { get; set; }


        public Func<SDL_Event, bool> AssignWaitingAction = null;

        private int _deadzone = (int)(SDL3.SDL_JOYSTICK_AXIS_MAX * 0.2);

        public InputHandler()
        {
            SDL3.SDL_Init(SDL_InitFlags.SDL_INIT_JOYSTICK | SDL_InitFlags.SDL_INIT_EVENTS);
            RefreshControllers();

            _timer.Interval = 33;
            _timer.Tick += Tick;
            _timer.Start();
        }

        public void Tick(object sender, EventArgs e)
        {
            ProcessJoystick();
            OnTick();
        }

        public void RefreshControllers()
        {
            foreach (var joystickInfo in Joysticks)
            {
                SDL3.SDL_CloseJoystick(joystickInfo.Joystick);
            }
            Joysticks.Clear();

            var ids = SDL3.SDL_GetJoysticks();
            foreach (var id in ids)
            {
                var joystick = SDL3.SDL_OpenJoystick(id);
                Joysticks.Add(new JoystickHandler(joystick));
            }
        }

        public void ProcessJoystick()
        {
            if (SelectedJoystick != null)
            {
                SelectedJoystick.InitLastTickArrays();

                SDL_Event e;
                var config = Config.Instance;

                while (SDL3.SDL_PollEvent(&e))
                {
                    if (SelectedJoystick != null && e.jbutton.which == SelectedJoystick.Id)
                    {
                        if (AssignWaitingAction != null && AssignWaitingAction(e))
                        {
                            AssignWaitingAction = null;
                        }

                        SelectedJoystick.HandleEvent(e);
                    }
                }

                if (!InEditMode)
                {
                    if (FocusGame && TestForPress(Config.Instance.FocusGame))
                    {
                        FocusGameAction();
                    }

                    bool shiftTest = !RequireShift || TestForActive(Config.Instance.Shift);
                    if (HotkeysOn && shiftTest)
                    {
                        if (TestForPress(Config.Instance.StartStop)) StartStopButtonAction();
                        if (TestForPress(Config.Instance.LoadSavestate)) LoadSavestateButtonAction();
                        if (TestForPress(Config.Instance.SlotLeft)) SlotLeftButtonAction();
                        if (TestForPress(Config.Instance.SlotRight)) SlotRightButtonAction();
                    }
                }
            }
        }

        private bool TestForPress(InputInfo input)
        {
            switch (input.InputType)
            {
                case InputType.Button:
                    return input.ID < SelectedJoystick.buttonStates.Length &&
                        SelectedJoystick.buttonStates[input.ID] && !SelectedJoystick.lastTickButtonStates[input.ID];
                case InputType.DPad:
                    return input.ID < SelectedJoystick.hatValues.Length &&
                        SelectedJoystick.hatValues[input.ID] == input.DPadValue && SelectedJoystick.lastTickHatValues[input.ID] != input.DPadValue;
                case InputType.Axis:
                    if (input.ID >= SelectedJoystick.axisValues.Length) return false;

                    short lastTick = SelectedJoystick.lastTickAxisValues[input.ID];
                    short current = SelectedJoystick.axisValues[input.ID];

                    return (Math.Sign(current) == input.AxisDirection && Math.Abs((int)current) > _deadzone)
                        && !(Math.Sign(lastTick) == input.AxisDirection && Math.Abs((int)lastTick) > _deadzone);
            }

            return false;
        }

        private bool TestForActive(InputInfo input)
        {
            switch (input.InputType)
            {
                case InputType.Button:
                    return input.ID < SelectedJoystick.buttonStates.Length &&
                        SelectedJoystick.buttonStates[input.ID];
                case InputType.DPad:
                    return input.ID < SelectedJoystick.hatValues.Length &&
                        SelectedJoystick.hatValues[input.ID] == input.DPadValue;
                case InputType.Axis:
                    if (input.ID >= SelectedJoystick.axisValues.Length) return false;

                    short current = SelectedJoystick.axisValues[input.ID];

                    return Math.Sign(current) == input.AxisDirection
                        && Math.Abs((int)current) > _deadzone;
            }

            return false;
        }

        public string[] GetJoystickNames()
        {
            unsafe
            {
                return Joysticks.Select(j => j.Name).ToArray();
            }
        }
    }
}
