﻿namespace Futor
{
    public class PluginsStackProcessor : Processor
    {
        public PluginsStackProcessor()
        {
            const string path = @"C:\Program Files\VstPluginsLib\Pitch\PitchShifter.dll";
            var plugin = new Plugin(path);
        }

        public override void Process(float[] buffer, int offset, int samples)
        {
            // for (var i = offset; i < offset + samples; i++)
            
            int channelsCount = WaveFormat.Channels;
            int size = samples / channelsCount;
//            using (var outputMgr = new VstAudioBufferManager(channelsCount, size))
//            {
//#pragma warning disable 618
//                var outputBuffers = outputMgr.ToArray();
//#pragma warning restore 618
//
//                //_plugin.PluginCommandStub.SetBlockSize(size);
//                //_plugin.PluginCommandStub.ProcessReplacing(null, outputBuffers);
//                //
//                //int outIndex = 0;
//                //for (var i = 0; i < count / channelsCount; ++i)
//                //    foreach (var outputBuffer in outputBuffers)
//                //        buffer[outIndex++] = outputBuffer[i];
//                //
//                //_plugin.PluginCommandStub.EditorIdle();
//            }
        }
    }
}
