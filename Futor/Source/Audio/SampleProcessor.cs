using CSCore;

namespace Futor
{
    public abstract class SampleProcessor : ISampleSource
    {
        public ISampleSource SampleSource { get; set; }
        
        public int Read(float[] buffer, int offset, int count)
        {
            SampleSource?.Read(buffer, offset, count);

            Process(buffer, offset, count);

            return count;
        }

        public abstract void Process(float[] buffer, int offset, int samples);

        public void Dispose()
        {
            SampleSource?.Dispose();
        }

        public bool CanSeek => SampleSource.CanSeek;

        public WaveFormat WaveFormat => SampleSource.WaveFormat;

        public long Position
        {
            get { return SampleSource.Position; }
            set { SampleSource.Position = value; }
        }

        public long Length => SampleSource.Length;
    }
}
