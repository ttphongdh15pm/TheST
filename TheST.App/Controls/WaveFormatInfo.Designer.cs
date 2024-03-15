namespace TheST.App.Controls
{
    partial class WaveFormatInfo
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
            grpMain = new GroupBox();
            lblBlockAlign = new Label();
            lblChannels = new Label();
            lblEncoding = new Label();
            lblSampleRate = new Label();
            lblBitsPerSample = new Label();
            lblAverageBytesPerSecond = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            grpMain.SuspendLayout();
            SuspendLayout();
            // 
            // grpMain
            // 
            grpMain.Controls.Add(lblBlockAlign);
            grpMain.Controls.Add(lblChannels);
            grpMain.Controls.Add(lblEncoding);
            grpMain.Controls.Add(lblSampleRate);
            grpMain.Controls.Add(lblBitsPerSample);
            grpMain.Controls.Add(lblAverageBytesPerSecond);
            grpMain.Controls.Add(label6);
            grpMain.Controls.Add(label5);
            grpMain.Controls.Add(label4);
            grpMain.Controls.Add(label3);
            grpMain.Controls.Add(label2);
            grpMain.Controls.Add(label1);
            grpMain.Dock = DockStyle.Fill;
            grpMain.Location = new Point(0, 0);
            grpMain.Name = "grpMain";
            grpMain.Size = new Size(230, 120);
            grpMain.TabIndex = 0;
            grpMain.TabStop = false;
            grpMain.Text = "Wave format";
            // 
            // lblBlockAlign
            // 
            lblBlockAlign.AutoSize = true;
            lblBlockAlign.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBlockAlign.Location = new Point(148, 94);
            lblBlockAlign.Name = "lblBlockAlign";
            lblBlockAlign.Size = new Size(61, 15);
            lblBlockAlign.TabIndex = 11;
            lblBlockAlign.Text = "Unknown";
            // 
            // lblChannels
            // 
            lblChannels.AutoSize = true;
            lblChannels.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblChannels.Location = new Point(148, 79);
            lblChannels.Name = "lblChannels";
            lblChannels.Size = new Size(61, 15);
            lblChannels.TabIndex = 10;
            lblChannels.Text = "Unknown";
            // 
            // lblEncoding
            // 
            lblEncoding.AutoSize = true;
            lblEncoding.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEncoding.Location = new Point(148, 64);
            lblEncoding.Name = "lblEncoding";
            lblEncoding.Size = new Size(61, 15);
            lblEncoding.TabIndex = 9;
            lblEncoding.Text = "Unknown";
            // 
            // lblSampleRate
            // 
            lblSampleRate.AutoSize = true;
            lblSampleRate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSampleRate.Location = new Point(148, 49);
            lblSampleRate.Name = "lblSampleRate";
            lblSampleRate.Size = new Size(61, 15);
            lblSampleRate.TabIndex = 8;
            lblSampleRate.Text = "Unknown";
            // 
            // lblBitsPerSample
            // 
            lblBitsPerSample.AutoSize = true;
            lblBitsPerSample.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBitsPerSample.Location = new Point(148, 34);
            lblBitsPerSample.Name = "lblBitsPerSample";
            lblBitsPerSample.Size = new Size(61, 15);
            lblBitsPerSample.TabIndex = 7;
            lblBitsPerSample.Text = "Unknown";
            // 
            // lblAverageBytesPerSecond
            // 
            lblAverageBytesPerSecond.AutoSize = true;
            lblAverageBytesPerSecond.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAverageBytesPerSecond.Location = new Point(148, 19);
            lblAverageBytesPerSecond.Name = "lblAverageBytesPerSecond";
            lblAverageBytesPerSecond.Size = new Size(61, 15);
            lblAverageBytesPerSecond.TabIndex = 6;
            lblAverageBytesPerSecond.Text = "Unknown";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(82, 94);
            label6.Name = "label6";
            label6.Size = new Size(70, 15);
            label6.TabIndex = 5;
            label6.Text = "Block Align:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(92, 64);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 4;
            label5.Text = "Encoding:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(93, 79);
            label4.Name = "label4";
            label4.Size = new Size(59, 15);
            label4.TabIndex = 3;
            label4.Text = "Channels:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(77, 49);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 2;
            label3.Text = "Sample Rate:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(61, 34);
            label2.Name = "label2";
            label2.Size = new Size(91, 15);
            label2.TabIndex = 1;
            label2.Text = "Bits Per Sample:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(146, 15);
            label1.TabIndex = 0;
            label1.Text = "Average Bytes Per Second:";
            // 
            // WaveFormatInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(grpMain);
            MaximumSize = new Size(230, 120);
            MinimumSize = new Size(230, 120);
            Name = "WaveFormatInfo";
            Size = new Size(230, 120);
            grpMain.ResumeLayout(false);
            grpMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpMain;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label5;
        private Label label6;
        private Label lblBlockAlign;
        private Label lblChannels;
        private Label lblEncoding;
        private Label lblSampleRate;
        private Label lblBitsPerSample;
        private Label lblAverageBytesPerSecond;
    }
}
