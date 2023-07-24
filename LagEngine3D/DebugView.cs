using Rendering;

namespace LagEngine3D
{
    public partial class DebugView : Form
    {
        public DebugView()
        {
            InitializeComponent();
        }

        private void DebugView_Load(object sender, EventArgs e)
        {
            DebugText.Text = "";
            RenderDebug.OnTextChanged += OnTextChanged;
        }

        private void OnTextChanged(string text)
        {
            DebugText.Text = text;
        }
    }
}
