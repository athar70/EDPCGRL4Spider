using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Logger 
{
    // Creates a text file at the specified path if it does not already exist
    public void CreateText(string path) 
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, string.Empty); // Write an empty string to create the file
        }
    }

    // Writes a timestamped title to the log file
    public void WriteTitle(string path)
    {
        string timestamp = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        File.AppendAllText(path, timestamp + "\n"); // Append the timestamp to the file
    }

    // Writes a value to the log file with a new line
    public void WriteValue(string path, string value)
    {
        File.AppendAllText(path, value + "\n"); // Append the value to the file
    }
}
