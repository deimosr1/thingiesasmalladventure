using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SSavePoint : MonoBehaviour, IInteractable
{
    [Header("Texbox Info")]
    [SerializeField] private Animator mSaveAnimator;
    [SerializeField] private string mScene;
    [SerializeField] private SInventoryMenu mPause;
    [SerializeField] private bool bIsSaving = false;
    [SerializeField] private SDataPersistenceManager mDataSave;
    [SerializeField] private SPlayerSpawnLocation mTransfer;


    [Header("SoundEffects")]
    [SerializeField] private AudioSource mSFX;
    [SerializeField] private AudioClip mSaveSound;

    private void Start()
    {
        mPause = GameObject.FindGameObjectWithTag("GameController").GetComponent<SInventoryMenu>();
        mSFX = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        mDataSave = GameObject.FindGameObjectWithTag("DataManager").GetComponent<SDataPersistenceManager>();
        mTransfer = GameObject.FindGameObjectWithTag("PersistentObject").GetComponent<SPlayerSpawnLocation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && bIsSaving)
        {
            StartCoroutine(TextBoxActive());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            bIsSaving = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bIsSaving = false;
        }
    }

    public void OnSavePress()
    {
        mDataSave.Savegame();
        mSFX.clip = mSaveSound;
        mSFX.Play();
    }

    public void OnSaveQuit()
    {
        mPause.bPaused = false;
        mSFX.clip = mSaveSound;
        mSFX.Play();
        StartCoroutine(TextBoxDeActive());
        mDataSave.Savegame();
        SPlayerSpawnLocation.Instance.LoadScene(mScene);
        Debug.Log($"Now loading {mScene}.");
    }

    public void OnContinue()
    {
        mPause.bPaused = false;
        StartCoroutine(TextBoxDeActive());
    }
    private IEnumerator TextBoxActive()
    {
        mSFX.clip = mPause.ActivateSound;
        mSFX.Play();
        mSaveAnimator.SetBool("Active", true);
        mPause.bPaused = true;
        yield return new WaitForSeconds(1.5f);
        mPause.Paused();

    }

    private IEnumerator TextBoxDeActive()
    {
        mSFX.clip = mPause.ActivateSound;
        mSFX.Play();
        mSaveAnimator.SetBool("Active", false);
        mPause.Paused();
        yield return new WaitForSeconds(1.0f);

    }

    public void OnInteract(SPlayer player)
    {
        if (bIsSaving)
        {
            mPause.bPaused = true;
            StartCoroutine(TextBoxActive());
        }
    }
}
