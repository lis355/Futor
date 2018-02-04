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
        readonly Color _defaultTextColor;

        public EventHandler OnSelectButtonClick;
        public EventHandler OnMoveUpButtonClick;
        public EventHandler OnMoveDownButtonClick;
        public EventHandler OnBypassButtonClick;
        public EventHandler OnUIButtonClick;
        public EventHandler OnRemoveButtonClick;

        public PluginLine()
        {
            InitializeComponent();

            _defaultTextColor = RemoveButton.ForeColor;

            UpdatePositionType();

            PluginName = "Empty";
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

        public string PluginName
        {
            get { return PluginNameLabel.Text; }
            set { PluginNameLabel.Text = value; }
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

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            OnRemoveButtonClick?.Invoke(sender, e);
        }
    }
}
