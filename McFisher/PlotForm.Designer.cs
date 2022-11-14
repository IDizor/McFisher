namespace McFisher;

partial class PlotForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.FftPlot = new ScottPlot.FormsPlot();
            this.SuspendLayout();
            // 
            // FftPlot
            // 
            this.FftPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FftPlot.Location = new System.Drawing.Point(0, 20);
            this.FftPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FftPlot.Name = "FftPlot";
            this.FftPlot.Size = new System.Drawing.Size(506, 257);
            this.FftPlot.TabIndex = 8;
            // 
            // PlotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 279);
            this.Controls.Add(this.FftPlot);
            this.Name = "PlotForm";
            this.Text = "PlotForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlotForm_FormClosing);
            this.ResumeLayout(false);

    }

    #endregion

    public ScottPlot.FormsPlot FftPlot;
}