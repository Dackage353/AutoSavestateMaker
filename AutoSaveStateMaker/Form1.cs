using AutoSavestateMaker.Input;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoSavestateMaker
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("User32.dll")]
        private static extern int SetForegroundWindow(IntPtr point);

        private const int offsetX = 185, offsetY = 90, buttonWidth = 35, buttonHeight = 25, horizontalPadding = 5, verticalPadding = 5;

        private readonly System.Windows.Forms.Timer _savestateTimer = new();
        private readonly InputHandler _inputHandler = new();

        private readonly Color RunningColor = Color.LightGreen,
            StoppedColor = Color.Tomato,
            RunningAndLoadedColor = Color.PaleGreen;


        private DateTime _lastSavestate = DateTime.MinValue;
        private int _currentSaveSlot = 1, _maxSaveSlot = 20, _extraWaitTime = 0;
        private bool _isNewSavedSlot = false;

        private List<Button> _savestateButtons = [];
        private InputEditDialog _testDialog = null;

        public Form1()
        {
            InitializeComponent();

            _inputHandler.RefreshControllers();
            controllerList_ComboBox.Items.AddRange(_inputHandler.GetJoystickNames());

            _inputHandler.StartStopButtonAction = () => StopOrStart();
            _inputHandler.LoadSavestateButtonAction = () => LoadCurrent();
            _inputHandler.SlotLeftButtonAction = () => DecreaseSlot();
            _inputHandler.SlotRightButtonAction = () => IncreaseSlot();
            _inputHandler.FocusGameAction = () => FocusWindow();

            _savestateTimer.Tick += (sender, e) =>
            {
                var timePassed = (DateTime.Now - _lastSavestate).TotalSeconds;
                var waitTime = (int)interval_NumericUpDown.Value + _extraWaitTime;

                if (timePassed >= waitTime)
                {
                    SaveSavestate(true, false);
                }
            };

            lastCreatedSlot_Label.Text = _currentSaveSlot.ToString();

            interval_NumericUpDown.Value = Config.Instance.Interval;
            _maxSaveSlot = Config.Instance.SavestateSlotCount;
            savestatesCount_NumericUpDown.Value = Config.Instance.SavestateSlotCount;

            SetSavestateButtons();
        }

        private void SetSavestateButtons()
        {
            foreach (var button in _savestateButtons)
            {
                Controls.Remove(button);
            }
            _savestateButtons.Clear();

            for (int i = 0; i < _maxSaveSlot; i++)
            {
                int row = i / 10;
                int column = i % 10;

                var button = new Button();
                button.Location = new Point(offsetX + ((buttonWidth + horizontalPadding) * column), offsetY + ((buttonHeight + verticalPadding) * row));
                button.Size = new Size(buttonWidth, buttonHeight);
                button.Text = (i + 1).ToString();
                button.Click += LoadSlotButton_Click;

                _savestateButtons.Add(button);
                Controls.Add(button);
            }
        }

        private void FocusWindow()
        {
            if (run_CheckBox.Checked)
            {
                Process process = Process.GetProcessesByName(Config.Instance.ProcessName).FirstOrDefault();
                if (process != null)
                {
                    SetForegroundWindow(process.MainWindowHandle);
                }
            }
        }

        private void StopOrStart()
        {
            run_CheckBox.Checked = !run_CheckBox.Checked;
        }

        private void LoadCurrent()
        {
            var timePassed = (DateTime.Now - _lastSavestate).TotalSeconds;

            if (_isNewSavedSlot && timePassed < Config.Instance.RewindAtLeastBySeconds)
            {
                DecreaseSlot();
            }

            status_PictureBox.BackColor = RunningAndLoadedColor;
            LoadSavestate(_currentSaveSlot);
            _extraWaitTime = Config.Instance.ExtraPauseSecondsOnLoad;
        }

        private void IncreaseSlot()
        {
            _currentSaveSlot++;
            if (_currentSaveSlot > _maxSaveSlot) _currentSaveSlot = 1;

            _isNewSavedSlot = false;
            lastCreatedSlot_Label.Text = _currentSaveSlot.ToString();
        }

        private void DecreaseSlot()
        {
            _currentSaveSlot--;
            if (_currentSaveSlot < 1) _currentSaveSlot = _maxSaveSlot;

            _isNewSavedSlot = false;
            lastCreatedSlot_Label.Text = _currentSaveSlot.ToString();
        }

        private void Start()
        {
            run_CheckBox.Checked = true;
            settings_Panel.Enabled = false;
            status_PictureBox.BackColor = RunningColor;

            SaveSavestate(true, true);

            _savestateTimer.Interval = 6;
            _savestateTimer.Start();
        }

        private void Stop()
        {
            run_CheckBox.Checked = false;
            settings_Panel.Enabled = true;
            status_PictureBox.BackColor = StoppedColor;

            _savestateTimer.Stop();
        }

        private void SaveSavestate(bool incrementSlot, bool forceFocus)
        {
            Process process = Process.GetProcessesByName(Config.Instance.ProcessName).FirstOrDefault();
            if (process != null)
            {
                if (forceFocus && process.MainWindowHandle != GetForegroundWindow())
                {
                    SetForegroundWindow(process.MainWindowHandle);
                }

                if (process.MainWindowHandle == GetForegroundWindow())
                {
                    if (incrementSlot)
                    {
                        _currentSaveSlot++;
                        _isNewSavedSlot = true;

                        if (_currentSaveSlot > _maxSaveSlot) _currentSaveSlot = 1;
                    }

                    string hotkey = "{" + Config.Instance.SaveSavestateHotkey + "}";

                    SendKeys.Send(GetKeyWithModifier(_currentSaveSlot));
                    SendKeys.Send(hotkey);

                    _lastSavestate = DateTime.Now;
                    _extraWaitTime = 0;

                    if (status_PictureBox.BackColor == Color.LightGreen) status_PictureBox.BackColor = Color.PaleGreen;
                    lastCreatedSlot_Label.Text = _currentSaveSlot.ToString();
                }
            }
        }

        private void LoadSavestate(int slot)
        {
            Process process = Process.GetProcessesByName(Config.Instance.ProcessName).FirstOrDefault();
            if (process != null)
            {
                if (GetForegroundWindow() != process.MainWindowHandle)
                {
                    SetForegroundWindow(process.MainWindowHandle);
                }

                string hotkey = "{" + Config.Instance.LoadSavestateHotkey + "}";

                SendKeys.Send(GetKeyWithModifier(slot));
                SendKeys.Send(hotkey);
            }
            else
            {
                Helper.ShowError("Process \"" + Config.Instance.ProcessName + "\" not found");
            }
        }

        private string GetKeyWithModifier(int slot)
        {
            string numberKey = (slot % 10).ToString();
            string modifier = string.Empty;

            // ctrl = ^, alt = %, shift = +
            switch (slot)
            {
                case <= 10: break;
                case <= 20: modifier = "^"; break;
                case <= 30: modifier = "%"; break;
                case <= 40: modifier = "+"; break;
                case <= 50: modifier = "^%"; break;
                case <= 60: modifier = "^+"; break;
                case <= 70: modifier = "%+"; break;
                case <= 80: modifier = "^%+"; break;
            }

            return modifier + numberKey;
        }

        private void run_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (run_CheckBox.Checked)
            {
                Process p = Process.GetProcessesByName(Config.Instance.ProcessName).FirstOrDefault();
                if (p != null)
                {
                    Start();
                }
                else
                {
                    run_CheckBox.Checked = false;
                    Helper.ShowError("Process \"" + Config.Instance.ProcessName + "\" not found");
                }
            }
            else
            {
                Stop();
            }
        }

        private void LoadSlotButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int slot = int.Parse(b.Text);

            LoadSavestate(slot);
        }

        private void save_Button_Click(object sender, EventArgs e)
        {
            SaveSavestate(false, true);
        }

        private void load_Button_Click(object sender, EventArgs e)
        {
            LoadCurrent();
        }

        private void savestatesCountSet_Button_Click(object sender, EventArgs e)
        {
            _maxSaveSlot = (int)savestatesCount_NumericUpDown.Value;
            if (_currentSaveSlot > _maxSaveSlot) _currentSaveSlot = 1;

            SetSavestateButtons();
        }

        private void hotkeys_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _inputHandler.HotkeysOn = hotkeys_CheckBox.Checked;
        }

        private void focusGameWithA_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _inputHandler.FocusGame = focusGameWithA_CheckBox.Checked;
        }

        private void requireShift_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _inputHandler.RequireShift = requireShift_CheckBox.Checked;
        }

        private void slotLeft_Button_Click(object sender, EventArgs e)
        {
            DecreaseSlot();
        }

        private void slotRight_Button_Click(object sender, EventArgs e)
        {
            IncreaseSlot();
        }

        private void refreshControllerList_Button_Click(object sender, EventArgs e)
        {
            controllerList_ComboBox.Text = string.Empty;
            _inputHandler.SelectedJoystick = null;

            _inputHandler.RefreshControllers();
            controllerList_ComboBox.Items.Clear();
            controllerList_ComboBox.Items.AddRange(_inputHandler.GetJoystickNames());
        }

        private void controllerList_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (controllerList_ComboBox.Text != string.Empty)
            {
                _inputHandler.SelectedJoystick = _inputHandler.Joysticks[controllerList_ComboBox.SelectedIndex];
            }
        }

        private void buttonTest_Button_Click(object sender, EventArgs e)
        {
            if (controllerList_ComboBox.Text != string.Empty)
            {
                _testDialog = new InputEditDialog(_inputHandler);
                _testDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("No controller selected.");
            }
        }
    }
}
