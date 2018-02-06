using System;
using System.Drawing;
using System.Windows.Forms;

namespace Futor
{
    public partial class PluginLine : UserControl
    {
        public enum EPosition
        {
            One,
            First,
            Middle,
            Last
        }

        EPosition _positionType;

        readonly Color _disableColor = SystemColors.ControlDark;
        readonly Color _bypassColor = Color.Yellow;
        readonly Color _defaultTextColor;
        readonly Color _defaultBackgroundColor;
        PluginsStack.PluginSlot _pluginSlot;

        public EventHandler OnSelectButtonClick;
        public EventHandler OnMoveUpButtonClick;
        public EventHandler OnMoveDownButtonClick;
        public EventHandler OnBypassButtonClick;
        public EventHandler OnUIButtonClick;
        public EventHandler OnRemoveButtonClick;

        public PluginLine(PluginsStack.PluginSlot pluginSlot)
        {
            InitializeComponent();

            _defaultTextColor = RemoveButton.ForeColor;
            _defaultBackgroundColor = RemoveButton.BackColor;

            Slot = pluginSlot;

            UpdatePositionType();
        }

        public EPosition PositionType
        {
            get { return _positionType; }
            set
            {
                if (_positionType == value)
                    return;

                _positionType = value;

                UpdatePositionType();
            }
        }

        public PluginsStack.PluginSlot Slot
        {
            get { return _pluginSlot; }
            set
            {
                if (_pluginSlot == value)
                    return;

                _pluginSlot = value;

                UpdateSlot();
            }
        }
        
        void UpdatePositionType()
        {
            switch (_positionType)
            {
                case EPosition.One:
                    MoveDownButton.ForeColor = _disableColor;
                    MoveDownButton.Enabled = false;

                    MoveUpButton.ForeColor = _disableColor;
                    MoveUpButton.Enabled = false;

                    break;

                case EPosition.First:
                    MoveDownButton.ForeColor = _defaultTextColor;
                    MoveDownButton.Enabled = true;

                    MoveUpButton.ForeColor = _disableColor;
                    MoveUpButton.Enabled = false;

                    break;

                case EPosition.Middle:
                    MoveDownButton.ForeColor = _defaultTextColor;
                    MoveDownButton.Enabled = true;

                    MoveUpButton.ForeColor = _defaultTextColor;
                    MoveUpButton.Enabled = true;

                    break;

                case EPosition.Last:
                    MoveDownButton.ForeColor = _disableColor;
                    MoveDownButton.Enabled = false;

                    MoveUpButton.ForeColor = _defaultTextColor;
                    MoveUpButton.Enabled = true;

                    break;
            }
        }

        // TODO make private
        public void UpdateSlot()
        {
            PluginNameLabel.Text = _pluginSlot.Name;

            var enabled = !_pluginSlot.IsEmpty;

            UIButton.Enabled = enabled;
            BypassButton.Enabled = enabled;
            MoveUpButton.Enabled = enabled;
            MoveDownButton.Enabled = enabled;
            RemoveButton.Enabled = enabled;

            BypassButton.BackColor = (_pluginSlot.IsBypass) ? _bypassColor : _defaultBackgroundColor;
        }

        void SelectPlugin()
        {
            OnSelectButtonClick?.Invoke(this, null);
        }

        void LabelPanel_Click(object sender, EventArgs e)
        {
            SelectPlugin();
        }

        void PluginNameLabel_Click(object sender, EventArgs e)
        {
            SelectPlugin();
        }

        void MoveUpButton_Click(object sender, EventArgs e)
        {
            OnMoveUpButtonClick?.Invoke(sender, e);
        }

        void MoveDownButton_Click(object sender, EventArgs e)
        {
            OnMoveDownButtonClick?.Invoke(sender, e);
        }

        void BypassButton_Click(object sender, EventArgs e)
        {
            OnBypassButtonClick?.Invoke(sender, e);
        }

        void UIButton_Click(object sender, EventArgs e)
        {
            OnUIButtonClick?.Invoke(sender, e);
        }

        void RemoveButton_Click(object sender, EventArgs e)
        {
            OnRemoveButtonClick?.Invoke(sender, e);
        }
    }
}
