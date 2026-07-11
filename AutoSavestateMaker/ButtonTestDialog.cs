using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoSavestateMaker
{
    public partial class ButtonTestDialog : Form
    {
        private InputHandler _inputHandler;

        public ButtonTestDialog(InputHandler inputHandler)
        {
            InitializeComponent();

            _inputHandler = inputHandler;
            _inputHandler.LastButtonPressed = -1;
            _inputHandler.OnTick += () => lastButton_Label.Text = _inputHandler.LastButtonPressed.ToString();

            FormClosed += (s, e) => { _inputHandler.OnTick = () => { }; };
        }
    }
}
