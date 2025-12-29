using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Path to config.cfg in the same folder as the executable
        string exeDir = AppContext.BaseDirectory;
        string configPath = Path.Combine(exeDir, "config.cfg");

        if (!File.Exists(configPath))
        {
            Console.WriteLine("config.cfg not found!");
            return;
        }

        // Read script name from config.cfg (first line)
        string scriptName = File.ReadAllText(configPath).Trim();
        if (string.IsNullOrEmpty(scriptName))
        {
            Console.WriteLine("Script name not found in config.cfg!");
            return;
        }

        // Create a random number generator
        Random random = new Random();

        // Pick a random number between 1 and 5 (inclusive)
        int seconds = random.Next(1, 6);

        Console.WriteLine($"Waiting {seconds} second(s)...");

        // Wait that amount of time
        await Task.Delay(seconds * 1000);

        Console.WriteLine($"Calling {scriptName}...");

        // Run the shell script
        Process.Start(new ProcessStartInfo
        {
            FileName = "bash",            // Use bash to run the script
            Arguments = scriptName,       // Script name from config.cfg
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        });
    }
}