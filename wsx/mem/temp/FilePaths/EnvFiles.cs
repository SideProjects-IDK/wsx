using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

using static wsx.io.o.std;

namespace wsx.mem.temp.FilePaths
{
    public class EnvFiles
    {
        // Base directory for user-specific environment files
        public static readonly string UserHomeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        // Paths for output history and the current output file
        public static readonly string EnvHistoryWriteActionsDir = Path.Combine(UserHomeDir, $".opensource_wsx_{Program.__Version__}", "iostream", "output_history");
        public static readonly string CurrentOutputFile = Path.Combine(EnvHistoryWriteActionsDir, "history.json");

        /// <summary>
        /// Ensures the existence of required directories and files for output history.
        /// </summary>
        public static bool EnsureEnvFilesForOutput()
        {
            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(EnvHistoryWriteActionsDir))
                {
                    println($"Creating directory: `{EnvHistoryWriteActionsDir}` (Directory did not exist!)");
                    Directory.CreateDirectory(EnvHistoryWriteActionsDir);
                }

                // Ensure the output file exists
                if (!File.Exists(CurrentOutputFile))
                {
                    println($"Creating file: `{CurrentOutputFile}` (File did not exist!)");
                    File.WriteAllText(CurrentOutputFile, "{}"); // Initialize with an empty JSON object
                }

                return true;
            }
            catch (Exception ex)
            {
                println($"Error ensuring environment files: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Saves all environment-related output data to the current output file.
        /// </summary>
        public static bool SaveAllEnvOutputData()
        {
            try
            {
                // Ensure environment files are ready
                if (!EnsureEnvFilesForOutput())
                {
                    println("Failed to ensure environment files.");
                    return false;
                }

                // Serialize the WriteHistory and save to the file
                string json = JsonSerializer.Serialize(wsx.io.o.std.WriteHistory, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(CurrentOutputFile, json);

                println("Environment data saved successfully.");
                return true;
            }
            catch (Exception ex)
            {
                println($"Error saving environment data: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Loads all environment-related output data from the current output file.
        /// </summary>
        public static bool LoadAllEnvOutputData()
        {
            try
            {
                // Check if the file exists before attempting to load
                if (!File.Exists(CurrentOutputFile))
                {
                    println($"Output file not found: `{CurrentOutputFile}`. Nothing to load.");
                    return false;
                }

                // Read and deserialize the file content
                string json = File.ReadAllText(CurrentOutputFile);
                wsx.io.o.std.WriteHistory = JsonSerializer.Deserialize<List<ActionWrite>>(json) ?? new List<ActionWrite>();

                println("Environment data loaded successfully.");
                return true;
            }
            catch (Exception ex)
            {
                println($"Error loading environment data: {ex.Message}");
                return false;
            }
        }
    }
}
