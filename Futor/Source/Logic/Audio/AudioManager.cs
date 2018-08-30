using System;
using System.Collections.Generic;
using System.Linq;
using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.SoundOut;
using CSCore.Streams;

namespace Futor
{
    public class AudioManager
    {
        public class AudioManagerEventArgs : EventArgs
        {
            public AudioManager AudioManager { get; }

            public AudioManagerEventArgs(AudioManager audioManager)
            {
                AudioManager = audioManager;
            }
        }

        const int _kMinimumLatencyMilliseconds = 5;

        readonly MMDeviceEnumerator _mmDeviceEnumerator;
        WasapiCapture _soundIn;
        WasapiOut _soundOut;
        SampleProcessor _sampleProcessor;
        bool _working;
        int _latencyMilliseconds = _kMinimumLatencyMilliseconds;
        string _inputDeviceName;
        string _outputDeviceName;

        public event EventHandler<AudioManagerEventArgs> OnInputDeviceChanged;
        public event EventHandler<AudioManagerEventArgs> OnOutputDeviceChanged;
        public event EventHandler<AudioManagerEventArgs> OnLatencyMillisecondsChanged;
        public event EventHandler<AudioManagerEventArgs> OnAudionConnectionStarted;
        public event EventHandler<AudioManagerEventArgs> OnAudionConnectionFinished;

        public string InputDeviceName
        {
            get => _inputDeviceName;
            set
            {
                if (_inputDeviceName == value)
                    return;

                _inputDeviceName = value;

                RestartIfWorking();

                OnInputDeviceChanged?.Invoke(this, new AudioManagerEventArgs(this));
            }
        }

        public string OutputDeviceName
        {
            get => _outputDeviceName;
            set
            {
                if (_outputDeviceName == value)
                    return;

                _outputDeviceName = value;

                RestartIfWorking();

                OnOutputDeviceChanged?.Invoke(this, new AudioManagerEventArgs(this));
            }
        }

        public List<MMDevice> GetInputMMDevices()
        {
            return _mmDeviceEnumerator.EnumAudioEndpoints(DataFlow.Capture, DeviceState.Active)
                .ToList();
        }

        public List<MMDevice> GetOutputMMDevices()
        {
            return _mmDeviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active)
                .ToList();
        }

        public int LatencyMilliseconds
        {
            get => _latencyMilliseconds;
            set
            {
                if (_latencyMilliseconds == value)
                    return;

                _latencyMilliseconds = Math.Max(_kMinimumLatencyMilliseconds, value);

                RestartIfWorking();

                OnLatencyMillisecondsChanged?.Invoke(this, new AudioManagerEventArgs(this));
            }
        }

        public int SampleRate => (!_working) ? 0 : _soundOut.WaveSource.WaveFormat.SampleRate;

        public SampleProcessor SampleProcessor
        {
            get => _sampleProcessor;
            set
            {
                if (_sampleProcessor == value)
                    return;

                _sampleProcessor = value;

                RestartIfWorking();
            }
        }

        public AudioManager()
        {
            _mmDeviceEnumerator = new MMDeviceEnumerator();
        }

        public void Start()
        {
            if (_working)
                throw new Exception("Call Start when working.");

            var inputMMDevices = GetInputMMDevices();
            var outputMMDevices = GetOutputMMDevices();
            if (!inputMMDevices.Any()
                || !outputMMDevices.Any())
                return;

            // DEBUG
            InputDeviceName = "CABLE Output (VB-Audio Virtual Cable)";

            var inputDevice = inputMMDevices.Find(x => x.FriendlyName == InputDeviceName);
            if (inputDevice == null)
            {
                inputDevice = inputMMDevices.First();
                InputDeviceName = inputDevice.FriendlyName;
            }

            var outputDevice = outputMMDevices.Find(x => x.FriendlyName == OutputDeviceName);
            if (outputDevice == null)
            {
                outputDevice = outputMMDevices.First();
                OutputDeviceName = outputDevice.FriendlyName;
            }

            const AudioClientShareMode audioClientSharedMode = AudioClientShareMode.Shared;

            _soundIn = new WasapiCapture(false, audioClientSharedMode, LatencyMilliseconds)
            {
                Device = inputDevice
            };

            _soundIn.Initialize();
            _soundIn.Start();

            var soundInSource = new SoundInSource(_soundIn)
            {
                FillWithZeros = true
            };

            IWaveSource waveSource = soundInSource;

            if (SampleProcessor != null)
            {
                SampleProcessor.SampleSource = soundInSource.ToSampleSource();
                waveSource = SampleProcessor.ToWaveSource();
            }

            _soundOut = new WasapiOut(false, audioClientSharedMode, LatencyMilliseconds)
            {
                Device = outputDevice
            };

            _soundOut.Initialize(waveSource);

            _soundOut.Play();

            _working = true;

            OnAudionConnectionStarted?.Invoke(this, new AudioManagerEventArgs(this));
        }

        public void Finish()
        {
            if (!_working)
                throw new Exception("Call Finish when not working.");

            _soundOut?.Dispose();
            _soundIn?.Dispose();

            _soundOut = null;
            _soundIn = null;

            _working = false;

            OnAudionConnectionFinished?.Invoke(this, new AudioManagerEventArgs(this));
        }

        void RestartIfWorking()
        {
            if (!_working)
                return;

            Finish();
            Start();
        }
    }
}
