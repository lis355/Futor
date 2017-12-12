using CSCore;

namespace PitchShifter
{
    public abstract class Processor : ISampleSource
    {
        readonly ISampleSource _sampleSource;

        protected Processor(ISampleSource sampleSource)
        {
            _sampleSource = sampleSource;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var samples = _sampleSource.Read(buffer, offset, count);

            Process(buffer, offset, samples);

            return samples;
        }

        public abstract void Process(float[] buffer, int offset, int samples);

        public void Dispose()
        {
            _sampleSource?.Dispose();
        }

        public bool CanSeek
        {
            get { return _sampleSource.CanSeek; }
        }

        public WaveFormat WaveFormat
        {
            get { return _sampleSource.WaveFormat; }
        }

        public long Position
        {
            get { return _sampleSource.Position; }
            set { _sampleSource.Position = value; }
        }

        public long Length
        {
            get { return _sampleSource.Length; }
        }
    }
}
