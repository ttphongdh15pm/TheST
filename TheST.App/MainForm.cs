using Audio;
using Audio.Capture;
using Audio.Playback;
using NAudio.Wave;
using System.Runtime.InteropServices;
using TheST.App.AudioProcessing;
using TheST.App.Controls;
using TheST.App.EventArguments;
using TheST.Core.Buffers;
using TheST.Models;
using TheST.Sockets;

namespace TheST.App
{
    public partial class MainForm : Form, IAudioBufferDataHandler
    {
        const string StartLabel = "Start";
        const string StopLabel = "Stop";
        private readonly IAudioCapture _audioCapture;
        private readonly IAudioPlayback _audioPlayback;
        private WaveFormat _waveFormat = new WaveFormat(8000, 16, 1);
        private readonly IAudioPipeline _audioPipeline;
        public MainForm()
        {
            InitializeComponent();
            _audioCapture = new AudioCapture(_waveFormat);
            _audioCapture.DataAvailable += AudioCapture_DataAvailable;
            _audioPlayback = new AudioPlayback(_waveFormat);
            _startButton.Text = StartLabel;
            _audioPipeline = new EmptyAudioPipeline(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _waveFormatInfo.Title = "Playback Wave Format";
            _waveFormatInfo.WaveFormat = _audioCapture.WaveFormat;
            _waveFormatConfiguration.SelectedWaveFormatChanged += WaveFormatChanged;
        }

        private void WaveFormatChanged(object? sender, WaveFormat? e)
        {
            if (e == null)
            {
                return;
            }

            //_audioCapture.WaveFormat = e;
            //_audioPlayback.WaveFormat = e;
            //_waveFormatInfo.WaveFormat = e;
        }

        private void AudioCapture_DataAvailable(object? sender, ReadOnlyMemory<byte> inputSamples)
        {
            _audioPipeline?.Put(inputSamples.Span);
        }

        private void HandleStartButtonClick(object sender, EventArgs e)
        {
            if (_startButton.Text == StartLabel)
            {
                _audioCapture.StartCapturing();
                _audioPlayback.Play();
                _audioPipeline?.Start();
                _startButton.Text = StopLabel;
                _waveFormatConfiguration.Enabled = false;
                _deviceConfiguration.Enabled = false;
                _ckbApplyEffect.Enabled = false;
            }
            else
            {
                _startButton.Enabled = true;
                _audioCapture.StopCapturing();
                _audioPlayback.Stop();
                _startButton.Text = StartLabel;
                _waveFormatConfiguration.Enabled = true;
                _deviceConfiguration.Enabled = true;
                _ckbApplyEffect.Enabled = true;
            }
        }

        private void CaptureDeviceChanged(object sender, DeviceChangedArgs e)
        {
            if (sender is DeviceConfiguration deviceConfiguration &&
                deviceConfiguration.SelectedCaptureDevice is Device selectedDevice)
            {
                _audioCapture.UpdateCaptureDevice(selectedDevice.Id);
            }
        }

        private void PlaybackDeviceChanged(object sender, DeviceChangedArgs e)
        {
            if (sender is DeviceConfiguration deviceConfiguration &&
                deviceConfiguration.SelectedPlaybackDevice is Device selectedDevice)
            {
                _audioPlayback.UpdatePlaybackDevice(selectedDevice.Id);
            }
        }

        public void ReceiveBuffer(ReadOnlySpan<byte> buffer)
        {
            _audioPlayback.AddSample(buffer);
        }

        private void ckbApplyEffect_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}