using SDL;

namespace AutoSavestateMaker.Input
{
    internal class InputInfo
    {
        public InputType InputType { get; set; }
        public int ID { get; set; }
        public int AxisDirection { get; set; }
        public int DPadValue { get; set; }

        public InputInfo() { }

        public InputInfo(InputType hotkeyType, int id)
        {
            InputType = hotkeyType;
            ID = id;
        }

        public override string ToString()
        {
            string text = InputType.ToString() + ": " + ID.ToString();

            if (InputType == InputType.DPad)
            {
                text += ", " + DPadValueAsText();
            }

            if (InputType == InputType.Axis)
            {
                text += AxisDirection > 0 ? "+" : "-"; ;
            }

            return text;
        }

        private string DPadValueAsText()
        {
            switch ((uint)DPadValue)
            {
                case SDL3.SDL_HAT_LEFT:
                    return "left";
                case SDL3.SDL_HAT_RIGHT:
                    return "right";
                case SDL3.SDL_HAT_UP:
                    return "up";
                case SDL3.SDL_HAT_DOWN:
                    return "down";
            }

            return DPadValue.ToString();
        }
    }
}
