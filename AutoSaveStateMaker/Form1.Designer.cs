namespace AutoSavestateMaker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            interval_NumericUpDown = new NumericUpDown();
            run_CheckBox = new CheckBox();
            label1 = new Label();
            lastCreatedSlot_Label = new Label();
            settings_Panel = new Panel();
            requireR_CheckBox = new CheckBox();
            focusGameWithA_CheckBox = new CheckBox();
            hotkeys_CheckBox = new CheckBox();
            label6 = new Label();
            savestatesCountSet_Button = new Button();
            savestatesCount_NumericUpDown = new NumericUpDown();
            label3 = new Label();
            status_PictureBox = new PictureBox();
            save_Button = new Button();
            load_Button = new Button();
            ((System.ComponentModel.ISupportInitialize)interval_NumericUpDown).BeginInit();
            settings_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)savestatesCount_NumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)status_PictureBox).BeginInit();
            SuspendLayout();
            // 
            // interval_NumericUpDown
            // 
            interval_NumericUpDown.Location = new Point(13, 34);
            interval_NumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            interval_NumericUpDown.Name = "interval_NumericUpDown";
            interval_NumericUpDown.Size = new Size(62, 23);
            interval_NumericUpDown.TabIndex = 0;
            interval_NumericUpDown.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // run_CheckBox
            // 
            run_CheckBox.AutoSize = true;
            run_CheckBox.Location = new Point(11, 6);
            run_CheckBox.Name = "run_CheckBox";
            run_CheckBox.Size = new Size(47, 19);
            run_CheckBox.TabIndex = 1;
            run_CheckBox.Text = "Run";
            run_CheckBox.UseVisualStyleBackColor = true;
            run_CheckBox.CheckedChanged += run_CheckBox_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(182, 14);
            label1.Name = "label1";
            label1.Size = new Size(28, 15);
            label1.TabIndex = 2;
            label1.Text = "Last";
            // 
            // lastCreatedSlot_Label
            // 
            lastCreatedSlot_Label.AutoSize = true;
            lastCreatedSlot_Label.Location = new Point(216, 14);
            lastCreatedSlot_Label.Name = "lastCreatedSlot_Label";
            lastCreatedSlot_Label.Size = new Size(41, 15);
            lastCreatedSlot_Label.TabIndex = 2;
            lastCreatedSlot_Label.Text = "<last>";
            // 
            // settings_Panel
            // 
            settings_Panel.BorderStyle = BorderStyle.FixedSingle;
            settings_Panel.Controls.Add(requireR_CheckBox);
            settings_Panel.Controls.Add(focusGameWithA_CheckBox);
            settings_Panel.Controls.Add(hotkeys_CheckBox);
            settings_Panel.Controls.Add(label6);
            settings_Panel.Controls.Add(savestatesCountSet_Button);
            settings_Panel.Controls.Add(savestatesCount_NumericUpDown);
            settings_Panel.Controls.Add(label3);
            settings_Panel.Controls.Add(interval_NumericUpDown);
            settings_Panel.Location = new Point(3, 31);
            settings_Panel.Name = "settings_Panel";
            settings_Panel.Size = new Size(162, 226);
            settings_Panel.TabIndex = 5;
            // 
            // requireR_CheckBox
            // 
            requireR_CheckBox.AutoSize = true;
            requireR_CheckBox.Location = new Point(6, 195);
            requireR_CheckBox.Name = "requireR_CheckBox";
            requireR_CheckBox.Size = new Size(76, 19);
            requireR_CheckBox.TabIndex = 12;
            requireR_CheckBox.Text = "Require R";
            requireR_CheckBox.UseVisualStyleBackColor = true;
            requireR_CheckBox.CheckedChanged += requireR_CheckBox_CheckedChanged;
            // 
            // focusGameWithA_CheckBox
            // 
            focusGameWithA_CheckBox.AutoSize = true;
            focusGameWithA_CheckBox.Location = new Point(8, 123);
            focusGameWithA_CheckBox.Name = "focusGameWithA_CheckBox";
            focusGameWithA_CheckBox.Size = new Size(127, 19);
            focusGameWithA_CheckBox.TabIndex = 8;
            focusGameWithA_CheckBox.Text = "Focus game with A";
            focusGameWithA_CheckBox.UseVisualStyleBackColor = true;
            focusGameWithA_CheckBox.CheckedChanged += focusGameWithA_CheckBox_CheckedChanged;
            // 
            // hotkeys_CheckBox
            // 
            hotkeys_CheckBox.AutoSize = true;
            hotkeys_CheckBox.Location = new Point(7, 170);
            hotkeys_CheckBox.Name = "hotkeys_CheckBox";
            hotkeys_CheckBox.Size = new Size(100, 19);
            hotkeys_CheckBox.TabIndex = 11;
            hotkeys_CheckBox.Text = "Dpad Hotkeys";
            hotkeys_CheckBox.UseVisualStyleBackColor = true;
            hotkeys_CheckBox.CheckedChanged += hotkeys_CheckBox_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 65);
            label6.Name = "label6";
            label6.Size = new Size(61, 15);
            label6.TabIndex = 10;
            label6.Text = "Savestates";
            // 
            // savestatesCountSet_Button
            // 
            savestatesCountSet_Button.Location = new Point(73, 83);
            savestatesCountSet_Button.Name = "savestatesCountSet_Button";
            savestatesCountSet_Button.Size = new Size(56, 23);
            savestatesCountSet_Button.TabIndex = 9;
            savestatesCountSet_Button.Text = "Set";
            savestatesCountSet_Button.UseVisualStyleBackColor = true;
            savestatesCountSet_Button.Click += savestatesCountSet_Button_Click;
            // 
            // savestatesCount_NumericUpDown
            // 
            savestatesCount_NumericUpDown.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            savestatesCount_NumericUpDown.Location = new Point(11, 83);
            savestatesCount_NumericUpDown.Maximum = new decimal(new int[] { 80, 0, 0, 0 });
            savestatesCount_NumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            savestatesCount_NumericUpDown.Name = "savestatesCount_NumericUpDown";
            savestatesCount_NumericUpDown.Size = new Size(56, 23);
            savestatesCount_NumericUpDown.TabIndex = 8;
            savestatesCount_NumericUpDown.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 13);
            label3.Name = "label3";
            label3.Size = new Size(100, 15);
            label3.TabIndex = 6;
            label3.Text = "Interval (seconds)";
            // 
            // status_PictureBox
            // 
            status_PictureBox.BackColor = Color.Tomato;
            status_PictureBox.Location = new Point(326, 12);
            status_PictureBox.Name = "status_PictureBox";
            status_PictureBox.Size = new Size(48, 48);
            status_PictureBox.TabIndex = 6;
            status_PictureBox.TabStop = false;
            // 
            // save_Button
            // 
            save_Button.Location = new Point(396, 14);
            save_Button.Name = "save_Button";
            save_Button.Size = new Size(47, 23);
            save_Button.TabIndex = 7;
            save_Button.Text = "Save";
            save_Button.UseVisualStyleBackColor = true;
            save_Button.Click += save_Button_Click;
            // 
            // load_Button
            // 
            load_Button.Location = new Point(449, 14);
            load_Button.Name = "load_Button";
            load_Button.Size = new Size(47, 23);
            load_Button.TabIndex = 7;
            load_Button.Text = "Load";
            load_Button.UseVisualStyleBackColor = true;
            load_Button.Click += load_Button_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(602, 343);
            Controls.Add(load_Button);
            Controls.Add(save_Button);
            Controls.Add(status_PictureBox);
            Controls.Add(settings_Panel);
            Controls.Add(lastCreatedSlot_Label);
            Controls.Add(label1);
            Controls.Add(run_CheckBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Auto Savestate Maker";
            ((System.ComponentModel.ISupportInitialize)interval_NumericUpDown).EndInit();
            settings_Panel.ResumeLayout(false);
            settings_Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)savestatesCount_NumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)status_PictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown interval_NumericUpDown;
        private CheckBox run_CheckBox;
        private Label label1;
        private Label lastCreatedSlot_Label;
        private Panel settings_Panel;
        private Label label3;
        private PictureBox status_PictureBox;
        private Button save_Button;
        private Button load_Button;
        private Label label6;
        private Button savestatesCountSet_Button;
        private NumericUpDown savestatesCount_NumericUpDown;
        private CheckBox hotkeys_CheckBox;
        private CheckBox focusGameWithA_CheckBox;
        private CheckBox requireR_CheckBox;
    }
}
