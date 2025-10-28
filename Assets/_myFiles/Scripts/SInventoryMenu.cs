using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SInventoryMenu : MonoBehaviour
{
    private SInventory mInventory;

    [SerializeField] private GameObject mInventoryMenu;
    public bool bPaused;
    [SerializeField] private string mScene;
    [SerializeField] private bool mPlayerHasEnded = false;
    [SerializeField] private string mNextLevel;
    [SerializeField] private Animator mInventoryAnim;

    [Header("SoundEffects")]
    [SerializeField] private AudioSource mSFX;
    public AudioClip ActivateSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bPaused = false;
        mPlayerHasEnded = false;
        mInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<SInventory>();
        mSFX = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        mInventoryAnim = mInventoryMenu.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && bPaused == false && mPlayerHasEnded == false)
        {
            StartCoroutine(Pausing());
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && bPaused == true && mPlayerHasEnded == false)
        {
            StartCoroutine(Pausing());
        }
    }
    private IEnumerator Pausing()
    {
        if (bPaused == false && mPlayerHasEnded == false)
        {
            mSFX.clip = ActivateSound;
            mSFX.Play();
            bPaused = true;
            mInventoryAnim.SetBool("Active", true);
            yield return new WaitForSeconds(1.0f);
            Paused();
        }
        else if (bPaused == true && mPlayerHasEnded == false)
        {
            mSFX.clip = ActivateSound;
            mSFX.Play();
            bPaused = false;
            Paused();
            yield return new WaitForSeconds(0.0f);
            mInventoryAnim.SetBool("Active", false);
        }

    }

    public void PauseButton()
    {
        StartCoroutine(Pausing());
    }

    public void Paused()
    {
        if (bPaused == true)
        {
            Time.timeScale = 0.0f;
            Debug.Log($"Game Paused.");
        }
        else
        {
            Time.timeScale = 1.0f;
            Debug.Log($"Game Unpaused.");
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log($"Now Exiting Game.");
    }

    public void MainMenu()
    {
        //Temporary before creating Save points
        //SDataPersistenceManager.DataInstance.Savegame();
        bPaused = false;
        Paused();
        SPlayerSpawnLocation.Instance.LoadScene(mScene);
        Debug.Log($"Now loading {mScene}.");
    }

    public void Conditions(bool endedGame)
    {

        mPlayerHasEnded = endedGame;

    }

    //public void SetActive()
    //{
    //    if (mInventory.bActiveIsFull == false)
    //    {
    //        mInventory.bIsFull[i] = true;
    //        Instantiate(mItemImage, mInventory.UIActiveSlot.transform, false);
    //        mInventory.ActiveName;
    //    }
    //}
}
