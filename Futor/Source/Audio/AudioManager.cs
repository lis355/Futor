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
        public class Device
        {
            public string Name { get; private set; }

            public Device(MMDevice device)
            {
                Name = device.FriendlyName;
            }
        }

        MMDeviceEnumerator _mmDeviceEnumerator;
        int _inputDeviceNumber;
        int _outputDevicesNumber;
        WasapiCapture _soundIn;
        WasapiOut _soundOut;
        Processor _sampleProcessor;
        bool _working;
        uint _latencyMilliseconds = 5;

        List<MMDevice> InputMMDevices
        {
            get
            {
                return _mmDeviceEnumerator.EnumAudioEndpoints(DataFlow.Capture, DeviceState.Active)
                    .ToList();
            }
        }

        public IList<Device> InputDevices
        {
            get
            {
                return InputMMDevices
                    .Select(x => new Device(x))
                    .ToList();
            }
        }
        
        List<MMDevice> OutputMMDevices
        {
            get
            {
                return _mmDeviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active)
                    .ToList();
            }
        }

        public IList<Device> OutputDevices
        {
            get
            {
                return OutputMMDevices
                    .Select(x => new Device(x))
                    .ToList();
            }
        }

        public uint LatencyMilliseconds
        {
            get { return _latencyMilliseconds; }
            set
            {
                if (_latencyMilliseconds == value)
                    return;

                _latencyMilliseconds = value;

                Restart();
            }
        }

        public Processor SampleProcessor
        {
            get { return _sampleProcessor; }
            set
            {
                if (_sampleProcessor == value)
                    return;

                _sampleProcessor = value;
                
                Restart();
            }
        }
        
        public void Init()
        {
            _mmDeviceEnumerator = new MMDeviceEnumerator();

            /////
            _inputDeviceNumber = 3;
            _outputDevicesNumber = 0;
        }

        public void Start()
        {
            if (_working)
                throw new Exception("Call Start when working.");

            const AudioClientShareMode audioClientSharedMode = AudioClientShareMode.Shared;

            _soundIn = new WasapiCapture(false, audioClientSharedMode, (int)LatencyMilliseconds);
            _soundIn.Device = InputMMDevices[_inputDeviceNumber];
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

            _soundOut = new WasapiOut(false, audioClientSharedMode, (int)LatencyMilliseconds);
            _soundOut.Device = OutputMMDevices[_outputDevicesNumber];
            _soundOut.Initialize(waveSource);

            _soundOut.Play();

            _working = true;
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
        }

        public void Restart()
        {
            if (_working)
            {
                Finish();
                Start();
            }
        }
    }
}
