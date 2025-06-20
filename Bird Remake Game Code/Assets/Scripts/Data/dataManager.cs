using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class dataManager : MonoBehaviour
{
    public static dataManager instance { get; private set; }

    [Header("Data File Storage Config")]
    [SerializeField] private string fileName; 

    private gameData saveData;
    private List<IData> dataObjects;
    private dataFileHandler fileHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple Data Managers in scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.fileHandler = new dataFileHandler(Application.persistentDataPath, fileName);
        this.dataObjects = FindAllDataObjects();
        LoadSave();
    }

    public void NewSave()
    {
        this.saveData = new gameData();
    }

    public void WriteSave()
    {
        foreach (IData dataObj in dataObjects)
        {
            dataObj.WriteSave(ref saveData);
        }
        fileHandler.Write(saveData); 
    }

    public void LoadSave()
    {
        this.saveData = fileHandler.Load();
        if (this.saveData == null)
        {
            Debug.Log("No save data found. Using default data");
            NewSave();
        }

        foreach (IData dataObj in dataObjects)
        {
            dataObj.LoadSave(saveData);
        }
    }

    private List<IData> FindAllDataObjects()
    {
        IEnumerable<IData> dataObjects = FindObjectsOfType<MonoBehaviour>().OfType<IData>();
        return new List<IData>(dataObjects);
    }

    private void OnApplicationQuit()
    {
        WriteSave();
    }
}
