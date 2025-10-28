using UnityEngine;
using UnityEngine.SceneManagement;

public class SMainMenu : MonoBehaviour
{

    [SerializeField] private string mControls;
    [SerializeField] private string mSettings;
    [SerializeField] private SDataPersistenceManager mManager;
    [SerializeField] private SPlayerSpawnLocation mTransfer;

    private void Start()
    {
        mManager = GameObject.FindGameObjectWithTag("DataManager").GetComponent<SDataPersistenceManager>();
        mTransfer = SPlayerSpawnLocation.Instance;
    }
    public void OnNewGame()
    {
       mManager.NewGame();
    }

    public void OnLoad()
    {
        mManager.LoadGame();

    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
