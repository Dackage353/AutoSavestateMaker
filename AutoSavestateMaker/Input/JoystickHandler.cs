using SDL;

namespace AutoSavestateMaker.Input
{
    internal unsafe class JoystickHandler
    {
        public SDL_Joystick* Joystick { get; set; }

        public SDL_JoystickID Id
        {
            get
            {
                return SDL3.SDL_GetJoystickID(Joystick);
            }
        }

        public string Name
        {
            get
            {
                return SDL3.SDL_GetJoystickName(Joystick);
            }
        }

        public short[] lastTickAxisValues, axisValues;
        public byte[] lastTickHatValues, hatValues;
        public bool[] lastTickButtonStates, buttonStates;

        public JoystickHandler(SDL_Joystick* joystick)
        {
            Joystick = joystick;

            axisValues = new short[SDL3.SDL_GetNumJoystickAxes(Joystick)];
            hatValues = new byte[SDL3.SDL_GetNumJoystickHats(Joystick)];
            buttonStates = new bool[SDL3.SDL_GetNumJoystickButtons(Joystick)];

            InitLastTickArrays();
        }

        public void InitLastTickArrays()
        {
            lastTickAxisValues = axisValues.ToArray();
            lastTickHatValues = hatValues.ToArray();
            lastTickButtonStates = buttonStates.ToArray();
        }

        public void HandleEvent(SDL_Event e)
        {
            SDL_EventType type = (SDL_EventType)e.type;

            switch (type)
            {
                case SDL_EventType.SDL_EVENT_JOYSTICK_BUTTON_DOWN:
                    buttonStates[e.jbutton.button] = true;
                    break;
                case SDL_EventType.SDL_EVENT_JOYSTICK_BUTTON_UP:
                    buttonStates[e.jbutton.button] = false;
                    break;
                case SDL_EventType.SDL_EVENT_JOYSTICK_HAT_MOTION:
                    hatValues[e.jhat.hat] = e.jhat.value;
                    break;
                case SDL_EventType.SDL_EVENT_JOYSTICK_AXIS_MOTION:
                    axisValues[e.jaxis.axis] = e.jaxis.value;
                    break;
            }
        }
    }
}
