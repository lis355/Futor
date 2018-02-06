using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Jacobi.Vst.Core;
using Jacobi.Vst.Interop.Host;

namespace Futor
{
    public class PluginsStack : SampleProcessor
    {
        public class PluginSlot
        {
            const string _kEmptyPluginName = "...";

            public VstPluginContext Plugin;

            public string Name
            {
                get { return (!IsEmpty) ? Plugin.PluginCommandStub.GetEffectName() : _kEmptyPluginName; }
            }

            public bool IsEmpty
            {
                get { return Plugin == null; }
            }

            // TODO event
            public bool IsBypass { get; set; }
        }

        const string _kPluginParameterPluginPath = "PluginPath";
        const string _kPluginParameterHostCmdStub = "HostCmdStub";

        readonly List<PluginSlot> _pluginSlots = new List<PluginSlot>();
        int _channelsCount;
        int _size;
        VstAudioBufferManager _bufferManager;
        VstAudioBuffer[] _inputBuffers;
        VstAudioBuffer[] _outputBuffers;

        public IEnumerable<PluginSlot> PluginSlots { get { return _pluginSlots; } }

        public class PluginEventArgs : EventArgs 
        {
            public PluginSlot PluginSlot { get; private set; }

            public PluginEventArgs(PluginSlot pluginSlot)
            {
                PluginSlot = pluginSlot;
            }
        }

        // TODO
        public EventHandler<PluginEventArgs> OnPluginSlotAdded;
        public EventHandler<PluginEventArgs> OnPluginSlotRemoved;
        public EventHandler<PluginEventArgs> OnPluginSlotChanged;

        public void LoadStack(List<PreferencesDescriptor.PluginInfo> pluginInfos)
        {
            for (int i = 0; i < pluginInfos.Count; i++)
            {
                var pluginInfo = pluginInfos[i];
                
                try
                {
                    var pluginSlot = OpenPlugin(pluginInfo.Path);

                    pluginSlot.IsBypass = pluginInfo.IsBypass;
                }
                catch
                {
                }
            }
        }
        
        public List<PreferencesDescriptor.PluginInfo> SaveStack()
        {
            var pluginInfos = new List<PreferencesDescriptor.PluginInfo>(_pluginSlots.Count);

            foreach (var pluginSlot in _pluginSlots)
            {
                var pluginInfo = new PreferencesDescriptor.PluginInfo();

                if (!pluginSlot.IsEmpty)
                {
                    var pluginContext = pluginSlot.Plugin;

                    pluginInfo.Path = pluginContext.Find<string>(_kPluginParameterPluginPath);
                    pluginInfo.IsBypass = pluginSlot.IsBypass;
                }

                pluginInfos.Add(pluginInfo);
            }

            return pluginInfos;
        }

        public PluginSlot OpenPlugin(string pluginPath)
        {
            if (!File.Exists(pluginPath))
                throw new FileNotFoundException(pluginPath);

            var pluginContext = OpenPluginContext(pluginPath);
            var pluginSlot = new PluginSlot
            {
                Plugin = pluginContext
            };
            
            _pluginSlots.Add(pluginSlot);
            
            return pluginSlot;
        }

        public override void Process(float[] buffer, int offset, int samples)
        {
            if (!_pluginSlots.Any())
                return;

            int channelsCount = WaveFormat.Channels;
            int size = samples / channelsCount;

            if (_channelsCount != channelsCount
                || _size != size)
                ResetBufferManager(channelsCount, size);

            _bufferManager.ClearAllBuffers();

            for (int i = offset, k = 0; i < offset + samples; i += channelsCount, k++)
                for (int j = 0; j < channelsCount; j++)
                    _inputBuffers[j][k] = buffer[i + j];

            // TODO cache
            var plugins = _pluginSlots
                .Where(x => x.Plugin != null 
                    && !x.IsBypass)
                .Select(x => x.Plugin)
                .ToList();

            for (int i = 0; i < plugins.Count; i++)
            {
                if (i > 0)
                {
                    var tmpBuffer = _outputBuffers;
                    _outputBuffers = _inputBuffers;
                    _inputBuffers = tmpBuffer;
                }

                var pluginContext = plugins[i];
                var pluginCommandStub = pluginContext.PluginCommandStub;

                pluginCommandStub.SetBlockSize(size);
                pluginCommandStub.ProcessReplacing(_inputBuffers, _outputBuffers);
            }

            for (int i = offset, k = 0; i < offset + samples; i += channelsCount, k++)
                for (int j = 0; j < channelsCount; j++)
                    buffer[i + j] = _outputBuffers[j][k];
        }

        void ResetBufferManager(int channelsCount, int size)
        {
            _bufferManager?.Dispose();

            _channelsCount = channelsCount;
            _size = size;

            _bufferManager = new VstAudioBufferManager(channelsCount * 2, size);

            var buffers = _bufferManager.ToList();
            _inputBuffers = buffers.Skip(0).Take(_channelsCount).ToArray();
            _outputBuffers = buffers.Skip(_channelsCount).Take(_channelsCount).ToArray();
        }
        
        static VstPluginContext OpenPluginContext(string pluginPath)
        {
            try
            {
                var hostCmdStub = new HostCommandStub();

                var pluginContext = VstPluginContext.Create(pluginPath, hostCmdStub);

                pluginContext.Set(_kPluginParameterPluginPath, pluginPath);
                pluginContext.Set(_kPluginParameterHostCmdStub, hostCmdStub);

                var pluginCommandStub = pluginContext.PluginCommandStub;

                pluginCommandStub.Open();

                pluginCommandStub.MainsChanged(true);

                return pluginContext;
            }
            catch (Exception e)
            {
                throw new Exception($"Can't open plugin at path {pluginPath}: {e.Message}");
            }
        }

        static void ClosePluginContext(VstPluginContext pluginContext)
        {
            var pluginCommandStub = pluginContext.PluginCommandStub;

            pluginCommandStub.MainsChanged(false);

            pluginCommandStub.Close();

            pluginContext.Dispose();
        }
    }
}
