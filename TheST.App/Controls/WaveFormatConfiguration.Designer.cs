namespace TheST.App.Controls
{
    partial class WaveFormatConfiguration
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
            cbbWaveFormats = new ComboBox();
            lblTitle = new Label();
            SuspendLayout();
            // 
            // cbbWaveFormats
            // 
            cbbWaveFormats.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbWaveFormats.FormattingEnabled = true;
            cbbWaveFormats.Location = new Point(3, 28);
            cbbWaveFormats.Name = "cbbWaveFormats";
            cbbWaveFormats.Size = new Size(298, 23);
            cbbWaveFormats.TabIndex = 0;
            cbbWaveFormats.SelectedIndexChanged += HandleWaveFormatChanged;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(3, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(75, 15);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Wave format";
            // 
            // WaveFormatConfiguration
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitle);
            Controls.Add(cbbWaveFormats);
            MaximumSize = new Size(308, 55);
            MinimumSize = new Size(308, 55);
            Name = "WaveFormatConfiguration";
            Size = new Size(308, 55);
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private ComboBox cbbWaveFormats;
        private Label lblTitle;
    }
}
