using System.Collections.Generic;
using System.Linq;
using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.SoundOut;
using CSCore.Streams;
using PitchShifter;

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
        PluginsStackProcessor _pluginsStack;
        bool _working;

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

        public void Init()
        {
            _mmDeviceEnumerator = new MMDeviceEnumerator();

            /////
            _inputDeviceNumber = 3;
            _outputDevicesNumber = 0;
        }

        public void Start()
        {
            const AudioClientShareMode audioClientSharedMode = AudioClientShareMode.Shared;
            const int latencyMs = 5;

            _soundIn = new WasapiCapture(false, audioClientSharedMode, latencyMs);
            _soundIn.Device = InputMMDevices[_inputDeviceNumber];
            _soundIn.Initialize();
            _soundIn.Start();

            var soundInSource = new SoundInSource(_soundIn)
            {
                FillWithZeros = true
            };

            _pluginsStack = new PluginsStackProcessor(soundInSource.ToSampleSource());

            _soundOut = new WasapiOut(false, audioClientSharedMode, latencyMs);
            _soundOut.Device = OutputMMDevices[_outputDevicesNumber];
            _soundOut.Initialize(_pluginsStack.ToWaveSource());

            _soundOut.Play();
        }

        public void Finish()
        {
            _soundOut?.Dispose();
            _soundIn?.Dispose();

            _soundOut = null;
            _soundIn = null;
        }

        public void Restart()
        {
            Finish();
            Start();
        }
    }
}
