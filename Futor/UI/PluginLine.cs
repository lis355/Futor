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

        readonly Color _disableColor = SystemColors.ControlDark;
        readonly Color _bypassColor = Color.Yellow;
        readonly Color _defaultTextColor;
        readonly Color _defaultBackgroundColor;

        EPosition _positionType;
        PluginsStack.Plugin _plugin;

        public Action OnSelectButtonClick;
        public Action OnMoveUpButtonClick;
        public Action OnMoveDownButtonClick;
        public Action OnBypassButtonClick;
        public Action OnUIButtonClick;
        public Action OnRemoveButtonClick;

        public bool IsBypass
        {
            get { return _plugin.IsBypass; }
            set
            {
                if (_plugin.IsBypass == value)
                    return;

                // TODO
            }
        }

        public PluginLine(PluginsStack.Plugin plugin)
        {
            InitializeComponent();

            _defaultTextColor = RemoveButton.ForeColor;
            _defaultBackgroundColor = RemoveButton.BackColor;

            Slot = plugin;

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

        public PluginsStack.Plugin Slot
        {
            get { return _plugin; }
            set
            {
                if (_plugin == value)
                    return;

                _plugin = value;

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
            PluginNameLabel.Text = _plugin.Name;

            var enabled = !_plugin.IsEmpty;

            UIButton.Enabled = enabled;
            BypassButton.Enabled = enabled;
            MoveUpButton.Enabled = enabled;
            MoveDownButton.Enabled = enabled;
            RemoveButton.Enabled = enabled;

            BypassButton.BackColor = (_plugin.IsBypass) ? _bypassColor : _defaultBackgroundColor;
        }

        void SelectPlugin()
        {
            OnSelectButtonClick?.Invoke();
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
            OnMoveUpButtonClick?.Invoke();
        }

        void MoveDownButton_Click(object sender, EventArgs e)
        {
            OnMoveDownButtonClick?.Invoke();
        }

        void BypassButton_Click(object sender, EventArgs e)
        {
            OnBypassButtonClick?.Invoke();
        }

        void UIButton_Click(object sender, EventArgs e)
        {
            OnUIButtonClick?.Invoke();
        }

        void RemoveButton_Click(object sender, EventArgs e)
        {
            OnRemoveButtonClick?.Invoke();
        }
    }
}
