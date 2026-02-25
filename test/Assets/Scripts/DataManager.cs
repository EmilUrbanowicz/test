using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class DataManager : MonoBehaviour, IManager
{
    private string _state;
    public string State;
    private string _dataPath;
    private string _textFile;
    private string _xmlLevelProgress;
    private string _xmlWeapons;
    private string _jsonWeapons;
    private string stream;
    private string _streamingTextFile
    {
        get { return _state; }
        set { _state = value; }
    }
    List<Weapon> weaponInventory = new List<Weapon>
    {
    new Weapon("Sword of Doom", 100),
    new Weapon("Butterfly knives", 25),
    new Weapon("Brass Knuckles", 15),
    };

    string IManager.State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        _state = "Data Manager initialized..";
        Debug.Log(_state);

        WriteToStream(_streamingTextFile);

        _state = "Data Manager initialized..";
        Debug.Log(_state);

        ReadFromStream(_streamingTextFile);

        _state = "Data Manager initialized..";
        Debug.Log(_state);

        WriteToXML(_xmlLevelProgress);

        _state = "Data Manager initialized..";
        Debug.Log(_state);

        SerializeXML();

        _state = "Data Manager initialized..";
        Debug.Log(_state);

        DeserializeXML();

        _state = "Data Manager initialized..";
        Debug.Log(_state);

        SerializeJSON();

        _state = "Data Manager initialized..";
        Debug.Log(_state);

        DeserializeJSON();
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

        _streamingTextFile = _dataPath + "Streaming_Save_Data.txt";

        _xmlLevelProgress = _dataPath + "Progress_Data.xml";

        _xmlWeapons = _dataPath + "WeaponInventory.xml";

        _jsonWeapons = _dataPath + "WeaponJSON.json";
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

    public void WriteToStream(string filename)
    {
        if (!File.Exists(filename))
        {
            StreamWriter newStream = File.CreateText(filename);

            newStream.WriteLine("<Save Data> for HERO BORN \n");
            newStream.Close();
            Debug.Log("New file created with StreamWriter!");
        }

        StreamWriter streamWriter = File.AppendText(filename);

        streamWriter.WriteLine("Game ended: " + DateTime.Now);
        streamWriter.Close();
        Debug.Log("File contents updated with StreamWriter!");
    } 

    public void ReadFromStream(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        StreamReader streamReader = new StreamReader(filename);
        Debug.Log(streamReader.ReadToEnd());
    }

    public void WriteToXML(string filename)
    {
        if (!File.Exists(filename))
        {
            // 2
            FileStream xmlStream = File.Create(filename);

            // 3
            XmlWriter xmlWriter = XmlWriter.Create(xmlStream);

            // 4
            xmlWriter.WriteStartDocument();
            // 5
            xmlWriter.WriteStartElement("level_progress");

            // 6
            for (int i = 1; i < 5; i++)
            {
                xmlWriter.WriteElementString("level", "Level-" + i);
            }

            // 7
            xmlWriter.WriteEndElement();

            // 8
            xmlWriter.Close();
            xmlStream.Close();
        }
    }

    public void SerializeXML()
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Weapon>));

        using (FileStream stream = File.Create(_xmlWeapons))
        {
            xmlSerializer.Serialize(stream, weaponInventory);
        }
    }

    public void DeserializeXML()
    {
        if (File.Exists(_xmlWeapons))
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Weapon>));

            using (FileStream stream = File.OpenRead(_xmlWeapons))
            {
                var weapons =
                (List<Weapon>)xmlSerializer.Deserialize(stream);

                foreach (var weapon in weapons)
                {
                    Debug.LogFormat("Weapon: {0} - Damage: {1}",
                                       weapon.Name, weapon.Damage);
                }
            }
        }
    }

    public void SerializeJSON()
    {
        string jsonString = JsonUtility.ToJson(weaponInventory, true);

        using (StreamWriter stream =
     File.CreateText(_jsonWeapons))
        {
            stream.WriteLine(jsonString);
        }

        WeaponShop shop = new WeaponShop();
        shop.inventory = weaponInventory;

        using (StreamWriter stream = File.CreateText(_jsonWeapons))
        {
            stream.WriteLine(jsonString);
        }

    }

    public void DeserializeJSON()
    {
        using (StreamReader stream = new StreamReader(_jsonWeapons))
        {
            if (File.Exists(_jsonWeapons))
            {
                var jsonString = stream.ReadToEnd();
                var weaponData = JsonUtility.FromJson<WeaponShop>
                 (jsonString);

                foreach (var weapon in weaponData.inventory)
                {
                    Debug.LogFormat("Weapon: {0} - Damage: {1}",
                      weapon.Name, weapon.Damage);
                }
            }
        }


    }




}









