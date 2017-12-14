using System;
using System.Windows.Forms;
using Jacobi.Vst.Interop.Host;

namespace Futor
{
    public class Plugin
    {
        readonly VstPluginContext _pluginContext;

        public Plugin(string path)
        {
            _pluginContext = OpenPlugin(path);

            if (_pluginContext != null)
            {

                var dlg = new EditorForm {PluginCommandStub = _pluginContext.PluginCommandStub};

                _pluginContext.PluginCommandStub.MainsChanged(true);
                dlg.ShowDialog();
            }
        }

        static VstPluginContext OpenPlugin(string pluginPath)
        {
            try
            {
                var hostCmdStub = new HostCommandStub();
                //hostCmdStub.PluginCalled += HostCmdStub_PluginCalled;

                var ctx = VstPluginContext.Create(pluginPath, hostCmdStub);
                
                ctx.Set("PluginPath", pluginPath);
                ctx.Set("HostCmdStub", hostCmdStub);
                
                ctx.PluginCommandStub.Open();

                return ctx;
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Can't open plugin at path {0}: {1}", pluginPath, e.Message));
            }

            return null;
        }

        void ClosePlugin()
        {
            _pluginContext.PluginCommandStub.MainsChanged(false);

            _pluginContext?.Dispose();
        }
    }
}
