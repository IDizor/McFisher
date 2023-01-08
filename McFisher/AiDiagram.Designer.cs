namespace McFisher;

partial class AiDiagram
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.AiIdTextBox = new System.Windows.Forms.TextBox();
            this.RedrawButton = new System.Windows.Forms.Button();
            this.DrawPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.InfoLabel);
            this.panel1.Controls.Add(this.AiIdTextBox);
            this.panel1.Controls.Add(this.RedrawButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1111, 25);
            this.panel1.TabIndex = 2;
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(352, 5);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(28, 15);
            this.InfoLabel.TabIndex = 4;
            this.InfoLabel.Text = "Info";
            // 
            // AiIdTextBox
            // 
            this.AiIdTextBox.Location = new System.Drawing.Point(81, 1);
            this.AiIdTextBox.Name = "AiIdTextBox";
            this.AiIdTextBox.Size = new System.Drawing.Size(265, 23);
            this.AiIdTextBox.TabIndex = 3;
            this.AiIdTextBox.Text = "id";
            // 
            // RedrawButton
            // 
            this.RedrawButton.Location = new System.Drawing.Point(0, 0);
            this.RedrawButton.Name = "RedrawButton";
            this.RedrawButton.Size = new System.Drawing.Size(75, 25);
            this.RedrawButton.TabIndex = 2;
            this.RedrawButton.Text = "Redraw";
            this.RedrawButton.UseVisualStyleBackColor = true;
            this.RedrawButton.Click += new System.EventHandler(this.RedrawButton_Click);
            // 
            // DrawPanel
            // 
            this.DrawPanel.BackColor = System.Drawing.SystemColors.Control;
            this.DrawPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DrawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawPanel.Location = new System.Drawing.Point(0, 25);
            this.DrawPanel.Name = "DrawPanel";
            this.DrawPanel.Size = new System.Drawing.Size(1111, 588);
            this.DrawPanel.TabIndex = 3;
            // 
            // AiDiagram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 613);
            this.Controls.Add(this.DrawPanel);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(600, 0);
            this.Name = "AiDiagram";
            this.Text = "AiDiagram";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AiDiagram_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private Panel panel1;
    private Button RedrawButton;
    private Panel DrawPanel;
    private TextBox AiIdTextBox;
    private Label InfoLabel;
}