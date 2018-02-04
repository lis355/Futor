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

        public StackForm()
        {
            InitializeComponent();

            LoadSlots();
        }

        void LoadSlots()
        {
            // TODO
        }

        PluginLine AddPluginLine()
        {
            var pluginLine = new PluginLine();

            // TODO
            //pluginLine.OnSelectButtonClick;
            pluginLine.OnMoveUpButtonClick += (sender, args) => MoveUpPlugin(pluginLine);
            pluginLine.OnMoveDownButtonClick += (sender, args) => MoveDownPlugin(pluginLine);
            //pluginLine.OnBypassButtonClick;
            //pluginLine.OnUIButtonClick;
            pluginLine.OnRemoveButtonClick += (sender, args) => RemovePluginLine(pluginLine);

            _pluginLines.Add(pluginLine);

            PluginsLayoutPanel.Controls.Add(pluginLine);

            PluginLinesChanged();

            return pluginLine;
        }

        void RemovePluginLine(PluginLine pluginLine)
        {
            _pluginLines.Remove(pluginLine);

            PluginsLayoutPanel.Controls.Remove(pluginLine);

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

            PluginLinesChanged();
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
                var fileName = openFileDialog.FileName;

                Preferences<PreferencesDescriptor>.Instance.LastPluginPath = Path.GetDirectoryName(fileName);

                var pluginLine = AddPluginLine();
                pluginLine.PluginName = Path.GetFileNameWithoutExtension(fileName);
            }
        }

        private void StackForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Preferences<PreferencesDescriptor>.Manager.Save();
        }

        private void AddButton_Click(Object sender, EventArgs e)
        {
            TryAddPluginLine();
        }
    }
}
