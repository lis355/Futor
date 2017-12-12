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
        List<MMDevice> _inputDevices;
        List<MMDevice> _outputDevices;
        int _inputDeviceNumber;
        int _outputDevicesNumber;
        WasapiCapture _soundIn;
        WasapiOut _soundOut;
        PluginsStackProcessor _pluginsStack;
        bool _working;
        
        public void Init()
        {
            var deviceEnum = new MMDeviceEnumerator();
            _inputDevices = deviceEnum.EnumAudioEndpoints(DataFlow.Capture, DeviceState.Active).ToList();
            _outputDevices = deviceEnum.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active).ToList();

            /////
            _inputDeviceNumber = 3;
            _outputDevicesNumber = 0;
        }

        public void Start()
        {
            const AudioClientShareMode audioClientSharedMode = AudioClientShareMode.Shared;
            const int latencyMs = 5;

            _soundIn = new WasapiCapture(false, audioClientSharedMode, latencyMs);
            _soundIn.Device = _inputDevices[_inputDeviceNumber];
            _soundIn.Initialize();
            _soundIn.Start();

            var soundInSource = new SoundInSource(_soundIn)
            {
                FillWithZeros = true
            };

            _pluginsStack = new PluginsStackProcessor(soundInSource.ToSampleSource());

            _soundOut = new WasapiOut(false, audioClientSharedMode, latencyMs);
            _soundOut.Device = _outputDevices[_outputDevicesNumber];
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
