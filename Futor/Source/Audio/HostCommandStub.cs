using System;
using System.Windows.Forms;
using Jacobi.Vst.Core.Host;

namespace Futor
{
    public class HostCommandStub : IVstHostCommandStub
    {
        public class PluginCalledEventArgs : EventArgs
        {
            public PluginCalledEventArgs(string message)
            {
                Message = message;
            }

            public string Message { get; private set; }
        }

        public event EventHandler<PluginCalledEventArgs> PluginCalled;

        void RaisePluginCalled(string message)
        {
            PluginCalled?.Invoke(this, new PluginCalledEventArgs(message));
        }

        public IVstPluginContext PluginContext { get; set; }

        public bool BeginEdit(int index)
        {
            RaisePluginCalled("BeginEdit(" + index + ")");

            return false;
        }

        public Jacobi.Vst.Core.VstCanDoResult CanDo(string cando)
        {
            RaisePluginCalled("CanDo(" + cando + ")");

            return Jacobi.Vst.Core.VstCanDoResult.Unknown;
        }

        public bool CloseFileSelector(Jacobi.Vst.Core.VstFileSelect fileSelect)
        {
            RaisePluginCalled("CloseFileSelector(" + fileSelect.Command + ")");

            return false;
        }

        public bool EndEdit(int index)
        {
            RaisePluginCalled("EndEdit(" + index + ")");

            return false;
        }

        public Jacobi.Vst.Core.VstAutomationStates GetAutomationState()
        {
            RaisePluginCalled("GetAutomationState()");

            return Jacobi.Vst.Core.VstAutomationStates.Off;
        }

        public int GetBlockSize()
        {
            RaisePluginCalled("GetBlockSize()");

            return 1024;
        }

        public string GetDirectory()
        {
            RaisePluginCalled("GetDirectory()");

            return null;
        }

        public int GetInputLatency()
        {
            RaisePluginCalled("GetInputLatency()");

            return 0;
        }

        public Jacobi.Vst.Core.VstHostLanguage GetLanguage()
        {
            RaisePluginCalled("GetLanguage()");

            return Jacobi.Vst.Core.VstHostLanguage.English;
        }

        public int GetOutputLatency()
        {
            RaisePluginCalled("GetOutputLatency()");

            return 0;
        }

        public Jacobi.Vst.Core.VstProcessLevels GetProcessLevel()
        {
            RaisePluginCalled("GetProcessLevel()");

            return Jacobi.Vst.Core.VstProcessLevels.Realtime;
        }

        public string GetProductString()
        {
            RaisePluginCalled("GetProductString()");

            return Application.ProductName;
        }

        public float GetSampleRate()
        {
            RaisePluginCalled("GetSampleRate()");

            return 44.1f;
        }

        public Jacobi.Vst.Core.VstTimeInfo GetTimeInfo(Jacobi.Vst.Core.VstTimeInfoFlags filterFlags)
        {
            RaisePluginCalled("GetTimeInfo(" + filterFlags + ")");

            return null;
        }

        public string GetVendorString()
        {
            RaisePluginCalled("GetVendorString()");

            return Application.CompanyName;
        }
        
        public int GetVendorVersion()
        {
            RaisePluginCalled("GetVendorVersion()");

            return 1000;
        }
        
        public bool IoChanged()
        {
            RaisePluginCalled("IoChanged()");

            return false;
        }
        
        public bool OpenFileSelector(Jacobi.Vst.Core.VstFileSelect fileSelect)
        {
            RaisePluginCalled("OpenFileSelector(" + fileSelect.Command + ")");

            return false;
        }
        
        public bool ProcessEvents(Jacobi.Vst.Core.VstEvent[] events)
        {
            RaisePluginCalled("ProcessEvents(" + events.Length + ")");

            return false;
        }

        public bool SizeWindow(int width, int height)
        {
            RaisePluginCalled("SizeWindow(" + width + ", " + height + ")");

            return false;
        }
        
        public bool UpdateDisplay()
        {
            RaisePluginCalled("UpdateDisplay()");

            return false;
        }
        
        public int GetCurrentPluginID()
        {
            RaisePluginCalled("GetCurrentPluginID()");

            return PluginContext.PluginInfo.PluginID;
        }
        
        public int GetVersion()
        {
            RaisePluginCalled("GetVersion()");

            return 1000;
        }
        
        public void ProcessIdle()
        {
            RaisePluginCalled("ProcessIdle()");
        }
        
        public void SetParameterAutomated(int index, float value)
        {
            RaisePluginCalled("SetParameterAutomated(" + index + ", " + value + ")");
        }
    }
}
