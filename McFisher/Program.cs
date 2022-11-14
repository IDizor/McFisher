using System.Diagnostics;

namespace McFisher;

internal static class Program
{
    public static AppForm MainForm;
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        MainForm = new AppForm();
        Application.Run(MainForm);
    }
}