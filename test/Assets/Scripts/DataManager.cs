using JetBrains.Annotations;
using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour, IManager
{
    private string _state;
    public string State;
    private string _dataPath;
    private string _textFile
    {
        get { return _state; }
        set { _state = value; }
    }
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        _state = "Data Manager initialized..";
        Debug.Log(_state);

        FilesystemInfo();

        _state = "Data Manager initialized..";
        Debug.Log(_state);
        NewTextFile();

        _state = "Data Manager initialized..";
        Debug.Log(_state);

        UpdateTextFile();

        _state = "Data Manager initialized..";
        Debug.Log(_state);

        ReadFromFile(_textFile);
    }
        public void FilesystemInfo()
    {
        Debug.LogFormat("Path separator character: {0}",Path.PathSeparator);
        Debug.LogFormat("Directory separator character: {0}",
            Path.DirectorySeparatorChar);
        Debug.LogFormat("Current directory: {0}",
           Directory.GetCurrentDirectory());
        Debug.LogFormat("Temporary path: {0}", Path.GetTempPath());
    }
    
    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data/";

        Debug.Log(_dataPath);

        _textFile = _dataPath + "Save_Data.txt";
    }

    public void NewDirectory()
    {
        if (Directory.Exists(_dataPath))
        {
            Debug.Log("Directory already exists...");
            return;
        }
        Directory.CreateDirectory(_dataPath);
        Debug.Log("New directory created!");
        _state = "Data Manager initialized..";
        Debug.Log(_state);
        NewDirectory();
    }

    public void DeleteDirectory()
    {
        if (!Directory.Exists(_dataPath))
        {
            Debug.Log("Directory doesn't exist or has already been deleted...");

            return;
        }
        Directory.Delete(_dataPath, true);
        Debug.Log("Directory successfully deleted!");
    }

    public void NewTextFile()
    {
        if (File.Exists(_textFile))
        {
            Debug.Log("File already exists...");
            return;

        }
        File.WriteAllText(_textFile, "<SAVE DATA>\n");
        Debug.Log("New file created!");
    }

    public void UpdateTextFile()
    {
        if (!File.Exists(_textFile))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        File.AppendAllText(_textFile, $"Game started:{DateTime.Now}\n");
        Debug.Log("File updated successfully!");
    }

    public void ReadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        Debug.Log(File.ReadAllText(filename));
    }

    public void DeleteFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exist or has already been deleted...");

            return;
        }
        File.Delete(_textFile);
        Debug.Log("File successfully deleted!");

    }



}
