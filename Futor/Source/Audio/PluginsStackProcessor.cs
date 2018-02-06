﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Jacobi.Vst.Core;
using Jacobi.Vst.Interop.Host;

namespace Futor
{
    public class PluginsStackProcessor : Processor
    {
        public class PluginSlot
        {
             
        }

        readonly List<VstPluginContext> _plugins = new List<VstPluginContext>();
        int _channelsCount;
        int _size;
        VstAudioBufferManager _bufferManager;
        VstAudioBuffer[] _inputBuffers;
        VstAudioBuffer[] _outputBuffers;

        public class PluginEventArgs : EventArgs 
        {
            public VstPluginContext PluginContext { get; private set; }

            public PluginEventArgs(VstPluginContext pluginContext)
            {
                PluginContext = pluginContext;
            }
        }

        public EventHandler<PluginEventArgs> OnPluginAdded;
        public EventHandler<PluginEventArgs> OnPluginRemoved;
        
        public void LoadStack()
        {
            var pluginInfos = Preferences<PreferencesDescriptor>.Instance.PluginInfos;

            for (int i = 0; i < pluginInfos.Count; i++)
            {
                var pluginInfo = pluginInfos[i];
                var error = true;
                var pluginPath = pluginInfo.Path;
                if (File.Exists(pluginPath))
                {
                    var pluginContext = OpenPlugin(pluginPath);
                    if (pluginContext != null)
                    {
                        error = false;

                        _plugins.Add(pluginContext);

                        OnPluginAdded?.Invoke(this, new PluginEventArgs(pluginContext));
                    }
                }

                if (error)
                {
                    pluginInfos.RemoveAt(i);
                    i--;
                }
            }
        }

        public override void Process(float[] buffer, int offset, int samples)
        {
            if (!_plugins.Any())
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

            for (int i = 0; i < _plugins.Count; i++)
            {
                if (i > 0)
                {
                    var tmpBuffer = _outputBuffers;
                    _outputBuffers = _inputBuffers;
                    _inputBuffers = tmpBuffer;
                }

                var pluginContext = _plugins[i];

                pluginContext.PluginCommandStub.SetBlockSize(size);
                pluginContext.PluginCommandStub.ProcessReplacing(_inputBuffers, _outputBuffers);
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

        // DEBUG
        public 
        static VstPluginContext OpenPlugin(string pluginPath)
        {
            try
            {
                var hostCmdStub = new HostCommandStub();

                var pluginContext = VstPluginContext.Create(pluginPath, hostCmdStub);

                pluginContext.Set("PluginPath", pluginPath);
                pluginContext.Set("HostCmdStub", hostCmdStub);

                pluginContext.PluginCommandStub.Open();

                pluginContext.PluginCommandStub.MainsChanged(true);

                return pluginContext;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Can't open plugin at path {pluginPath}: {e.Message}");
            }

            return null;
        }

        static void ClosePlugin(VstPluginContext pluginContext)
        {
            pluginContext.PluginCommandStub.MainsChanged(false);

            pluginContext.Dispose();
        }
    }
}
