namespace McFisher;

partial class AppForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.ShowLiveTimer = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ShowHitLabelTimer = new System.Windows.Forms.Timer(this.components);
            this.RecordCheckKeyTimer = new System.Windows.Forms.Timer(this.components);
            this.RodTimer = new System.Windows.Forms.Timer(this.components);
            this.SettingsPanel = new System.Windows.Forms.Panel();
            this.InfoBox = new System.Windows.Forms.TextBox();
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.GenPopLimitUpDown = new System.Windows.Forms.NumericUpDown();
            this.NeuronsMemoryUpDown = new System.Windows.Forms.NumericUpDown();
            this.BlocksToUseUpDown = new System.Windows.Forms.NumericUpDown();
            this.GenErrorsThresholdUpDown = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.ThreadsUpDown = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.LookIntoWaterCheckBox = new System.Windows.Forms.CheckBox();
            this.FishingTimeoutUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.GameTitleTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SignalMultiplierUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.MomentumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.ErrorsGoalUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.BrainsFileLabel = new System.Windows.Forms.Label();
            this.GoCheckBox = new System.Windows.Forms.CheckBox();
            this.BrainsComboBox = new System.Windows.Forms.ComboBox();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.FinalizeCheckBox = new System.Windows.Forms.CheckBox();
            this.HitLabel = new System.Windows.Forms.Label();
            this.GenerationLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.FishingCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowLiveCheckBox = new System.Windows.Forms.CheckBox();
            this.MutateCheckBox = new System.Windows.Forms.CheckBox();
            this.GenerateCheckBox = new System.Windows.Forms.CheckBox();
            this.RecordCheckBox = new System.Windows.Forms.CheckBox();
            this.ErrorsCountLabel = new System.Windows.Forms.Label();
            this.SurvivalsLabel = new System.Windows.Forms.Label();
            this.PopulationLabel = new System.Windows.Forms.Label();
            this.TopMostCheckBox = new System.Windows.Forms.CheckBox();
            this.FishingTimeoutTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TrainingFileStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.BrainsFileStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.WaitPlayerLooksIntoWaterTimer = new System.Windows.Forms.Timer(this.components);
            this.EvolutionTimer = new System.Windows.Forms.Timer(this.components);
            this.SettingsPanel.SuspendLayout();
            this.OptionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GenPopLimitUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NeuronsMemoryUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlocksToUseUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GenErrorsThresholdUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FishingTimeoutUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SignalMultiplierUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MomentumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorsGoalUpDown)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ShowLiveTimer
            // 
            this.ShowLiveTimer.Interval = 20;
            this.ShowLiveTimer.Tick += new System.EventHandler(this.ShowLiveTimer_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ShowHitLabelTimer
            // 
            this.ShowHitLabelTimer.Tick += new System.EventHandler(this.ListenLiveTimer_Tick);
            // 
            // RecordCheckKeyTimer
            // 
            this.RecordCheckKeyTimer.Interval = 10;
            this.RecordCheckKeyTimer.Tick += new System.EventHandler(this.RecordCheckKeyTimer_Tick);
            // 
            // RodTimer
            // 
            this.RodTimer.Interval = 600;
            this.RodTimer.Tick += new System.EventHandler(this.RodTimer_Tick);
            // 
            // SettingsPanel
            // 
            this.SettingsPanel.Controls.Add(this.InfoBox);
            this.SettingsPanel.Controls.Add(this.OptionsPanel);
            this.SettingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsPanel.Location = new System.Drawing.Point(0, 75);
            this.SettingsPanel.Name = "SettingsPanel";
            this.SettingsPanel.Padding = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.SettingsPanel.Size = new System.Drawing.Size(338, 278);
            this.SettingsPanel.TabIndex = 21;
            // 
            // InfoBox
            // 
            this.InfoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoBox.Location = new System.Drawing.Point(8, 193);
            this.InfoBox.Multiline = true;
            this.InfoBox.Name = "InfoBox";
            this.InfoBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InfoBox.Size = new System.Drawing.Size(322, 77);
            this.InfoBox.TabIndex = 11;
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.Controls.Add(this.GenPopLimitUpDown);
            this.OptionsPanel.Controls.Add(this.NeuronsMemoryUpDown);
            this.OptionsPanel.Controls.Add(this.BlocksToUseUpDown);
            this.OptionsPanel.Controls.Add(this.GenErrorsThresholdUpDown);
            this.OptionsPanel.Controls.Add(this.label14);
            this.OptionsPanel.Controls.Add(this.ThreadsUpDown);
            this.OptionsPanel.Controls.Add(this.label13);
            this.OptionsPanel.Controls.Add(this.label12);
            this.OptionsPanel.Controls.Add(this.label10);
            this.OptionsPanel.Controls.Add(this.LookIntoWaterCheckBox);
            this.OptionsPanel.Controls.Add(this.FishingTimeoutUpDown);
            this.OptionsPanel.Controls.Add(this.label6);
            this.OptionsPanel.Controls.Add(this.label5);
            this.OptionsPanel.Controls.Add(this.GameTitleTextBox);
            this.OptionsPanel.Controls.Add(this.label4);
            this.OptionsPanel.Controls.Add(this.SignalMultiplierUpDown);
            this.OptionsPanel.Controls.Add(this.label3);
            this.OptionsPanel.Controls.Add(this.MomentumUpDown);
            this.OptionsPanel.Controls.Add(this.label11);
            this.OptionsPanel.Controls.Add(this.ErrorsGoalUpDown);
            this.OptionsPanel.Controls.Add(this.label2);
            this.OptionsPanel.Controls.Add(this.BrainsFileLabel);
            this.OptionsPanel.Controls.Add(this.GoCheckBox);
            this.OptionsPanel.Controls.Add(this.BrainsComboBox);
            this.OptionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OptionsPanel.Enabled = false;
            this.OptionsPanel.Location = new System.Drawing.Point(8, 0);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(322, 193);
            this.OptionsPanel.TabIndex = 0;
            // 
            // GenPopLimitUpDown
            // 
            this.GenPopLimitUpDown.Location = new System.Drawing.Point(250, 115);
            this.GenPopLimitUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.GenPopLimitUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GenPopLimitUpDown.Name = "GenPopLimitUpDown";
            this.GenPopLimitUpDown.Size = new System.Drawing.Size(70, 23);
            this.GenPopLimitUpDown.TabIndex = 16;
            this.GenPopLimitUpDown.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // NeuronsMemoryUpDown
            // 
            this.NeuronsMemoryUpDown.DecimalPlaces = 2;
            this.NeuronsMemoryUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.NeuronsMemoryUpDown.Location = new System.Drawing.Point(170, 160);
            this.NeuronsMemoryUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            131072});
            this.NeuronsMemoryUpDown.Name = "NeuronsMemoryUpDown";
            this.NeuronsMemoryUpDown.Size = new System.Drawing.Size(149, 23);
            this.NeuronsMemoryUpDown.TabIndex = 16;
            this.NeuronsMemoryUpDown.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // BlocksToUseUpDown
            // 
            this.BlocksToUseUpDown.Location = new System.Drawing.Point(0, 71);
            this.BlocksToUseUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.BlocksToUseUpDown.Name = "BlocksToUseUpDown";
            this.BlocksToUseUpDown.Size = new System.Drawing.Size(149, 23);
            this.BlocksToUseUpDown.TabIndex = 16;
            this.BlocksToUseUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // GenErrorsThresholdUpDown
            // 
            this.GenErrorsThresholdUpDown.Location = new System.Drawing.Point(1, 115);
            this.GenErrorsThresholdUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.GenErrorsThresholdUpDown.Name = "GenErrorsThresholdUpDown";
            this.GenErrorsThresholdUpDown.Size = new System.Drawing.Size(149, 23);
            this.GenErrorsThresholdUpDown.TabIndex = 16;
            this.GenErrorsThresholdUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(170, 144);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(126, 15);
            this.label14.TabIndex = 15;
            this.label14.Text = "Neurons memory max";
            // 
            // ThreadsUpDown
            // 
            this.ThreadsUpDown.Location = new System.Drawing.Point(250, 71);
            this.ThreadsUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ThreadsUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ThreadsUpDown.Name = "ThreadsUpDown";
            this.ThreadsUpDown.Size = new System.Drawing.Size(70, 23);
            this.ThreadsUpDown.TabIndex = 16;
            this.ThreadsUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(135, 15);
            this.label13.TabIndex = 15;
            this.label13.Text = "Gen errors threshold (%)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(250, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 15);
            this.label12.TabIndex = 15;
            this.label12.Text = "Gen pop lim";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(250, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 15);
            this.label10.TabIndex = 15;
            this.label10.Text = "Threads";
            // 
            // LookIntoWaterCheckBox
            // 
            this.LookIntoWaterCheckBox.Location = new System.Drawing.Point(234, 5);
            this.LookIntoWaterCheckBox.Name = "LookIntoWaterCheckBox";
            this.LookIntoWaterCheckBox.Size = new System.Drawing.Size(95, 16);
            this.LookIntoWaterCheckBox.TabIndex = 12;
            this.LookIntoWaterCheckBox.Text = "Water check";
            this.LookIntoWaterCheckBox.UseVisualStyleBackColor = true;
            // 
            // FishingTimeoutUpDown
            // 
            this.FishingTimeoutUpDown.Location = new System.Drawing.Point(80, 160);
            this.FishingTimeoutUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FishingTimeoutUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FishingTimeoutUpDown.Name = "FishingTimeoutUpDown";
            this.FishingTimeoutUpDown.Size = new System.Drawing.Size(70, 23);
            this.FishingTimeoutUpDown.TabIndex = 11;
            this.FishingTimeoutUpDown.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Timeout (s)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Use signals blocks";
            // 
            // GameTitleTextBox
            // 
            this.GameTitleTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GameTitleTextBox.Location = new System.Drawing.Point(170, 24);
            this.GameTitleTextBox.Name = "GameTitleTextBox";
            this.GameTitleTextBox.Size = new System.Drawing.Size(150, 23);
            this.GameTitleTextBox.TabIndex = 7;
            this.GameTitleTextBox.Text = "Minecraft";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Game title";
            // 
            // SignalMultiplierUpDown
            // 
            this.SignalMultiplierUpDown.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SignalMultiplierUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.SignalMultiplierUpDown.Location = new System.Drawing.Point(0, 160);
            this.SignalMultiplierUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.SignalMultiplierUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.SignalMultiplierUpDown.Name = "SignalMultiplierUpDown";
            this.SignalMultiplierUpDown.Size = new System.Drawing.Size(74, 23);
            this.SignalMultiplierUpDown.TabIndex = 5;
            this.SignalMultiplierUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SignalMultiplierUpDown.ValueChanged += new System.EventHandler(this.SignalMultiplierUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Signal (%)";
            // 
            // MomentumUpDown
            // 
            this.MomentumUpDown.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MomentumUpDown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MomentumUpDown.Location = new System.Drawing.Point(170, 115);
            this.MomentumUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.MomentumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MomentumUpDown.Name = "MomentumUpDown";
            this.MomentumUpDown.Size = new System.Drawing.Size(74, 23);
            this.MomentumUpDown.TabIndex = 5;
            this.MomentumUpDown.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(170, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 15);
            this.label11.TabIndex = 3;
            this.label11.Text = "Momentum";
            // 
            // ErrorsGoalUpDown
            // 
            this.ErrorsGoalUpDown.Location = new System.Drawing.Point(170, 71);
            this.ErrorsGoalUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ErrorsGoalUpDown.Name = "ErrorsGoalUpDown";
            this.ErrorsGoalUpDown.Size = new System.Drawing.Size(74, 23);
            this.ErrorsGoalUpDown.TabIndex = 5;
            this.ErrorsGoalUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Errors Goal";
            // 
            // BrainsFileLabel
            // 
            this.BrainsFileLabel.AutoSize = true;
            this.BrainsFileLabel.Location = new System.Drawing.Point(0, 6);
            this.BrainsFileLabel.Name = "BrainsFileLabel";
            this.BrainsFileLabel.Size = new System.Drawing.Size(39, 15);
            this.BrainsFileLabel.TabIndex = 2;
            this.BrainsFileLabel.Text = "Brains";
            // 
            // GoCheckBox
            // 
            this.GoCheckBox.Location = new System.Drawing.Point(88, 5);
            this.GoCheckBox.Name = "GoCheckBox";
            this.GoCheckBox.Size = new System.Drawing.Size(41, 16);
            this.GoCheckBox.TabIndex = 1;
            this.GoCheckBox.Text = "Go";
            this.GoCheckBox.UseVisualStyleBackColor = true;
            // 
            // BrainsComboBox
            // 
            this.BrainsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BrainsComboBox.FormattingEnabled = true;
            this.BrainsComboBox.Location = new System.Drawing.Point(0, 24);
            this.BrainsComboBox.Name = "BrainsComboBox";
            this.BrainsComboBox.Size = new System.Drawing.Size(150, 23);
            this.BrainsComboBox.TabIndex = 0;
            this.BrainsComboBox.SelectedIndexChanged += new System.EventHandler(this.BrainsComboBox_SelectedIndexChanged);
            this.BrainsComboBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrainsComboBox_MouseDown);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.FinalizeCheckBox);
            this.MainPanel.Controls.Add(this.HitLabel);
            this.MainPanel.Controls.Add(this.GenerationLabel);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.label9);
            this.MainPanel.Controls.Add(this.label8);
            this.MainPanel.Controls.Add(this.label7);
            this.MainPanel.Controls.Add(this.FishingCheckBox);
            this.MainPanel.Controls.Add(this.ShowLiveCheckBox);
            this.MainPanel.Controls.Add(this.MutateCheckBox);
            this.MainPanel.Controls.Add(this.GenerateCheckBox);
            this.MainPanel.Controls.Add(this.RecordCheckBox);
            this.MainPanel.Controls.Add(this.ErrorsCountLabel);
            this.MainPanel.Controls.Add(this.SurvivalsLabel);
            this.MainPanel.Controls.Add(this.PopulationLabel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(338, 75);
            this.MainPanel.TabIndex = 22;
            this.MainPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseClick);
            // 
            // FinalizeCheckBox
            // 
            this.FinalizeCheckBox.AutoSize = true;
            this.FinalizeCheckBox.Enabled = false;
            this.FinalizeCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FinalizeCheckBox.Location = new System.Drawing.Point(268, 21);
            this.FinalizeCheckBox.Name = "FinalizeCheckBox";
            this.FinalizeCheckBox.Size = new System.Drawing.Size(62, 19);
            this.FinalizeCheckBox.TabIndex = 37;
            this.FinalizeCheckBox.Text = "Finalize";
            this.FinalizeCheckBox.UseVisualStyleBackColor = true;
            this.FinalizeCheckBox.CheckedChanged += new System.EventHandler(this.FinalizeCheckBox_CheckedChanged);
            // 
            // HitLabel
            // 
            this.HitLabel.AutoSize = true;
            this.HitLabel.BackColor = System.Drawing.Color.LightGreen;
            this.HitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.HitLabel.ForeColor = System.Drawing.Color.Green;
            this.HitLabel.Location = new System.Drawing.Point(10, 55);
            this.HitLabel.Name = "HitLabel";
            this.HitLabel.Size = new System.Drawing.Size(28, 17);
            this.HitLabel.TabIndex = 36;
            this.HitLabel.Text = "Hit!";
            this.HitLabel.Visible = false;
            // 
            // GenerationLabel
            // 
            this.GenerationLabel.AutoSize = true;
            this.GenerationLabel.Location = new System.Drawing.Point(229, 23);
            this.GenerationLabel.Name = "GenerationLabel";
            this.GenerationLabel.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.GenerationLabel.Size = new System.Drawing.Size(14, 15);
            this.GenerationLabel.TabIndex = 35;
            this.GenerationLabel.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 34;
            this.label1.Text = "Generation:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(193, 55);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 15);
            this.label9.TabIndex = 33;
            this.label9.Text = "Errors:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(177, 39);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 15);
            this.label8.TabIndex = 32;
            this.label8.Text = "Survivals:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(165, 7);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 15);
            this.label7.TabIndex = 31;
            this.label7.Text = "Population:";
            // 
            // FishingCheckBox
            // 
            this.FishingCheckBox.AutoSize = true;
            this.FishingCheckBox.Location = new System.Drawing.Point(10, 38);
            this.FishingCheckBox.Name = "FishingCheckBox";
            this.FishingCheckBox.Size = new System.Drawing.Size(64, 19);
            this.FishingCheckBox.TabIndex = 28;
            this.FishingCheckBox.Text = "Fishing";
            this.FishingCheckBox.UseVisualStyleBackColor = true;
            this.FishingCheckBox.CheckedChanged += new System.EventHandler(this.FishingCheckBox_CheckedChanged);
            // 
            // ShowLiveCheckBox
            // 
            this.ShowLiveCheckBox.AutoSize = true;
            this.ShowLiveCheckBox.Location = new System.Drawing.Point(10, 22);
            this.ShowLiveCheckBox.Name = "ShowLiveCheckBox";
            this.ShowLiveCheckBox.Size = new System.Drawing.Size(76, 19);
            this.ShowLiveCheckBox.TabIndex = 22;
            this.ShowLiveCheckBox.Text = "ShowLive";
            this.ShowLiveCheckBox.UseVisualStyleBackColor = true;
            this.ShowLiveCheckBox.CheckedChanged += new System.EventHandler(this.ShowLiveCheckBox_CheckedChanged);
            // 
            // MutateCheckBox
            // 
            this.MutateCheckBox.AutoSize = true;
            this.MutateCheckBox.Location = new System.Drawing.Point(86, 38);
            this.MutateCheckBox.Name = "MutateCheckBox";
            this.MutateCheckBox.Size = new System.Drawing.Size(64, 19);
            this.MutateCheckBox.TabIndex = 30;
            this.MutateCheckBox.Text = "Mutate";
            this.MutateCheckBox.UseVisualStyleBackColor = true;
            this.MutateCheckBox.CheckedChanged += new System.EventHandler(this.MutateCheckBox_CheckedChanged);
            // 
            // GenerateCheckBox
            // 
            this.GenerateCheckBox.AutoSize = true;
            this.GenerateCheckBox.Location = new System.Drawing.Point(86, 22);
            this.GenerateCheckBox.Name = "GenerateCheckBox";
            this.GenerateCheckBox.Size = new System.Drawing.Size(73, 19);
            this.GenerateCheckBox.TabIndex = 26;
            this.GenerateCheckBox.Text = "Generate";
            this.GenerateCheckBox.UseVisualStyleBackColor = true;
            this.GenerateCheckBox.CheckedChanged += new System.EventHandler(this.GenerateCheckBox_CheckedChanged);
            // 
            // RecordCheckBox
            // 
            this.RecordCheckBox.AutoSize = true;
            this.RecordCheckBox.Location = new System.Drawing.Point(86, 6);
            this.RecordCheckBox.Name = "RecordCheckBox";
            this.RecordCheckBox.Size = new System.Drawing.Size(63, 19);
            this.RecordCheckBox.TabIndex = 29;
            this.RecordCheckBox.Text = "Record";
            this.RecordCheckBox.UseVisualStyleBackColor = true;
            this.RecordCheckBox.CheckedChanged += new System.EventHandler(this.RecordCheckBox_CheckedChanged);
            // 
            // ErrorsCountLabel
            // 
            this.ErrorsCountLabel.AutoSize = true;
            this.ErrorsCountLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ErrorsCountLabel.Location = new System.Drawing.Point(229, 55);
            this.ErrorsCountLabel.Name = "ErrorsCountLabel";
            this.ErrorsCountLabel.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.ErrorsCountLabel.Size = new System.Drawing.Size(14, 15);
            this.ErrorsCountLabel.TabIndex = 27;
            this.ErrorsCountLabel.Text = "0";
            this.ErrorsCountLabel.Click += new System.EventHandler(this.ErrorsCountLabel_Click);
            // 
            // SurvivalsLabel
            // 
            this.SurvivalsLabel.AutoSize = true;
            this.SurvivalsLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SurvivalsLabel.Location = new System.Drawing.Point(229, 39);
            this.SurvivalsLabel.Name = "SurvivalsLabel";
            this.SurvivalsLabel.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.SurvivalsLabel.Size = new System.Drawing.Size(14, 15);
            this.SurvivalsLabel.TabIndex = 25;
            this.SurvivalsLabel.Text = "0";
            this.SurvivalsLabel.Click += new System.EventHandler(this.SurvivalsLabel_Click);
            // 
            // PopulationLabel
            // 
            this.PopulationLabel.AutoSize = true;
            this.PopulationLabel.Location = new System.Drawing.Point(229, 7);
            this.PopulationLabel.Name = "PopulationLabel";
            this.PopulationLabel.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.PopulationLabel.Size = new System.Drawing.Size(14, 15);
            this.PopulationLabel.TabIndex = 24;
            this.PopulationLabel.Text = "0";
            // 
            // TopMostCheckBox
            // 
            this.TopMostCheckBox.Location = new System.Drawing.Point(10, 6);
            this.TopMostCheckBox.Name = "TopMostCheckBox";
            this.TopMostCheckBox.Size = new System.Drawing.Size(72, 18);
            this.TopMostCheckBox.TabIndex = 21;
            this.TopMostCheckBox.Text = "TopMost";
            this.TopMostCheckBox.UseVisualStyleBackColor = true;
            this.TopMostCheckBox.CheckedChanged += new System.EventHandler(this.TopMostCheckBox_CheckedChanged);
            // 
            // FishingTimeoutTimer
            // 
            this.FishingTimeoutTimer.Interval = 20000;
            this.FishingTimeoutTimer.Tick += new System.EventHandler(this.FishingTimeoutTimer_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.TrainingFileStatusLabel,
            this.BrainsFileStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 353);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(338, 22);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.StatusLabel.Size = new System.Drawing.Size(166, 17);
            this.StatusLabel.Text = "                                                     ";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TrainingFileStatusLabel
            // 
            this.TrainingFileStatusLabel.AutoSize = false;
            this.TrainingFileStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.TrainingFileStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TrainingFileStatusLabel.Name = "TrainingFileStatusLabel";
            this.TrainingFileStatusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.TrainingFileStatusLabel.Size = new System.Drawing.Size(100, 17);
            this.TrainingFileStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BrainsFileStatusLabel
            // 
            this.BrainsFileStatusLabel.AutoSize = false;
            this.BrainsFileStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.BrainsFileStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BrainsFileStatusLabel.Name = "BrainsFileStatusLabel";
            this.BrainsFileStatusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.BrainsFileStatusLabel.Size = new System.Drawing.Size(187, 17);
            this.BrainsFileStatusLabel.Spring = true;
            this.BrainsFileStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WaitPlayerLooksIntoWaterTimer
            // 
            this.WaitPlayerLooksIntoWaterTimer.Interval = 2000;
            this.WaitPlayerLooksIntoWaterTimer.Tick += new System.EventHandler(this.WaitPlayerLooksIntoWaterTimer_Tick);
            // 
            // EvolutionTimer
            // 
            this.EvolutionTimer.Interval = 500;
            this.EvolutionTimer.Tick += new System.EventHandler(this.EvolutionTimer_Tick);
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 375);
            this.Controls.Add(this.TopMostCheckBox);
            this.Controls.Add(this.SettingsPanel);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.statusStrip1);
            this.KeyPreview = true;
            this.Name = "AppForm";
            this.Text = "McFisher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AppForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AppForm_FormClosed);
            this.Load += new System.EventHandler(this.AppForm_Load);
            this.SettingsPanel.ResumeLayout(false);
            this.SettingsPanel.PerformLayout();
            this.OptionsPanel.ResumeLayout(false);
            this.OptionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GenPopLimitUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NeuronsMemoryUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlocksToUseUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GenErrorsThresholdUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FishingTimeoutUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SignalMultiplierUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MomentumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorsGoalUpDown)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Timer ShowLiveTimer;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.Timer ShowHitLabelTimer;
    private System.Windows.Forms.Timer RecordCheckKeyTimer;
    private System.Windows.Forms.Timer RodTimer;
    private System.Windows.Forms.Panel SettingsPanel;
    private System.Windows.Forms.TextBox InfoBox;
    private System.Windows.Forms.Panel OptionsPanel;
    private System.Windows.Forms.ComboBox BrainsComboBox;
    private System.Windows.Forms.CheckBox GoCheckBox;
    private System.Windows.Forms.Label BrainsFileLabel;
    private System.Windows.Forms.Panel MainPanel;
    private System.Windows.Forms.CheckBox MutateCheckBox;
    private System.Windows.Forms.CheckBox RecordCheckBox;
    private System.Windows.Forms.CheckBox FishingCheckBox;
    private System.Windows.Forms.Label ErrorsCountLabel;
    private System.Windows.Forms.CheckBox GenerateCheckBox;
    private System.Windows.Forms.Label SurvivalsLabel;
    private System.Windows.Forms.Label PopulationLabel;
    private System.Windows.Forms.CheckBox TopMostCheckBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Timer FishingTimeoutTimer;
    private Label label3;
    private Label label4;
    private Label label5;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel StatusLabel;
    private ToolStripStatusLabel TrainingFileStatusLabel;
    private ToolStripStatusLabel BrainsFileStatusLabel;
    private Label label6;
    public NumericUpDown SignalMultiplierUpDown;
    public TextBox GameTitleTextBox;
    public NumericUpDown FishingTimeoutUpDown;
    private System.Windows.Forms.Timer WaitPlayerLooksIntoWaterTimer;
    public CheckBox LookIntoWaterCheckBox;
    private Label label9;
    private Label label8;
    private Label label7;
    private Label label10;
    public NumericUpDown ThreadsUpDown;
    private System.Windows.Forms.Timer EvolutionTimer;
    private Label GenerationLabel;
    private Label label1;
    public NumericUpDown GenPopLimitUpDown;
    private Label label12;
    private Label label11;
    public NumericUpDown GenErrorsThresholdUpDown;
    private Label label13;
    public NumericUpDown ErrorsGoalUpDown;
    public NumericUpDown MomentumUpDown;
    private Label HitLabel;
    public NumericUpDown NeuronsMemoryUpDown;
    private Label label14;
    public CheckBox ShowLiveCheckBox;
    public NumericUpDown BlocksToUseUpDown;
    private CheckBox FinalizeCheckBox;
}