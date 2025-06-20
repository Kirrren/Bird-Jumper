using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class dataFileHandler
{
    private string dataDirPath;
    private string dataFileName;

    public dataFileHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public gameData Load()
    {
        string absPath = Path.Combine(dataDirPath, dataFileName);
        gameData loadedData = null;
        if (File.Exists(absPath))
        {
            try
            {
                string dataToLoad;
                using (FileStream stream = new FileStream(absPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<gameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file " + absPath + "\n" + e);
            }
        }
        return loadedData;
        
    }

    public void Write(gameData data)
    {
        string absPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(absPath));
            string dataToWrite = JsonUtility.ToJson(data, true);
            using (FileStream stream = new FileStream(absPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToWrite);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error when saving data to file " + absPath + "\n" + e);
        }
    }
}
