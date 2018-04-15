using System.Windows.Forms;

namespace WallE
{
    public partial class OptionForm : Form
    {
        public int Value { get; protected set; }

        public OptionForm(int value)
        {
            InitializeComponent();
            trackBar.Value = value;
        }

        private void OptionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Value = trackBar.Value;
        }
    }
}
