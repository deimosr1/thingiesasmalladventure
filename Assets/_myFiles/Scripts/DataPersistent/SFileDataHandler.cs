using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class SFileDataHandler 
{
    private string mDataDirPath = "";

    private string mDataFileName = "";

    private bool bUseEncryption = false;
    private readonly string mEncryptionCodeword = "Pit";

    public SFileDataHandler(string mDataDirPath, string mDataFileName, bool bUseEncryption)
    {
        this.mDataDirPath = mDataDirPath;
        this.mDataFileName = mDataFileName;
        this.bUseEncryption = bUseEncryption;
    }

    public SGameData Load()
    {
        string fullPath = Path.Combine(mDataDirPath, mDataFileName);
        SGameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (bUseEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                //deserialize the data from JSON to C#
                loadedData = JsonUtility.FromJson<SGameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured when trying to load data from file: {fullPath} \n {e}");
            }
        }
        return loadedData;
    }

    public void Save(SGameData data)
    {
        string fullPath = Path.Combine(mDataDirPath, mDataFileName);
        try
        {
            //Creates directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize C# game Data to Json
            string dataToStore = JsonUtility.ToJson(data, true);

            if (bUseEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }
            //write the data to file

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

            Debug.Log(fullPath);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error ocurred when trying to save data to file: {fullPath} \n {e}.");
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ mEncryptionCodeword[i % mEncryptionCodeword.Length]);
        }
        return modifiedData;
    }
}
