using System.IO;
using Newtonsoft.Json;

namespace McFisher;

internal class Settings
{
    [JsonIgnore]
    private AppForm MainForm = Program.MainForm;
    private const string SettingsFile = "settings.json";

    public string GameTitle { get; set; } = "Better Min";
    public int FishingTimeout { get; set; } = 40;
    public int SignalMultiplier { get; set; } = 100;
    public bool CheckLookIntoWater { get; set; } = true;
    public int FrequenciesBlocksToUse { get; set; } = 1;
    public int ErrorsGoal { get; set; } = 10;
    public int ThreadsCount { get; set; } = 5;
    public int Momentum { get; set; } = 500;
    public int GenerationPopulationLimit { get; set; } = 5000;
    public int GenerationErrorsThreshold { get; set; } = 5;
    public decimal NeuronsMemory { get; set; } = 0.7m;

    public static Settings Load()
    {
        Settings settings;

        if (File.Exists(SettingsFile))
        {
            settings = SettingsFile.ParseJsonFile<Settings>();
            settings.ApplyToAppForm();
        }
        else
        {
            settings = new Settings();
            settings.ApplyToAppForm();
            settings.Save();
        }

        return settings;
    }

    public void Save()
    {
        GameTitle = MainForm.GameTitleTextBox.Text;
        FishingTimeout = MainForm.FishingTimeoutUpDown.IntValue();
        SignalMultiplier = MainForm.SignalMultiplierUpDown.IntValue();
        CheckLookIntoWater = MainForm.LookIntoWaterCheckBox.Checked;
        FrequenciesBlocksToUse = MainForm.BlocksToUseUpDown.IntValue();
        ErrorsGoal = MainForm.ErrorsGoalUpDown.IntValue();
        ThreadsCount = MainForm.ThreadsUpDown.IntValue();
        Momentum = MainForm.MomentumUpDown.IntValue();
        GenerationPopulationLimit = MainForm.GenPopLimitUpDown.IntValue();
        GenerationErrorsThreshold = MainForm.GenErrorsThresholdUpDown.IntValue();
        NeuronsMemory = MainForm.NeuronsMemoryUpDown.Value;

        this.SaveAsJsonPretty(SettingsFile);
    }

    private void ApplyToAppForm()
    {
        MainForm.GameTitleTextBox.Text = GameTitle;
        MainForm.FishingTimeoutUpDown.Value = FishingTimeout;
        MainForm.SignalMultiplierUpDown.Value = SignalMultiplier;
        MainForm.LookIntoWaterCheckBox.Checked = CheckLookIntoWater;
        MainForm.BlocksToUseUpDown.Value = FrequenciesBlocksToUse;
        MainForm.ErrorsGoalUpDown.Value = ErrorsGoal;
        MainForm.ThreadsUpDown.Value = ThreadsCount;
        MainForm.MomentumUpDown.Value = Momentum;
        MainForm.GenPopLimitUpDown.Value = GenerationPopulationLimit;
        MainForm.GenErrorsThresholdUpDown.Value = GenerationErrorsThreshold;
        MainForm.NeuronsMemoryUpDown.Value = NeuronsMemory;
    }
}
