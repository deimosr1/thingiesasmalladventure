using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SPlayerSpawnLocation : MonoBehaviour, IDataPersistence
{
    public static SPlayerSpawnLocation Instance;
    public bool hasStarted = true;
    public bool firstOpening = true;
    public ECurrentLevel CurrentLevel;

    public List<STransfer> TransferPoints = new List<STransfer>();

    public Animator TransitionAnim;
    [SerializeField] private GameObject mScreen;

    [SerializeField] private SInventory mInventory;


    // Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
          
        TransferPoints.Clear();
    }

    private void Start()
    {
        TransferPoints.Clear();
        var transferPointsTemp = GameObject.FindObjectsByType<STransfer>(FindObjectsSortMode.None);
        TransferPoints.AddRange(transferPointsTemp);

        CurrentLevel = ECurrentLevel.Town;

        LocateScreen();
        mScreen.SetActive(false);
        mInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<SInventory>();
    }

    public void LoadData(SGameData data)
    {
        this.CurrentLevel = data.LastLevel;
    }

    public void SaveData(ref SGameData data)
    {
        data.LastLevel = this.CurrentLevel;
    }

    public void LoadScene(string levelName)
    {
        StartCoroutine(OnLoadScene(levelName));
    }

    public IEnumerator OnLoadScene(string levelName)
    {
        Debug.Log($"Now loading {levelName}...");
        AsyncOperation loadingLevel = SceneManager.LoadSceneAsync(levelName);

        while (!loadingLevel.isDone)
        {
            TransitionTrue();
            yield return null;
        }

        LocateScreen();
        mInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<SInventory>();
        TransitionFalse();
        Debug.Log($"Loaded: {levelName}!");

        mInventory.SearchingPanels();
        mInventory.UpdateToolUI();
        mInventory.UpdateInventoryUI();
        GrabAndMovePlayer(levelName);
    }

    public void LocateScreen()
    {
        TransitionAnim = GameObject.FindGameObjectWithTag("TransitionAnim").GetComponent<Animator>();
        mScreen = GameObject.FindGameObjectWithTag("TransitionBlock");
    }

    public void TransitionTrue()
    {
        BlockScreen();
        TransitionAnim.SetBool("Transition", true);
    }

    public void TransitionFalse()
    {
        mScreen.SetActive(false);
        TransitionAnim.SetBool("Transition", false);
    }

    public void BlockScreen()
    {
        mScreen.SetActive(true);
    }

    public void GrabAndMovePlayer(string levelName)
    {
        StartCoroutine(OnGrabAndMovePlayer(levelName));
    }

    public IEnumerator OnGrabAndMovePlayer(string levelName)
    {
        // Grab all Transer Points
        TransferPoints.Clear();
        var transferPointsTemp = GameObject.FindObjectsByType<STransfer>(FindObjectsSortMode.None);
        TransferPoints.AddRange(transferPointsTemp);

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<SPlayer>();

        if (player != null)
        {
            // Check Previous Level and set player to entry gate location of previous level
            var point = TransferPoints.First(tPoint => tPoint.LevelEntryID == CurrentLevel);
            switch (CurrentLevel)
            {
                case ECurrentLevel.None:
                    break;
                case ECurrentLevel.Forest:
                    // Find the forest Level Spawn Location
                    point = TransferPoints.First(tPoint => tPoint.LevelEntryID == CurrentLevel);
                    Debug.Log($"{CurrentLevel} is being located");
                    yield return new WaitForSeconds(0.01f);
                    player.enabled = false;
                    player.gameObject.transform.position = point.SpawnLocation.position;
                    Debug.Log($"Moving Player to {point}");
                    yield return new WaitForSeconds(0.1f);
                    player.enabled = true;
                    break;
                case ECurrentLevel.Ruins:
                    // Find the ruins Level Spawn Location
                    point = TransferPoints.First(tPoint => tPoint.LevelEntryID == CurrentLevel);
                    Debug.Log($"{CurrentLevel} is being located");
                    yield return new WaitForSeconds(0.01f);
                    player.enabled = false;
                    player.gameObject.transform.position = point.SpawnLocation.position;
                    Debug.Log($"Moving Player to {point}");
                    yield return new WaitForSeconds(0.1f);
                    player.enabled = true;
                    break;
                case ECurrentLevel.EvilTower:
                    // Find the eviltower Level Spawn Location
                    Debug.Log($"{CurrentLevel} is being located");
                    point = TransferPoints.First(tPoint => tPoint.LevelEntryID == CurrentLevel);
                    yield return new WaitForSeconds(0.01f);
                    player.enabled = false;
                    player.gameObject.transform.position = point.SpawnLocation.position;
                    Debug.Log($"Moving Player to {point}");
                    yield return new WaitForSeconds(0.1f);
                    player.enabled = true;
                    break;
                case ECurrentLevel.Prairie:
                    // Find the prairie Level Spawn Location
                    point = TransferPoints.First(tPoint => tPoint.LevelEntryID == CurrentLevel);
                    Debug.Log($"{CurrentLevel} is being located");
                    yield return new WaitForSeconds(0.01f);
                    player.enabled = false;
                    player.gameObject.transform.position = point.SpawnLocation.position;
                    Debug.Log($"Moving Player to {point}");
                    yield return new WaitForSeconds(0.1f);
                    player.enabled = true;
                    break;
                case ECurrentLevel.Shores:
                    // Find the shores Level Spawn Location
                    point = TransferPoints.First(tPoint => tPoint.LevelEntryID == CurrentLevel);
                    Debug.Log($"{CurrentLevel} is being located");
                    yield return new WaitForSeconds(0.01f);
                    player.enabled = false;
                    player.gameObject.transform.position = point.SpawnLocation.position;
                    Debug.Log($"Moving Player to {point}");
                    yield return new WaitForSeconds(0.1f);
                    player.enabled = true;
                    break;
                case ECurrentLevel.Cliffs:
                    // Find the cliffs Level Spawn Location
                    point = TransferPoints.First(tPoint => tPoint.LevelEntryID == CurrentLevel);
                    Debug.Log($"{CurrentLevel} is being located");
                    yield return new WaitForSeconds(0.01f);
                    player.enabled = false;
                    player.gameObject.transform.position = point.SpawnLocation.position;
                    Debug.Log($"Moving Player to {point}");
                    yield return new WaitForSeconds(0.1f);
                    player.enabled = true;
                    break;
                case ECurrentLevel.Town:
                    // Find the town Level Spawn Location
                    point = TransferPoints.First(tPoint => tPoint.LevelEntryID == CurrentLevel);
                    Debug.Log($"{CurrentLevel} is being located");
                    yield return new WaitForSeconds(0.01f);
                    player.enabled = false;
                    player.gameObject.transform.position = point.SpawnLocation.position;
                    Debug.Log($"Moving Player to {point}");
                    yield return new WaitForSeconds(0.1f);
                    player.enabled = true;
                    break;
            }

            SetCurrentLevel(levelName);
        }

        else { SetCurrentLevel(levelName); }
    }

    public void SetCurrentLevel(string levelName)
    {
        switch (levelName)
        {
            case "TCliffs":
                CurrentLevel = ECurrentLevel.Cliffs; 
                break;
            case "TEvilTower":
                CurrentLevel = ECurrentLevel.EvilTower;
                break;
            case "TForest":
                CurrentLevel = ECurrentLevel.Forest;
                break;
            case "TPrairie":
                CurrentLevel = ECurrentLevel.Prairie;
                break;
            case "TRuins":
                CurrentLevel = ECurrentLevel.Ruins;
                break;
            case "TShores":
                CurrentLevel = ECurrentLevel.Shores;
                break;
            case "TTown":
                CurrentLevel = ECurrentLevel.Town;
                break;
            default:
                CurrentLevel = ECurrentLevel.Town;
                break;
        }
    }

    public void StartGame()
    {
        hasStarted = true;
    }
}

public enum ECurrentLevel
{
    None,
    Forest,
    Ruins,
    EvilTower,
    Prairie,
    Shores,
    Cliffs,
    Town,
}
