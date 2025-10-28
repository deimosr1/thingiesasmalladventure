using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class STransfer : MonoBehaviour
{
    public ECurrentLevel LevelEntryID;
    [SerializeField] private string mScene;
    [SerializeField] private string mControls;
    [SerializeField] private string mSettings;
    public Transform SpawnLocation;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            SceneLoading();
        }
        
    }

    public void SceneLoading()
    {
        SPlayerSpawnLocation.Instance.LoadScene(mScene);
    }

    public void IsNewGame()
    {
        SPlayerSpawnLocation.Instance.firstOpening = true;
    }

    public void IsOldGame()
    {
        SPlayerSpawnLocation.Instance.firstOpening = false;
    }

    public void OnControls()
    {
        SPlayerSpawnLocation.Instance.LoadScene(mControls);
    }

    public void OnSettings()
    {
        SPlayerSpawnLocation.Instance.LoadScene(mSettings);
    }
}
