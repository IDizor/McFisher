namespace McFisher;

public partial class PlotForm : Form
{
    public PlotForm()
    {
        InitializeComponent();
    }

    private void PlotForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (Program.MainForm.ShowLiveCheckBox.Checked)
        {
            Program.MainForm.ShowLiveCheckBox.Checked = false;
        }
        
        e.Cancel = true;
    }
}
