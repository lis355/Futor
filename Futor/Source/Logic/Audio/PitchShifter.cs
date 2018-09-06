using System;

namespace Futor
{
    public class PitchShifter : SampleProcessor
    {
        class SmbPitchShifter
        {
            const int _kMaxFrameLength = 16000;
            readonly float[] _gInFifo = new float[_kMaxFrameLength];
            readonly float[] _gOutFifo = new float[_kMaxFrameLength];
            readonly float[] _gFfTworksp = new float[2 * _kMaxFrameLength];
            readonly float[] _gLastPhase = new float[_kMaxFrameLength / 2 + 1];
            readonly float[] _gSumPhase = new float[_kMaxFrameLength / 2 + 1];
            readonly float[] _gOutputAccum = new float[2 * _kMaxFrameLength];
            readonly float[] _gAnaFreq = new float[_kMaxFrameLength];
            readonly float[] _gAnaMagn = new float[_kMaxFrameLength];
            readonly float[] _gSynFreq = new float[_kMaxFrameLength];
            readonly float[] _gSynMagn = new float[_kMaxFrameLength];
            long _gRover;

            public void PitchShift(float pitchShift, long numSampsToProcess, long fftFrameSize, long osamp, float sampleRate, float[] indata)
            {
                double magn, phase, tmp, window, real, imag;
                double freqPerBin, expct;
                long i, k, qpd, index, inFifoLatency, stepSize, fftFrameSize2;

                var outdata = indata;
                fftFrameSize2 = fftFrameSize / 2;
                stepSize = fftFrameSize / osamp;
                freqPerBin = sampleRate / (double)fftFrameSize;
                expct = 2.0 * Math.PI * stepSize / fftFrameSize;
                inFifoLatency = fftFrameSize - stepSize;

                if (_gRover == 0)
                    _gRover = inFifoLatency;

                for (i = 0; i < numSampsToProcess; i++)
                {
                    _gInFifo[_gRover] = indata[i];
                    outdata[i] = _gOutFifo[_gRover - inFifoLatency];
                    _gRover++;

                    if (_gRover >= fftFrameSize)
                    {
                        _gRover = inFifoLatency;

                        for (k = 0; k < fftFrameSize; k++)
                        {
                            window = -.5 * Math.Cos(2.0 * Math.PI * k / fftFrameSize) + .5;
                            _gFfTworksp[2 * k] = (float)(_gInFifo[k] * window);
                            _gFfTworksp[2 * k + 1] = 0.0F;
                        }

                        ShortTimeFourierTransform(_gFfTworksp, fftFrameSize, -1);

                        for (k = 0; k <= fftFrameSize2; k++)
                        {
                            real = _gFfTworksp[2 * k];
                            imag = _gFfTworksp[2 * k + 1];

                            magn = 2.0 * Math.Sqrt(real * real + imag * imag);
                            phase = Math.Atan2(imag, real);

                            tmp = phase - _gLastPhase[k];
                            _gLastPhase[k] = (float)phase;

                            tmp -= k * expct;

                            qpd = (long)(tmp / Math.PI);
                            if (qpd >= 0)
                                qpd += qpd & 1;
                            else
                                qpd -= qpd & 1;
                            tmp -= Math.PI * qpd;

                            tmp = osamp * tmp / (2.0 * Math.PI);

                            tmp = k * freqPerBin + tmp * freqPerBin;

                            _gAnaMagn[k] = (float)magn;
                            _gAnaFreq[k] = (float)tmp;
                        }

                        for (int zero = 0; zero < fftFrameSize; zero++)
                        {
                            _gSynMagn[zero] = 0;
                            _gSynFreq[zero] = 0;
                        }

                        for (k = 0; k <= fftFrameSize2; k++)
                        {
                            index = (long)(k * pitchShift);
                            if (index <= fftFrameSize2)
                            {
                                _gSynMagn[index] += _gAnaMagn[k];
                                _gSynFreq[index] = _gAnaFreq[k] * pitchShift;
                            }
                        }

                        for (k = 0; k <= fftFrameSize2; k++)
                        {
                            magn = _gSynMagn[k];
                            tmp = _gSynFreq[k];

                            tmp -= k * freqPerBin;

                            tmp /= freqPerBin;

                            tmp = 2.0 * Math.PI * tmp / osamp;

                            tmp += k * expct;

                            _gSumPhase[k] += (float)tmp;
                            phase = _gSumPhase[k];

                            _gFfTworksp[2 * k] = (float)(magn * Math.Cos(phase));
                            _gFfTworksp[2 * k + 1] = (float)(magn * Math.Sin(phase));
                        }

                        for (k = fftFrameSize + 2; k < 2 * fftFrameSize; k++)
                            _gFfTworksp[k] = 0.0F;

                        ShortTimeFourierTransform(_gFfTworksp, fftFrameSize, 1);

                        for (k = 0; k < fftFrameSize; k++)
                        {
                            window = -.5 * Math.Cos(2.0 * Math.PI * k / fftFrameSize) + .5;
                            _gOutputAccum[k] += (float)(2.0 * window * _gFfTworksp[2 * k] / (fftFrameSize2 * osamp));
                        }

                        for (k = 0; k < stepSize; k++)
                            _gOutFifo[k] = _gOutputAccum[k];

                        for (k = 0; k < fftFrameSize; k++)
                        {
                            _gOutputAccum[k] = _gOutputAccum[k + stepSize];
                        }

                        for (k = 0; k < inFifoLatency; k++)
                            _gInFifo[k] = _gInFifo[k + stepSize];
                    }
                }
            }

            void ShortTimeFourierTransform(float[] fftBuffer, long fftFrameSize, long sign)
            {
                float wr, wi, arg, temp;
                float tr, ti, ur, ui;
                long i, bitm, j, le, le2, k;

                for (i = 2; i < 2 * fftFrameSize - 2; i += 2)
                {
                    for (bitm = 2, j = 0; bitm < 2 * fftFrameSize; bitm <<= 1)
                    {
                        if ((i & bitm) != 0)
                            j++;
                        j <<= 1;
                    }

                    if (i < j)
                    {
                        temp = fftBuffer[i];
                        fftBuffer[i] = fftBuffer[j];
                        fftBuffer[j] = temp;
                        temp = fftBuffer[i + 1];
                        fftBuffer[i + 1] = fftBuffer[j + 1];
                        fftBuffer[j + 1] = temp;
                    }
                }

                long max = (long)(Math.Log(fftFrameSize) / Math.Log(2.0) + .5);
                for (k = 0, le = 2; k < max; k++)
                {
                    le <<= 1;
                    le2 = le >> 1;
                    ur = 1.0F;
                    ui = 0.0F;
                    arg = (float)Math.PI / (le2 >> 1);
                    wr = (float)Math.Cos(arg);
                    wi = (float)(sign * Math.Sin(arg));
                    for (j = 0; j < le2; j += 2)
                    {

                        for (i = j; i < 2 * fftFrameSize; i += le)
                        {
                            tr = fftBuffer[i + le2] * ur - fftBuffer[i + le2 + 1] * ui;
                            ti = fftBuffer[i + le2] * ui + fftBuffer[i + le2 + 1] * ur;
                            fftBuffer[i + le2] = fftBuffer[i] - tr;
                            fftBuffer[i + le2 + 1] = fftBuffer[i + 1] - ti;
                            fftBuffer[i] += tr;
                            fftBuffer[i + 1] += ti;

                        }

                        tr = ur * wr - ui * wi;
                        ui = ur * wi + ui * wr;
                        ur = tr;
                    }
                }
            }
        }

        readonly int _fftSize;
        readonly long _osamp;
        readonly SmbPitchShifter _shifterLeft = new SmbPitchShifter();
        readonly SmbPitchShifter _shifterRight = new SmbPitchShifter();
        float _pitchFactorLog;
        int _pitchFactor;

        const int _kMaxAbsIntFactor = 12;
        const float _kLimThresh = 0.95f;
        const float _kLimRange = 1f - _kLimThresh;
        const float _kMPi2 = (float)(Math.PI / 2);

        public OptionMinMax<int> PitchFactor { get; }

        public PitchShifter(int fftSize, long osamp, int initialPitch)
        {
            _fftSize = fftSize;
            _osamp = osamp;

            PitchFactor = new OptionMinMax<int>(
                () => _pitchFactor,
                value =>
                {
                    SetPitchFactor(value);
                },
                -_kMaxAbsIntFactor,
                _kMaxAbsIntFactor);

            SetPitchFactor(initialPitch);
        }

        void SetPitchFactor(int value)
        {
            _pitchFactor = value;

            _pitchFactorLog = (float)Math.Pow(2, Math.Abs(_pitchFactor) / 12f);

            if (value < 0)
                _pitchFactorLog = 1 / _pitchFactorLog;
        }

        public PitchShifter()
            : this(4096, 4L, 0)
        {
        }

        public override void Process(float[] buffer, int offset, int count)
        {
            if (Math.Abs(_pitchFactorLog - 1f) < 1e-5)
                return;

            if (WaveFormat.Channels == 1)
            {
                var mono = new float[count];
                int index = 0;

                for (int sample = offset; sample <= count + offset - 1; sample++)
                {
                    mono[index] = buffer[sample];
                    index += 1;
                }

                _shifterLeft.PitchShift(_pitchFactorLog, count, _fftSize, _osamp, WaveFormat.SampleRate, mono);

                index = 0;
                for (int sample = offset; sample <= count + offset - 1; sample++)
                {
                    buffer[sample] = Limiter(mono[index]);
                    index += 1;
                }
            }
            else if (WaveFormat.Channels == 2)
            {
                var left = new float[(count >> 1)];
                var right = new float[(count >> 1)];

                int index = 0;
                for (int sample = offset; sample <= count + offset - 1; sample += 2)
                {
                    left[index] = buffer[sample];
                    right[index] = buffer[sample + 1];
                    index += 1;
                }

                _shifterLeft.PitchShift(_pitchFactorLog, count >> 1, _fftSize, _osamp, WaveFormat.SampleRate, left);
                _shifterRight.PitchShift(_pitchFactorLog, count >> 1, _fftSize, _osamp, WaveFormat.SampleRate, right);

                index = 0;

                for (int sample = offset; sample <= count + offset - 1; sample += 2)
                {
                    buffer[sample] = Limiter(left[index]);
                    buffer[sample + 1] = Limiter(right[index]);
                    index += 1;
                }
            }
            else
            {
                throw new Exception("Shifting of more than 2 channels is currently not supported.");
            }
        }

        float Limiter(float sample)
        {
            float res;
            if ((_kLimThresh < sample))
            {
                res = (sample - _kLimThresh) / _kLimRange;
                res = (float)((Math.Atan(res) / _kMPi2) * _kLimRange + _kLimThresh);
            }
            else if ((sample < -_kLimThresh))
            {
                res = -(sample + _kLimThresh) / _kLimRange;
                res = -(float)((Math.Atan(res) / _kMPi2) * _kLimRange + _kLimThresh);
            }
            else
            {
                res = sample;
            }

            return res;
        }
    }
}
