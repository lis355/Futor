using System;
using Jacobi.Vst.Interop.Host;

namespace Futor
{
    public class Plugin
    {
        VstPluginContext _pluginContext;

        public Plugin(string path)
        {
            _pluginContext = OpenPlugin(path);
        }

        VstPluginContext OpenPlugin(string pluginPath)
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
            }

            return null;
        }
    }
}
