using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class SDataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string mFileName;
    [SerializeField] private bool bUseEncryption;

    private SGameData mGameData;
    public static SDataPersistenceManager DataInstance { get; private set; }

    private List<IDataPersistence> mDataPersistenceObjects;

    private SFileDataHandler mDataHandler;

    private void Awake()
    {
        if (DataInstance != null && DataInstance != this)
        {
            Destroy(this.gameObject);
            Debug.Log("Found more than one Data Persistence Manager in scene.");
        }
        DataInstance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        mDataHandler = new SFileDataHandler(Application.persistentDataPath, mFileName, bUseEncryption);
        mDataPersistenceObjects = FindAllDataPersistenceObjects();
        Debug.Log(mDataPersistenceObjects);
        //NewGame();
        //LoadGame();
    }
    public void NewGame()
    {
        this.mGameData = new SGameData();
        Debug.Log("Initializing new Game");
        Debug.Log(mGameData);
    }

    public void LoadGame()
    {
        this.mDataPersistenceObjects = FindAllDataPersistenceObjects();
        Debug.Log(mDataPersistenceObjects);
        this.mGameData = this.mDataHandler.Load();
        Debug.Log(mGameData + "  " + this.mDataHandler);

        //TODO: Load any saved data from a file using data handler
        //if no data can be loaded, initialize to a new game

        if (this.mGameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in mDataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(mGameData);
            Debug.Log("Loading game");
        }

        Debug.Log($"Loaded last place is {mGameData.LastLevel}");
    }

    public void Savegame()
    {
        //TODO: Pass the data to other scripts so they can update it.
        //TODO: Save that data to a file using the data handler.
        this.mDataPersistenceObjects = FindAllDataPersistenceObjects();
        Debug.Log(mDataPersistenceObjects);
        Debug.Log(mGameData);
        if (this.mGameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        if (mDataPersistenceObjects != null)
        {
            foreach (IDataPersistence dataPersistenceObj in mDataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(ref mGameData);
                Debug.Log("Saving the game");
            }

            Debug.Log($"Saved last place is {mGameData.LastLevel}");

            this.mDataHandler.Save(mGameData);
        }

        else if (mDataPersistenceObjects == null)
        {
            Debug.Log("Data is null");
        }
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Savegame();
            Debug.Log("Save");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //LoadGame();
            Debug.Log("Load");
        }
    }
}
