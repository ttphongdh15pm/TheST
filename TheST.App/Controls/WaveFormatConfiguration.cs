using Audio;
using NAudio.Wave;

namespace TheST.App.Controls
{
    public partial class WaveFormatConfiguration : UserControl
    {
        public EventHandler<WaveFormat?>? SelectedWaveFormatChanged;

        private List<WaveFormatWrapper> _waveFormats = new List<WaveFormatWrapper>();

        public WaveFormat? SelectedWaveFormat
        {
            get
            {
                if (cbbWaveFormats.SelectedItem is WaveFormatWrapper waveFormat)
                {
                    return waveFormat;
                }
                return null;
            }
        }

        public string Title
        {
            get => lblTitle.Text;
            set
            {
                lblTitle.Text = value;
                Invalidate();
            }
        }

        public WaveFormatConfiguration()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _waveFormats.AddRange(WaveFormats.WaveFormatsProvider.WaveFormats.Select(wf => new WaveFormatWrapper(wf)));
            cbbWaveFormats.DataSource = _waveFormats;
            cbbWaveFormats.DisplayMember = "DisplayText";
            cbbWaveFormats.ValueMember = "Key";
        }

        private void HandleWaveFormatChanged(object sender, EventArgs e)
        {
            SelectedWaveFormatChanged?.Invoke(this, SelectedWaveFormat);
        }

        private class WaveFormatWrapper : WaveFormat
        {
            public WaveFormatWrapper(WaveFormat waveFormat)
                : base(waveFormat.SampleRate, waveFormat.BitsPerSample, waveFormat.Channels)
            {
                waveFormatTag = waveFormat.Encoding;
                blockAlign = (short)waveFormat.BlockAlign;
                extraSize = (short)waveFormat.ExtraSize;
            }
            public string Key => $"{Encoding}-{SampleRate}-{BitsPerSample}-{Channels}";
            public string DisplayText => ToString();
            public override string ToString()
            {
                return $"[{Encoding}] - {SampleRate} Hz, {BitsPerSample} bit, {Channels} channel(s))";
            }
        }
    }
}