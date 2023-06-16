using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

class Program
{
    static void Main()
    {
        string sourcePath = "Debug\\";
        string exeFile = "Debug\\Trinity.exe"; // Update with the path to your provided EXE file

        string version = GetFileVersion(exeFile);
        if (string.IsNullOrEmpty(version))
        {
            Console.WriteLine("Failed to retrieve the version from the provided EXE file.");
            return;
        }

        string destinationFolder = $"\\\\stosqlp01101\\databas\\Trinity64\\Trinity64.{version}"; // Update with the path to your destination folder

        // Copy the file to the destination folder
        CopyDirectory(sourcePath, destinationFolder);

        Console.WriteLine("File copied successfully.");
    }

    static void CopyDirectory(string sourceDir, string destinationDir)
    {
        Directory.CreateDirectory(destinationDir);

        foreach (string file in Directory.GetFiles(sourceDir))
        {
            string fileName = Path.GetFileName(file);
            string destinationPath = Path.Combine(destinationDir, fileName);
            File.Copy(file, destinationPath, true);
        }

        foreach (string subdirectory in Directory.GetDirectories(sourceDir))
        {
            string subdirectoryName = Path.GetFileName(subdirectory);
            string destinationSubdirectory = Path.Combine(destinationDir, subdirectoryName);
            CopyDirectory(subdirectory, destinationSubdirectory);
        }
    }


    static string GetFileVersion(string filePath)
    {
        try
        {
            var assemblyName = AssemblyName.GetAssemblyName(filePath); 
            var version = assemblyName.Version;
            var versionInfo = FileVersionInfo.GetVersionInfo(filePath);
            return version.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving assembly version: {ex.Message}");
            return null;
        }
    }
}
