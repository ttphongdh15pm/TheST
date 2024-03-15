using NAudio.Wave;

namespace TheST.App.Controls
{
    public partial class WaveFormatInfo : UserControl
    {
        private const string Unknown = "Unknown";
        private WaveFormat? _waveFormat;
        public int AverageBytesPerSecond => _waveFormat?.AverageBytesPerSecond ?? -1;

        public int BitsPerSample => _waveFormat?.BitsPerSample ?? -1;

        public int BlockAlign => _waveFormat?.BlockAlign ?? -1;

        public int Channels => _waveFormat?.Channels ?? -1;

        public WaveFormatEncoding Encoding => _waveFormat?.Encoding ?? WaveFormatEncoding.Unknown;

        public int SampleRate => _waveFormat?.SampleRate ?? -1;

        public string Title
        {
            get => grpMain.Text;
            set
            {
                grpMain.Text = value;
                Validate();
            }
        }

        public WaveFormat? WaveFormat
        {
            get => _waveFormat;
            set
            {
                _waveFormat = value;
                Populate();
                Validate();
            }
        }

        public WaveFormatInfo()
        {
            InitializeComponent();
        }

        private void Populate()
        {
            lblBitsPerSample.Text = BitsPerSample == -1 ? Unknown : BitsPerSample.ToString();
            lblChannels.Text = Channels == -1 ? Unknown : Channels.ToString();
            lblSampleRate.Text = SampleRate == -1 ? Unknown : SampleRate.ToString();
            lblEncoding.Text = Encoding.ToString() ?? Unknown;
            lblAverageBytesPerSecond.Text = AverageBytesPerSecond == -1 ? Unknown : AverageBytesPerSecond.ToString();
            lblBlockAlign.Text = BlockAlign == -1 ? Unknown : BlockAlign.ToString();
        }
    }
}