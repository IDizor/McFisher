using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using FftSharp;
using McFisher.AI;
using McFisher.Misc;
using McFisher.Training;
using NAudio.Wave;

namespace McFisher;

public partial class AppForm : Form
{
    Settings AppSettings;
    SystemAudioCapture? SystemWave;
    PlotForm PlotForm = new();
    AiDiagram DiagramForm = new();
    Bitmap ScreenBitmap;
    public const int BufferMilliseconds = 40;
    
    int SampleRate;
    int SampleCount;
    float FrequencyResolution;

    double[]? AudioValues;
    double[]? FftValues;
    Queue<float[]> FrequenciesPrev = new();
    float SignalMultiplier = 1;
    int RodTimerInterval = 2000;
    bool NoPlot = true;
    bool GoalKeyPressed = false;
    TrainingSet? Training;
    TrainingSet? ReferenceTraining;
    AiBrain? SelectedBrain;
    Evolution? Evolution = null;
    AiBrain? BestEvolutionBrain = null;

    private static FloatRange[] FrequencyRanges = new FloatRange[98]
    {
        new FloatRange(50, 74.9999f),
        new FloatRange(75, 99.9999f),
        new FloatRange(100, 124.9999f),
        new FloatRange(125, 149.9999f),
        new FloatRange(150, 174.9999f),
        new FloatRange(175, 199.9999f),
        new FloatRange(200, 224.9999f),
        new FloatRange(225, 249.9999f),
        new FloatRange(250, 274.9999f),
        new FloatRange(275, 299.9999f),
        new FloatRange(300, 324.9999f),
        new FloatRange(325, 349.9999f),
        new FloatRange(350, 374.9999f),
        new FloatRange(375, 399.9999f),
        new FloatRange(400, 424.9999f),
        new FloatRange(425, 449.9999f),
        new FloatRange(450, 474.9999f),
        new FloatRange(475, 499.9999f),
        new FloatRange(500, 524.9999f),
        new FloatRange(525, 549.9999f),
        new FloatRange(550, 574.9999f),
        new FloatRange(575, 599.9999f),
        new FloatRange(600, 624.9999f),
        new FloatRange(625, 649.9999f),
        new FloatRange(650, 674.9999f),
        new FloatRange(675, 699.9999f),
        new FloatRange(700, 724.9999f),
        new FloatRange(725, 749.9999f),
        new FloatRange(750, 774.9999f),
        new FloatRange(775, 799.9999f),
        new FloatRange(800, 824.9999f),
        new FloatRange(825, 849.9999f),
        new FloatRange(850, 874.9999f),
        new FloatRange(875, 899.9999f),
        new FloatRange(900, 924.9999f),
        new FloatRange(925, 949.9999f),
        new FloatRange(950, 974.9999f),
        new FloatRange(975, 999.9999f),
        new FloatRange(1000, 1024.9999f),
        new FloatRange(1025, 1049.9999f),
        new FloatRange(1050, 1074.9999f),
        new FloatRange(1075, 1099.9999f),
        new FloatRange(1100, 1124.9999f),
        new FloatRange(1125, 1149.9999f),
        new FloatRange(1150, 1174.9999f),
        new FloatRange(1175, 1199.9999f),
        new FloatRange(1200, 1224.9999f),
        new FloatRange(1225, 1249.9999f),
        new FloatRange(1250, 1274.9999f),
        new FloatRange(1275, 1299.9999f),
        new FloatRange(1300, 1324.9999f),
        new FloatRange(1325, 1349.9999f),
        new FloatRange(1350, 1374.9999f),
        new FloatRange(1375, 1399.9999f),
        new FloatRange(1400, 1424.9999f),
        new FloatRange(1425, 1449.9999f),
        new FloatRange(1450, 1474.9999f),
        new FloatRange(1475, 1499.9999f),
        new FloatRange(1500, 1524.9999f),
        new FloatRange(1525, 1549.9999f),
        new FloatRange(1550, 1574.9999f),
        new FloatRange(1575, 1599.9999f),
        new FloatRange(1600, 1624.9999f),
        new FloatRange(1625, 1649.9999f),
        new FloatRange(1650, 1674.9999f),
        new FloatRange(1675, 1699.9999f),
        new FloatRange(1700, 1724.9999f),
        new FloatRange(1725, 1749.9999f),
        new FloatRange(1750, 1774.9999f),
        new FloatRange(1775, 1799.9999f),
        new FloatRange(1800, 1824.9999f),
        new FloatRange(1825, 1849.9999f),
        new FloatRange(1850, 1874.9999f),
        new FloatRange(1875, 1899.9999f),
        new FloatRange(1900, 1924.9999f),
        new FloatRange(1925, 1949.9999f),
        new FloatRange(1950, 1974.9999f),
        new FloatRange(1975, 1999.9999f),
        new FloatRange(2000, 2024.9999f),
        new FloatRange(2025, 2049.9999f),
        new FloatRange(2050, 2074.9999f),
        new FloatRange(2075, 2099.9999f),
        new FloatRange(2100, 2124.9999f),
        new FloatRange(2125, 2149.9999f),
        new FloatRange(2150, 2174.9999f),
        new FloatRange(2175, 2199.9999f),
        new FloatRange(2200, 2224.9999f),
        new FloatRange(2225, 2249.9999f),
        new FloatRange(2250, 2274.9999f),
        new FloatRange(2275, 2299.9999f),
        new FloatRange(2300, 2324.9999f),
        new FloatRange(2325, 2349.9999f),
        new FloatRange(2350, 2374.9999f),
        new FloatRange(2375, 2399.9999f),
        new FloatRange(2400, 2424.9999f),
        new FloatRange(2425, 2449.9999f),
        new FloatRange(2450, 2474.9999f),
        new FloatRange(2475, 2499.9999f)
    };

    public AppForm()
    {
        InitializeComponent();

        foreach (var control in MainPanel.Controls)
        {
            if (control is CheckBox checkbox)
            {
                checkbox.CheckedChanged += AnyMainCheckBox_CheckedChanged;
            }
        }

        RodTimerInterval = RodTimer.Interval;

        Directory.CreateDirectory("jsons");
        SetProcessPriority(ProcessPriorityClass.Normal);
    }

    private void SetProcessPriority(ProcessPriorityClass priority)
    {
        using (Process p = Process.GetCurrentProcess())
            p.PriorityClass = priority;
    }

    private void AppForm_Load(object sender, EventArgs e)
    {
        AppSettings = Settings.Load();
    }

    private void AppForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        AppSettings.Save();
        PlotForm.Hide();
        StopFishingMonitoring();
        Evolution?.Stop();
        Visible = false;
    }

    private void AppForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        Thread.Sleep(1000);
        Environment.Exit(0);
    }

    private void AnyMainCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        // disable other checkboxes on the main panel
        foreach (var control in MainPanel.Controls)
        {
            if (control != sender && control is CheckBox checkbox)
            {
                checkbox.Enabled = !((CheckBox)sender).Checked;
            }
        }
    }

    private void TopMostCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        TopMost = TopMostCheckBox.Checked;
        PlotForm.TopMost = TopMostCheckBox.Checked;
    }

    private void ShowLiveTimer_Tick(object sender, EventArgs e)
    {
        if (AudioValues == null)
        {
            return;
        }

        UpdateFFT();
    }

    private void UpdateFFT()
    {
        if (NoPlot)
        {
            NoPlot = false;
            FftValues = Transform.FFTmagnitude(AudioValues);
            
            SampleRate = SystemWave.WaveFormat.SampleRate;
            SampleCount = AudioValues.Length;
            FrequencyResolution = (float)SampleRate / SampleCount;

            InfoBox.AppendText($"Buffer: {(float)AudioValues.Length / SystemWave.WaveFormat.SampleRate * 1000} ms\r\n");
            InfoBox.AppendText($"SampleRate: {SampleRate} Hz\r\n");
            InfoBox.AppendText($"Samples: {SampleCount}\r\n");
            InfoBox.AppendText($"Res: {FrequencyResolution} Hz");

            PlotForm.FftPlot.Plot.Clear();
            PlotForm.FftPlot.Plot.Title("Fast Fourier Transform");
            PlotForm.FftPlot.Plot.XLabel("Frequency (Hz)");
            PlotForm.FftPlot.Plot.YLabel("Magnitude (RMS?)");
            PlotForm.FftPlot.Plot.SetAxisLimits(xMin: -200, xMax: 6000, yMin: -0.02, yMax: 0.3);
            PlotForm.FftPlot.Plot.AddSignal(FftValues, (double)SampleCount / SampleRate);
            return;
        }

        var newFftValues = Transform.FFTmagnitude(AudioValues);
        
        if ((bool)ShowLiveCheckBox.Tag == true)
        {
            for (int i = 0; i < FftValues.Length; i++)
                FftValues[i] = Math.Max(FftValues[i], newFftValues[i]);
        }
        else
        {
            Array.Copy(newFftValues, FftValues, FftValues.Length);
        }

        PlotForm.FftPlot.RefreshRequest();
    }

    public void DataAvailable(object? sender, WaveInEventArgs e)
    {
        int bytesPerSample = SystemWave.WaveFormat.BitsPerSample / 8;
        int samplesCount = e.Buffer.Length / bytesPerSample / SystemWave.WaveFormat.Channels;

        var audioValues = new double[samplesCount];
        for (int i = 0; i < audioValues.Length; i++)
        {
            var startIndex = i * bytesPerSample * SystemWave.WaveFormat.Channels;

            switch (SystemWave.WaveFormat.BitsPerSample)
            {
                case 32:
                    audioValues[i] = BitConverter.ToSingle(e.Buffer, startIndex);

                    // if Right channel has greater value - take it instead
                    if (SystemWave.WaveFormat.Channels > 1)
                    {
                        audioValues[i] = Math.Max(audioValues[i],
                            BitConverter.ToSingle(e.Buffer, startIndex + bytesPerSample));
                    }
                    break;
                default:
                    throw new Exception($"Unsupported BitsPerSample = {SystemWave.WaveFormat.BitsPerSample}.");
            }
        }

        AudioValues = FftSharp.Pad.ZeroPad(audioValues);

        if (FishingCheckBox.Checked)
        {
            if (!WaitPlayerLooksIntoWaterTimer.Enabled)
            {
                CheckLiveValues();
            }
        }
        else if (RecordCheckBox.Checked)
        {
            RecordFFT();
        }
    }

    private void RecordCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (RecordCheckBox.Checked)
        {
            ReferenceTraining = null;

            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                openFileDialog1.Title = "Select Reference Training to use its goal marks";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ReferenceTraining = openFileDialog1.FileName.ParseJsonFile<TrainingSet>();
                    ReferenceTraining.SaveAsJson("jsons\\tr.json");
                }
            }

            SetProcessPriority(ProcessPriorityClass.AboveNormal);
            NoPlot = true;
            GoalKeyPressed = false;
            RecordCheckKeyTimer.Start();
            Training = new TrainingSet();
            SystemWave = new SystemAudioCapture(BufferMilliseconds);
            SystemWave.DataAvailable += DataAvailable;
            SystemWave.RecordingStopped += (s, a) =>
            {
                SystemWave.Dispose();
            };
            SystemWave.StartRecording();
        }
        else
        {
            RecordCheckKeyTimer.Stop();
            SystemWave.StopRecording();
            Training.Complete();
            HitLabel.Visible = false;

            var trainingNumber = 0;
            var trainingFile = "";
            do
            {
                trainingFile = $"training.{trainingNumber:000}.json";
                trainingNumber++;
            }
            while (AiTools.FileExists("jsons\\" + trainingFile));
            Training.SaveAsJson("jsons\\" + trainingFile);
            StatusLabel.Text = "Saved: " + trainingFile;
            SetProcessPriority(ProcessPriorityClass.Normal);
        }
    }

    private void RecordFFT()
    {
        var frequencies = CalcFrequencyRangesValues();
        var block = Training.AddBlock(frequencies, GoalKeyPressed, ReferenceTraining);
        TrainingHit(block?.IsGoal == true);
    }

    private void ShowLiveCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (ShowLiveCheckBox.Checked)
        {
            ShowLiveCheckBox.Tag = Keyboard.IsKeyDown(Key.LeftShift);
            ShowLiveTimer.Interval = BufferMilliseconds;
            SystemWave = new SystemAudioCapture(BufferMilliseconds);
            SystemWave.DataAvailable += DataAvailable;
            SystemWave.RecordingStopped += (s, a) =>
            {
                SystemWave.Dispose();
            };

            NoPlot = true;
            InfoBox.Clear();
            PlotForm.Show();
            PlotForm.Location = new Point(Location.X + Size.Width - 14, Location.Y);
            SystemWave.StartRecording();
            ShowLiveTimer.Start();
        }
        else
        {
            PlotForm.Hide();
            ShowLiveTimer.Stop();
            SystemWave.StopRecording();
        }
    }

    private TrainingSet? SelectTrainings()
    {
        TrainingSet? training = null;

        openFileDialog1.Title = "Select Trainings";
        openFileDialog1.Multiselect = true;
        if (openFileDialog1.ShowDialog() != DialogResult.OK)
        {
            return null;
        }

        foreach (var f in openFileDialog1.FileNames)
        {
            var t = f.ParseJsonFile<TrainingSet>();

            if (training == null)
            {
                training = t;
                TrainingFileStatusLabel.Text = Path.GetFileName(f);
            }
            else
            {
                training.Blocks.AddRange(t.Blocks);
            }
        }

        return training;
    }

    private void GenerateCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (GenerateCheckBox.Checked)
        {
            if (EvolutionTimer.Enabled)
            {
                GenerateCheckBox.Checked = false;
                return;
            }

            if (!Keyboard.IsKeyDown(Key.LeftShift) || Training == null)
            {
                Training = SelectTrainings();
                if (Training == null)
                {
                    GenerateCheckBox.Checked = false;
                    return;
                }
            }

            WaitForGo();
            AppSettings.Save();

            Evolution = new Evolution(new AiConfig()
            {
                GenerationPopulationLimit = GenPopLimitUpDown.IntValue(),
                ThreadsCount = ThreadsUpDown.IntValue(),
                GenerationMomentum = MomentumUpDown.IntValue(),
                Training = Training,
                ErrorsGoal = ErrorsGoalUpDown.IntValue(),
                AcceptableErrorsThreshold = (float)GenErrorsThresholdUpDown.Value / 100,
                NeuronsMemory = (float)NeuronsMemoryUpDown.Value,
                FrequenciesPrevMax = FrequenciesPrevMaxUpDown.IntValue(),
            });

            SetProcessPriority(ProcessPriorityClass.BelowNormal);
            BestEvolutionBrain = null;
            Evolution.Start();
            EvolutionTimer.Start();

            StatusLabel.Text = Evolution.Config.EvolutionDir;
        }
        else
        {
            if (EvolutionTimer.Enabled && Evolution?.Running == true)
            {
                EvolutionTimer.Stop();
                Evolution.Stop();
                EvolutionTimer_Tick(sender, EventArgs.Empty);
            }

            SetProcessPriority(ProcessPriorityClass.Normal);
        }
    }

    private void MutateCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (MutateCheckBox.Checked)
        {
            if (EvolutionTimer.Enabled)
            {
                MutateCheckBox.Checked = false;
                return;
            }

            if (!Keyboard.IsKeyDown(Key.LeftShift) || Training == null || BrainsComboBox.DataSource == null)
            {
                Training = SelectTrainings();
                if (Training == null)
                {
                    MutateCheckBox.Checked = false;
                    return;
                }

                openFileDialog1.Title = "Select Brains";
                if (openFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    MutateCheckBox.Checked = false;
                    return;
                }

                LoadBrains(openFileDialog1.FileName);
            }

            WaitForGo();
            AppSettings.Save();

            var brains = Keyboard.IsKeyDown(Key.LeftShift)
                ? new AiBrain[] { SelectedBrain }
                : (AiBrain[])BrainsComboBox.DataSource;

            BestEvolutionBrain = brains[^1];
            
            Evolution = new Evolution(new AiConfig()
            {
                GenerationPopulationLimit = GenPopLimitUpDown.IntValue(),
                ThreadsCount = ThreadsUpDown.IntValue(),
                GenerationMomentum = MomentumUpDown.IntValue(),
                EvolutionDir = Path.GetDirectoryName((string)BrainsComboBox.Tag),
                Training = Training,
                Prototypes = brains.ToList(),
                ErrorsGoal = ErrorsGoalUpDown.IntValue(),
                AcceptableErrorsThreshold = (float)GenErrorsThresholdUpDown.Value / 100,
                NeuronsMemory = (float)NeuronsMemoryUpDown.Value,
                FrequenciesPrevMax = FrequenciesPrevMaxUpDown.IntValue(),
            });

            SetProcessPriority(ProcessPriorityClass.BelowNormal);
            Evolution.Start();
            EvolutionTimer.Start();
            
            StatusLabel.Text = Evolution.Config.EvolutionDir;
        }
        else
        {
            if (EvolutionTimer.Enabled && Evolution?.Running == true)
            {
                EvolutionTimer.Stop();
                Evolution.Stop();
                EvolutionTimer_Tick(sender, EventArgs.Empty);
            }

            SetProcessPriority(ProcessPriorityClass.Normal);
        }
    }

    private void ListenLiveTimer_Tick(object sender, EventArgs e)
    {
        ShowHitLabelTimer.Stop();
        HitLabel.Visible = false;
        Application.DoEvents();
    }

    private void FishingCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (FishingCheckBox.Checked)
        {
            var allJsons = Directory.GetFiles("jsons").Where(f => f.EndsWith(".json")).ToArray();
            var fastStart = allJsons.Length == 1 && allJsons[0].EndsWith("\\brain.json")
                && !Keyboard.IsKeyDown(Key.LeftShift);

            if (fastStart)
            {
                LoadBrains(allJsons[0]);
            }
            else if (!Keyboard.IsKeyDown(Key.LeftShift) || BrainsComboBox.DataSource == null)
            {
                openFileDialog1.Title = "Select Brains";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    LoadBrains(openFileDialog1.FileName);
                }
                else
                {
                    FishingCheckBox.Checked = false;
                    return;
                }
            }

            WaitForGo(fastStart);
            AppSettings.Save();
            SetProcessPriority(ProcessPriorityClass.AboveNormal);
            WaitPlayerLooksIntoWaterTimer.Stop();
            FishingTimeoutTimer.Interval = FishingTimeoutUpDown.IntValue() * 1000;
            StartFishingMonitoring();
            HitLabel.Visible = false;
        }
        else
        {
            FishingTimeoutTimer.Stop();
            WaitPlayerLooksIntoWaterTimer.Stop();
            StopFishingMonitoring();
            SetProcessPriority(ProcessPriorityClass.Normal);
        }
    }

    private void StartFishingMonitoring()
    {
        NoPlot = true;
        FrequenciesPrev.Clear();
        SystemWave = new SystemAudioCapture(BufferMilliseconds);
        SystemWave.DataAvailable += DataAvailable;
        SystemWave.RecordingStopped += (s, a) =>
        {
            SystemWave.Dispose();
            SystemWave = null;
        };
        SystemWave.StartRecording();
    }

    private void StopFishingMonitoring()
    {
        if (SystemWave != null)
        {
            SystemWave.StopRecording();
        }

        if (ScreenCap.IsActive)
        {
            ScreenCap.StopCapture();
        }
    }

    private float[] CalcFrequencyRangesValues()
    {
        if (NoPlot)
        {
            NoPlot = false;
            SampleRate = SystemWave.WaveFormat.SampleRate;
            SampleCount = AudioValues.Length;
            FrequencyResolution = (float)SampleRate / SampleCount;

            // update strart and end indices for each range
            for (int i = 0; i < FrequencyRanges.Length; i++)
            {
                FrequencyRanges[i].StartIndex = (int)(FrequencyRanges[i].X / FrequencyResolution) + 1;
                FrequencyRanges[i].EndIndex = (int)(FrequencyRanges[i].Y / FrequencyResolution);
            }
        }

        FftValues = Transform.FFTmagnitude(AudioValues);
        var frequencies = new float[FrequencyRanges.Length];

        for (int i = 0; i < FrequencyRanges.Length; i++)
        {
            frequencies[i] = 0;

            // read range values
            for (int j = FrequencyRanges[i].StartIndex; j <= FrequencyRanges[i].EndIndex; j++)
            {
                frequencies[i] = Math.Max(frequencies[i], (float)(FftValues[j] * SignalMultiplier));
            }

            // normalization
            frequencies[i] = frequencies[i] / 0.15f;
        }

        return frequencies;
    }

    private void CheckLiveValues()
    {
        if (SelectedBrain != null)
        {
            var frequencies = CalcFrequencyRangesValues();
            var frequenciesPrevMax = FrequenciesPrevMaxUpDown.IntValue();

            if (FrequenciesPrev.Count < frequenciesPrevMax)
            {
                FrequenciesPrev.Enqueue(frequencies);
                return;
            }

            if (frequenciesPrevMax > 0)
            {
                SelectedBrain.ProcessSignals(frequencies, FrequenciesPrev.Dequeue());
                FrequenciesPrev.Enqueue(frequencies);
            }
            else
            {
                SelectedBrain.ProcessSignals(frequencies, null);
            }
            
            if (SelectedBrain.Result)
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        if (HitLabel.InvokeRequired)
        {
            Action safeCall = delegate { Hit(); };
            HitLabel.Invoke(safeCall);
        }
        else
        {
            HitLabel.Visible = true;
            Application.DoEvents();
            if (!RodTimer.Enabled)
            {
                if (TryUseRod())
                {
                    RodTimer.Start();
                }
                FishingTimeoutTimer.Restart();
            }
            ShowHitLabelTimer.Restart();
        }
    }

    private void TrainingHit(bool visible)
    {
        if (HitLabel.InvokeRequired)
        {
            Action<bool> safeCall = delegate { TrainingHit(visible); };
            HitLabel.Invoke(safeCall, visible);
        }
        else
        {
            HitLabel.Visible = visible;
            Application.DoEvents();
        }
    }

    private void RecordCheckKeyTimer_Tick(object sender, EventArgs e)
    {
        GoalKeyPressed = Keyboard.IsKeyDown(Key.LeftCtrl);
    }

    private void RodTimer_Tick(object sender, EventArgs e)
    {
        if (RodTimer.Interval == RodTimerInterval)
        {
            // cast the rod and
            TryUseRod(skipLookCheck: true);
            RodTimer.Interval = RodTimerInterval + 10; // keep timer running for a while to block immediate re-cast
            RodTimer.Restart();
        }
        else
        {
            // stop timer to allow next cast
            RodTimer.Stop();
            RodTimer.Interval = RodTimerInterval;
        }
        
    }

    private bool TryUseRod(bool skipLookCheck = false)
    {
        if (WaitPlayerLooksIntoWaterTimer.Enabled)
        {
            return false;
        }

        var activeWindow = Native.GetForegroundWindow();
        var windowTitle = Native.GetWindowTitle(activeWindow);

        if (windowTitle?.Contains(GameTitleTextBox.Text) == true)
        {
            if (skipLookCheck || !LookIntoWaterCheckBox.Checked || PlayerLooksIntoWater())
            {
                Native.SendMessage(activeWindow, Native.WM_RBUTTONDOWN, 1, Native.CreateLParam(500, 500));
                Native.SendMessage(activeWindow, Native.WM_RBUTTONUP, 1, Native.CreateLParam(500, 500));

                return true;
            }
            else if (LookIntoWaterCheckBox.Checked)
            {
                StopFishingMonitoring();
                WaitPlayerLooksIntoWaterTimer.Tag = 0;
                WaitPlayerLooksIntoWaterTimer.Start();
            }
        }
        else
        {
            if (LookIntoWaterCheckBox.Checked)
            {
                StopFishingMonitoring();
                WaitPlayerLooksIntoWaterTimer.Tag = 0;
                WaitPlayerLooksIntoWaterTimer.Start();
            }
        }

        return false;
    }

    public void LoadBrains(string fileName)
    {
        BrainsComboBox.DataSource = null;
        BrainsComboBox.Tag = fileName;
        BrainsFileStatusLabel.Text = Path.GetFileName(fileName);

        AiBrain[] brains = fileName.ParseJsonFile<AiBrain[]>();
        
        for (int i = 0; i < brains.Length; i++)
        {
            brains[i].Populate();
            brains[i].Info = $"{i + 1:00}: Errors = {brains[i].ErrorsCount}";
        }

        BrainsComboBox.DataSource = brains;
        BrainsComboBox.DisplayMember = nameof(AiBrain.Info);
        BrainsComboBox.SelectedIndex = brains.Length - 1;
    }

    public void WaitForGo(bool fastSelect = false)
    {
        OptionsPanel.Enabled = true;
        GoCheckBox.Checked = fastSelect;
        MainPanel.Enabled = false;

        while (!GoCheckBox.Checked)
        {
            Thread.Sleep(20);
            Application.DoEvents();
        }

        OptionsPanel.Enabled = false;
        MainPanel.Enabled = true;
    }

    private void FishingTimeoutTimer_Tick(object sender, EventArgs e)
    {
        if (!LookIntoWaterCheckBox.Checked || !WaitPlayerLooksIntoWaterTimer.Enabled)
        {
            TryUseRod();
        }
    }

    private void SignalMultiplierUpDown_ValueChanged(object sender, EventArgs e)
    {
        SignalMultiplier = (float)SignalMultiplierUpDown.Value / 100f;
    }

    private bool PlayerLooksIntoWater()
    {
        var hwnd = Native.GetForegroundWindow();
        
        // check window title
        var windowTitle = Native.GetWindowTitle(hwnd);
        if (windowTitle?.Contains(GameTitleTextBox.Text) != true)
        {
            return false;
        }

        // get screen bitmap
        // ScreenCap can crash sometimes. Check and restart it.
        if (FishingCheckBox.Checked && LookIntoWaterCheckBox.Checked && ScreenCap.IsNotActive)
        {
            ScreenBitmap = null;
            ScreenCap.PreserveBitmap = true;
            ScreenCap.StartCapture((bitmap) => ScreenBitmap = bitmap);

            // wait for first screenshot
            while (ScreenBitmap == null && ScreenCap.IsActive)
            {
                Thread.Sleep(10);
            }
        }

        if (ScreenBitmap == null)
        {
            return false;
        }
        
        // get game picture rect
        const int zoneWidth = 200;
        const int zoneHeight = 200;

        var gameRect = Native.GetClientRect(hwnd);
        if (gameRect == null || gameRect.Value.Width < zoneWidth + 5 || gameRect.Value.Height < zoneHeight + 5) // 5 is just for safety
        {
            return false;
        }

        int zoneX = (int)(gameRect.Value.X + (gameRect.Value.Width / 2) - (zoneWidth / 2));
        int zoneY = (int)(gameRect.Value.Y + (gameRect.Value.Height / 2) - (zoneHeight / 2));

        // collect pixels colors
        var colorValues = new List<Color>();
        int divCount = 50;
        int xStep = zoneWidth / divCount;
        int yStep = zoneHeight / divCount;

        if (xStep > 0 && yStep > 0)
        {
            for (int i = 0; i < divCount; i++)
            {
                colorValues.Add(ScreenBitmap.GetPixel(i * xStep + zoneX, i * yStep + zoneY));
                colorValues.Add(ScreenBitmap.GetPixel(i * xStep + zoneX, zoneY + zoneHeight - (i * yStep)));
            }
        }
        else
        {
            return false;
        }

        // check if blue is dominant color
        int matchesCount = 0;
        foreach (var c in colorValues)
        {
            if (1.5f * c.R < c.B && 1.5f * c.G < c.B)
            {
                matchesCount++;
            }
        }
        
        float result = (float)matchesCount / colorValues.Count;
        //InfoBox.AppendText($"{colorValues.Count},{matchesCount},{result:0.00}\r\n");

        return result >= 0.65f;
    }

    private void WaitPlayerLooksIntoWaterTimer_Tick(object sender, EventArgs e)
    {
        if (PlayerLooksIntoWater())
        {
            var successCount = (int)WaitPlayerLooksIntoWaterTimer.Tag + 1;
            WaitPlayerLooksIntoWaterTimer.Tag = successCount;

            if (successCount >= 3)
            {
                WaitPlayerLooksIntoWaterTimer.Stop();
                FishingTimeoutTimer.Restart();
                StartFishingMonitoring();
            }
        }
    }

    private void EvolutionTimer_Tick(object sender, EventArgs e)
    {
        if (Evolution != null)
        {
            PopulationLabel.Text = $"{Evolution.PopulationPrev} + {Evolution.PopulationTotal - Evolution.PopulationPrev}";
            GenerationLabel.Text = Evolution.Generation.ToString();
            SurvivalsLabel.Text = Evolution.SurvivalsCount.ToString();
            ErrorsCountLabel.Text = Evolution.ErrorsCountPrev < int.MaxValue
                ? $"{Evolution.ErrorsCountPrev} > {Evolution.ErrorsCountPrev - Math.Max(1, (int)(Evolution.ErrorsCountPrev * Evolution.Config.AcceptableErrorsThreshold))} : {Evolution.ErrorsCount}"
                : $"{Evolution.ErrorsCount}";

            if (!Evolution.Running)
            {
                EvolutionTimer.Stop();
                GenerateCheckBox.Checked = false;
                MutateCheckBox.Checked = false;
            }

            if (Evolution.BestMutant != null && BestEvolutionBrain != Evolution.BestMutant)
            {
                BestEvolutionBrain = Evolution.BestMutant;
                if (DiagramForm.Visible)
                {
                    DiagramForm.DrawAi(BestEvolutionBrain);
                }
            }
        }
        else
        {
            EvolutionTimer.Stop();
        }
    }

    private void BrainsComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (BrainsComboBox.SelectedIndex > -1 && BrainsComboBox.SelectedItem != null)
        {
            SelectedBrain = (AiBrain)BrainsComboBox.SelectedItem;
            if (DiagramForm.Visible)
            {
                DiagramForm.DrawAi(SelectedBrain);
            }
        }
        else
        {
            SelectedBrain = null;
        }
    }

    private void SurvivalsLabel_Click(object sender, EventArgs e)
    {
        if (Evolution != null && Evolution.SurvivalsCount > 0 && Evolution.Mutators.Any())
        {
            var groups = Evolution.Mutators
                .SelectMany(m => m.Survivals)
                .GroupBy(m => m.ErrorsCount)
                .OrderByDescending(g => g.Key)
                .Select(g => $"Errors: {g.Key:000}, Count: {g.Count()}")
                .ToList();

            if (groups.Count > 20)
            {
                groups = groups.Skip(groups.Count - 20).ToList();
                groups.Insert(0, "...");
            }

            MessageBox.Show(string.Join("\r\n", groups), $"Survivals: {Evolution.SurvivalsCount}");
        }
    }

    private void MainPanel_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        DiagramForm.Show();
    }

    private void DiagramButton_Click(object sender, EventArgs e)
    {
        if (!DiagramForm.Visible && BestEvolutionBrain != null)
        {
            DiagramForm.Show();
            DiagramForm.DrawAi(BestEvolutionBrain);
        }
    }

    private void BrainsComboBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            if (SelectedBrain != null)
            {
                DiagramForm.Show();
                DiagramForm.DrawAi(SelectedBrain);
            }
        }
    }
}