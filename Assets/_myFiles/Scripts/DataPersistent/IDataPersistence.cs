using UnityEngine;

public interface IDataPersistence
{
    void LoadData(SGameData data);

    void SaveData(ref SGameData data);
}
