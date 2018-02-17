using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Futor
{
    public partial class StackForm : Form
    {
        readonly List<PluginLine> _pluginLines = new List<PluginLine>(); 

        public Action<PluginLine> OnSelectPluginButtonClick;
        public Action<PluginLine> OnMoveUpPluginButtonClick;
        public Action<PluginLine> OnMoveDownPluginButtonClick;
        public Action<PluginLine> OnBypassPluginButtonClick;
        public Action<PluginLine> OnUIPluginButtonClick;
        public Action<PluginLine> OnRemovePluginButtonClick;

        public StackForm(PluginsStack pluginsStack)
        {
            InitializeComponent();

            LoadSlots(pluginsStack);
        }

        void LoadSlots(PluginsStack stack)
        {
            foreach (var pluginSlot in stack.PluginSlots)
                AddPluginLine(pluginSlot);
        }
        
        PluginLine AddPluginLine(PluginsStack.Plugin plugin)
        {
            var pluginLine = new PluginLine(plugin);

            pluginLine.OnSelectButtonClick += () =>
            {
                OnSelectPluginButtonClick?.Invoke(pluginLine);
            };
            pluginLine.OnMoveUpButtonClick += () =>
            {
                OnMoveUpPluginButtonClick.Invoke(pluginLine);

                // TODO
                MoveUpPlugin(pluginLine);

            };
            pluginLine.OnMoveDownButtonClick += () =>
            {
                // TODO
                MoveDownPlugin(pluginLine);
            };
            pluginLine.OnBypassButtonClick += () =>
            {
                // TODO
                BypassPlugin(pluginLine);
            };
            pluginLine.OnUIButtonClick += () =>
            {
                // TODO
            };
            pluginLine.OnRemoveButtonClick += () =>
            {
                // TODO
                RemovePluginLine(pluginLine);
            };

            _pluginLines.Add(pluginLine);

            PluginsLayoutPanel.Controls.Add(pluginLine);

            PluginLinesChanged();

            return pluginLine;
        }

        void RemovePluginLine(PluginLine pluginLine)
        {
            _pluginLines.Remove(pluginLine);

            PluginsLayoutPanel.Controls.Remove(pluginLine);

            //_pluginsStack.ClosePlugin(pluginLine.Slot);

            PluginLinesChanged();
        }

        void MoveUpPlugin(PluginLine pluginLine)
        {
            MovePlugin(pluginLine, _pluginLines.IndexOf(pluginLine) - 1);
        }

        void MoveDownPlugin(PluginLine pluginLine)
        {
            MovePlugin(pluginLine, _pluginLines.IndexOf(pluginLine) + 1);
        }

        void MovePlugin(PluginLine pluginLine, int newIndex)
        {
            _pluginLines.Remove(pluginLine);
            _pluginLines.Insert(newIndex, pluginLine);

            PluginsLayoutPanel.Controls.SetChildIndex(pluginLine, newIndex);

            //_pluginsStack.SetPluginIndex(pluginLine.Slot, newIndex);

            PluginLinesChanged();
        }

        void BypassPlugin(PluginLine pluginLine)
        {
            var bypass = pluginLine.Slot.IsBypass;
            pluginLine.Slot.IsBypass = !bypass;

            pluginLine.UpdateSlot();
        }
        
        void PluginLinesChanged()
        {
            if (!_pluginLines.Any())
                return;

            if (_pluginLines.Count == 1)
            {
                _pluginLines.First().PositionType = PluginLine.EPosition.One;
            }
            else
            {
                _pluginLines.First().PositionType = PluginLine.EPosition.First;
                _pluginLines.Last().PositionType = PluginLine.EPosition.Last;
                
                _pluginLines
                    .Skip(1)
                    .Take(_pluginLines.Count - 2)
                    .ToList()
                    .ForEach(x => x.PositionType = PluginLine.EPosition.Middle);
            }
        }
        
        void TryAddPluginLine()
        {
            var openFileDialog = new OpenFileDialog();

            if (Directory.Exists(Preferences<PreferencesDescriptor>.Instance.LastPluginPath))
                openFileDialog.InitialDirectory = Preferences<PreferencesDescriptor>.Instance.LastPluginPath;

            openFileDialog.Filter = "VST Plugins|*.dll";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var pluginPath = openFileDialog.FileName;

                Preferences<PreferencesDescriptor>.Instance.LastPluginPath = Path.GetDirectoryName(pluginPath);

                //var pluginSlot = _pluginsStack.OpenPlugin(pluginPath);
                //var pluginLine = AddPluginLine(pluginSlot);
            }
        }

        void StackForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Preferences<PreferencesDescriptor>.Instance.PluginInfos = _pluginsStack.SaveStack();
            //
            //Preferences<PreferencesDescriptor>.Manager.Save();
        }

        void AddButton_Click(object sender, EventArgs e)
        {
            TryAddPluginLine();
        }
    }
}
