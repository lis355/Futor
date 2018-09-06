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
        bool _firstStart = true;
        bool _working;
        int _latencyMilliseconds = _kMinimumLatencyMilliseconds;
        string _inputDeviceName = string.Empty;
        string _outputDeviceName = string.Empty;

        public Option<bool> IsWorking { get; }

        public string InputDeviceName
        {
            get => _inputDeviceName;
            set
            {
                if (_inputDeviceName == value)
                    return;

                if (FindInputDevice(value) == null)
                    value = string.Empty;

                _inputDeviceName = value;

                RestartIfStarted();

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

                if (FindOutputDevice(value) == null)
                    value = string.Empty;

                _outputDeviceName = value;

                RestartIfStarted();

                OnOutputDeviceChanged?.Invoke(this, new AudioManagerEventArgs(this));
            }
        }
        
        public int LatencyMilliseconds
        {
            get => _latencyMilliseconds;
            set
            {
                if (_latencyMilliseconds == value)
                    return;

                _latencyMilliseconds = Math.Max(_kMinimumLatencyMilliseconds, value);

                RestartIfStarted();

                OnLatencyMillisecondsChanged?.Invoke(this, new AudioManagerEventArgs(this));
            }
        }

        //public int SampleRate => (!IsWorking.Value) ? 0 : _soundOut.WaveSource.WaveFormat.SampleRate;

        public SampleProcessor SampleProcessor
        {
            get => _sampleProcessor;
            set
            {
                if (_sampleProcessor == value)
                    return;

                _sampleProcessor = value;

                RestartIfStarted();
            }
        }

        public event EventHandler<AudioManagerEventArgs> OnInputDeviceChanged;
        public event EventHandler<AudioManagerEventArgs> OnOutputDeviceChanged;
        public event EventHandler<AudioManagerEventArgs> OnLatencyMillisecondsChanged;

        public AudioManager()
        {
            _mmDeviceEnumerator = new MMDeviceEnumerator();

            IsWorking = new Option<bool>(
                () => _working,
                value => _working = value);

            //DEBUG
            IsWorking.OnChanged += (sender, args) => System.Diagnostics.Debug.Print("AudioManager {0}", (args.NewValue) ? "ON" : "OFF");
        }

        public void Start()
        {
            if (IsWorking.Value)
                throw new Exception("Call Start when working.");

            var inputMMDevices = GetInputMMDevices();
            var outputMMDevices = GetOutputMMDevices();
            if (!inputMMDevices.Any()
                || !outputMMDevices.Any())
                return;

            var inputDevice = FindInputDevice(InputDeviceName);
            if (inputDevice == null)
            {
                //inputDevice = inputMMDevices.First();
                InputDeviceName = string.Empty; //inputDevice.FriendlyName;
            }

            var outputDevice = FindOutputDevice(OutputDeviceName);
            if (outputDevice == null)
            {
                //outputDevice = outputMMDevices.First();
                OutputDeviceName = string.Empty; //outputDevice.FriendlyName;
            }

            if (inputDevice == null
                || outputDevice == null)
                return;

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

            IsWorking.Value = true;

            _firstStart = false;
        }

        public void Finish()
        {
            if (!IsWorking.Value)
                throw new Exception("Call Finish when not working.");

            _soundOut.Stop();
            _soundIn.Stop();

            SampleProcessor.SampleSource = null;

            _soundOut?.Dispose();
            _soundIn?.Dispose();

            _soundOut = null;
            _soundIn = null;

            IsWorking.Value = false;
        }

        List<MMDevice> GetInputMMDevices()
        {
            return _mmDeviceEnumerator.EnumAudioEndpoints(DataFlow.Capture, DeviceState.Active).ToList();
        }

        List<MMDevice> GetOutputMMDevices()
        {
            return _mmDeviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active).ToList();
        }

        public IEnumerable<string> GetInputDevicesNames()
        {
            return GetInputMMDevices().Select(x => x.FriendlyName);
        }

        public IEnumerable<string> GetOutputDevicesNames()
        {
            return GetOutputMMDevices().Select(x => x.FriendlyName);
        }

        MMDevice FindInputDevice(string deviceName)
        {
            return GetInputMMDevices().Find(x => x.FriendlyName == deviceName);
        }

        MMDevice FindOutputDevice(string deviceName)
        {
            return GetOutputMMDevices().Find(x => x.FriendlyName == deviceName);
        }

        void RestartIfStarted()
        {
            if (_firstStart)
                return;

            if (IsWorking.Value)
                Finish();

            Start();
        }
    }
}
