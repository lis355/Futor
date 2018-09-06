﻿using System;
using System.Windows.Forms;

namespace Futor
{
    public class Application : IDisposable
    {
        public PreferenceController Options { get; }

        public AudioManager AudioManager { get; private set; }

        public PitchShifter PitchShifter { get; private set; }
        
        public Application()
        {
            Options = new PreferenceController();
            Options.OnLoaded += () =>
            {
                OptionsLoaded();
            };

            Options.Load();

            Options.IsAutorun.OnChanged += (sender, args) =>
            {
                SetAutorun();
            };

            ProcessAudioManager();

            HotKeyManager.Instance.RegisterHotKey(new HotKeyManager.HotKey { Key = Keys.Subtract, Modifiers = HotKeyManager.Modifiers.Ctrl | HotKeyManager.Modifiers.Alt }, () =>
            {
                Options.PitchFactor.Value -= 1;
            });

            HotKeyManager.Instance.RegisterHotKey(new HotKeyManager.HotKey { Key = Keys.Add, Modifiers = HotKeyManager.Modifiers.Ctrl | HotKeyManager.Modifiers.Alt }, () =>
            {
                Options.PitchFactor.Value += 1;
            });
        }

        void OptionsLoaded()
        {
            SetAutorun();
        }

        void SetAutorun()
        {
            var hasAutorun = Options.IsAutorun.Value;
            if (hasAutorun != AutorunProvider.HasSturtup())
            {
                if (hasAutorun)
                    AutorunProvider.AddToStartup();
                else
                    AutorunProvider.RemoveFromStartup();
            }
        }

        void ProcessAudioManager()
        {
            AudioManager = new AudioManager();
            
            AudioManager.OnInputDeviceChanged += (sender, args) =>
            {
                Options.InputDeviceName.Value = args.AudioManager.InputDeviceName;
            };
            
            AudioManager.OnOutputDeviceChanged += (sender, args) =>
            {
                Options.OutputDeviceName.Value = args.AudioManager.OutputDeviceName;
            };
            
            AudioManager.OnLatencyMillisecondsChanged += (sender, args) =>
            {
                Options.LatencyMilliseconds.Value = args.AudioManager.LatencyMilliseconds;
            };

            SetInputDeviceName();
            SetOutputDeviceName();
            SetLatencyMilliseconds();

            PitchShifter = new PitchShifter();
            SetPitchFactor();

            AudioManager.SampleProcessor = PitchShifter;

            Options.PitchFactor.OnChanged += (sender, args) => 
            {
                SetPitchFactor();
            };

            Options.InputDeviceName.OnChanged += (sender, args) =>
            {
                SetInputDeviceName();
            };

            Options.OutputDeviceName.OnChanged += (sender, args) =>
            {
                SetOutputDeviceName();
            };

            Options.LatencyMilliseconds.OnChanged += (sender, args) =>
            {
                SetLatencyMilliseconds();
            };

            Options.IsBypassAll.OnChanged += (sender, args) =>
            {
                if (args.NewValue
                    && AudioManager.IsWorking.Value)
                {
                    AudioManager.Finish();
                }
                else if (!args.NewValue
                     && !AudioManager.IsWorking.Value)
                {
                    AudioManager.Start();
                }
            };

            AudioManager.Start();
        }

        void SetInputDeviceName()
        {
            if (!Options.IsBypassAll.Value)
                AudioManager.InputDeviceName = Options.InputDeviceName.Value;
        }

        void SetOutputDeviceName()
        {
            if (!Options.IsBypassAll.Value)
                AudioManager.OutputDeviceName = Options.OutputDeviceName.Value;
        }

        void SetLatencyMilliseconds()
        {
            if (!Options.IsBypassAll.Value)
                AudioManager.LatencyMilliseconds = Options.LatencyMilliseconds.Value;
        }

        void SetPitchFactor()
        {
            PitchShifter.PitchFactor.Value = Options.PitchFactor.Value;
        }

        public void Dispose()
        {
            Exit();
        }

        public void Exit()
        {
            HotKeyManager.Instance.Dispose();

            if (AudioManager.IsWorking.Value)
                AudioManager.Finish();

            Options.Save();
        }
    }
}
