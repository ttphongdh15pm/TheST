namespace TheST.App.Controls
{
    partial class DeviceConfiguration
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            _cbbCaptureDevice = new ComboBox();
            label2 = new Label();
            _cbbPlaybackDevice = new ComboBox();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(_cbbCaptureDevice);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(_cbbPlaybackDevice);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(320, 120);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Device setup";
            // 
            // _cbbCaptureDevice
            // 
            _cbbCaptureDevice.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbbCaptureDevice.FormattingEnabled = true;
            _cbbCaptureDevice.Location = new Point(6, 81);
            _cbbCaptureDevice.Name = "_cbbCaptureDevice";
            _cbbCaptureDevice.Size = new Size(297, 23);
            _cbbCaptureDevice.TabIndex = 7;
            _cbbCaptureDevice.SelectedIndexChanged += new EventHandler(OnCaptureDeviceSelectedIndexChanged);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 63);
            label2.Name = "label2";
            label2.Size = new Size(86, 15);
            label2.TabIndex = 6;
            label2.Text = "Capture device";
            // 
            // _cbbPlaybackDevice
            // 
            _cbbPlaybackDevice.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbbPlaybackDevice.FormattingEnabled = true;
            _cbbPlaybackDevice.Location = new Point(6, 37);
            _cbbPlaybackDevice.Name = "_cbbPlaybackDevice";
            _cbbPlaybackDevice.Size = new Size(297, 23);
            _cbbPlaybackDevice.TabIndex = 5;
            _cbbPlaybackDevice.SelectedIndexChanged += new EventHandler(OnPlaybackDeviceSelectedIndexChanged);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 4;
            label1.Text = "Playback device";
            // 
            // DeviceSetup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            MaximumSize = new Size(320, 120);
            MinimumSize = new Size(320, 120);
            Name = "DeviceSetup";
            Size = new Size(320, 120);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private GroupBox groupBox1;
        private ComboBox _cbbCaptureDevice;
        private Label label2;
        private ComboBox _cbbPlaybackDevice;
        private Label label1;
    }
}
