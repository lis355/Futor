using System;
using System.Linq;
using NAudio.CoreAudioApi;
using NAudio.Utils;
using NAudio.Wave;

namespace Futor
{
    public class WaveProvider32 : IWaveProvider
    {
        const int _kBitsInByte = 8;

        public delegate int ReadDelegate(float[] buffer, int offset, int count);

        public WaveFormat WaveFormat { get; }

        public ReadDelegate ReadFunction { get; }

        public WaveProvider32(int sampleRate, int channels, ReadDelegate readFunction)
        {
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
            ReadFunction = readFunction;
        }
        
        public int Read(byte[] buffer, int offset, int count)
        {
            int bytesPerSample = WaveFormat.BitsPerSample / _kBitsInByte;
            var waveBuffer = new WaveBuffer(buffer);
            int samplesRequired = count / bytesPerSample;
            int samplesRead = ReadFloat(waveBuffer, offset / bytesPerSample, samplesRequired);
            return samplesRead * bytesPerSample;
        }

        public int ReadFloat(float[] buffer, int offset, int sampleCount)
        {
            return (ReadFunction != null) ? ReadFunction(buffer, offset, sampleCount) : sampleCount;
        }
    }
    
    class AudioManager
    {
        IWaveIn _waveIn;
        WaveProvider32 _inChannel;
        WaveOut _waveOut;
        WaveProvider32 _outChannel;

        BufferedWaveProvider _buffer;

        public void Init()
        {
            var deviceEnum = new MMDeviceEnumerator();
            var devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();

            _waveIn = new WasapiCapture(devices[3], false, 10);
            //_waveIn.DeviceNumber = 0;
            _waveIn.DataAvailable += (sender, e) =>
            {
                var wb = new WaveBuffer(e.Buffer);
                float[] floatb = wb;

                _buffer.AddSamples(e.Buffer, 0, e.BytesRecorded);
            };

            _waveIn.WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
            //_waveIn.BufferMilliseconds = 10;

            //CircularBuffer

            //var inName = WaveIn.GetCapabilities(_waveIn.DeviceNumber).ProductName;
            
            float[] a = new float[44100];

            //_inChannel = new WaveProvider32(44100, 2, (buffer, offset, count) =>
            //{
            //    bool noise = true;
            //
            //    for (int i = 0; i < count; i++)
            //    {
            //        a[i] = buffer[i + offset];
            //
            //        if (Math.Abs(a[i]) > 1e-3)
            //            noise = false;
            //    }
            //    
            //    //Array.Copy(buffer, offset, a, 0, count);
            //
            //    return count;
            //});

            //_waveIn.Init(_inChannel);

            _buffer = new BufferedWaveProvider(_waveIn.WaveFormat);
            _buffer.DiscardOnBufferOverflow = true;

            _waveIn.StartRecording();
            //_waveIn.Play();

            _waveOut = new WaveOut();
            _waveOut.DeviceNumber = 1;

            var outName = WaveOut.GetCapabilities(_waveOut.DeviceNumber).ProductName;

            //_outChannel = new WaveProvider32(44100, 2, (buffer, offset, count) =>
            //{
            //    for (int i = 0; i < count; i++)
            //        buffer[i + offset] = a[i];
            //
            //    //Array.Copy(a, 0, buffer, offset, count);
            //
            //    return count;
            //});

            _waveOut.Init(_buffer);
            _waveOut.Play();
        }
        
        //public void Start()
        //{
        //    
        //}
        //
        //public void Finish()
        //{
        //    _waveOut.Stop();
        //    _waveOut.Dispose();
        //}
    }
}
