using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SNPC : MonoBehaviour, IInteractable
{
    [Header("NPC Stats")]
    [SerializeField] private string mNPCName;
    [SerializeField] private List<SItemProfile> mRequiredItems = new List<SItemProfile>();
    [SerializeField] private List<SItemProfile> mItemsGiven = new List<SItemProfile>();
    [SerializeField] private SInventory mInventory;
    [SerializeField] private SPlayer mPlayer;

    [Header("Dialogue Text")]
    [SerializeField] private string[] mDialogue;
    [SerializeField] private string[] mFlavourText;
    [SerializeField] private string mItemMissing;

    [Header("Texbox Info")]
    [SerializeField] private Animator mTextAnimator;
    [SerializeField] private TextMeshProUGUI mTextBox;

    [Header("Talking Variables")]
    [SerializeField] private int mInteractedCount;
    [SerializeField] private int mFlavourCount;
    [SerializeField] private float charactersPerSecond = 10.0f;
    [SerializeField] private bool bTalking;
    [SerializeField] private bool bIsInside = false;
    [SerializeField] private bool bHasRecieved = false;
    [SerializeField] private bool bHasGiven = false;
    [SerializeField] private bool bDuringTalk = false;

    [Header("SoundEffects")]
    [SerializeField] private AudioSource mSFX;
    [SerializeField] private AudioClip mInteractSound;


    private void Start()
    {
        mInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<SInventory>();
        mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SPlayer>();
        mSFX = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) && bIsInside && mInteractedCount < mDialogue.Length && bDuringTalk == false) || ((Input.GetMouseButtonDown(0)) && bIsInside && mInteractedCount < mDialogue.Length && bDuringTalk == false))
        {
            mSFX.clip = mInteractSound;
            mSFX.Play();
            bTalking = true;
            var i = mInteractedCount;
            StartCoroutine(TextBox());
            StartCoroutine(TypeText(mDialogue[i]));
            mInteractedCount++;
        }

        if (Input.GetMouseButtonDown(1))
        {
            bTalking = false;
            StartCoroutine(TextBox());
        }

        if (mInteractedCount >= mDialogue.Length && bHasRecieved == true)
        {
            mInteractedCount -= 1;
        }
        else if (mInteractedCount >= mDialogue.Length)
        {
            mInteractedCount = 0;
        }

        if (mFlavourCount >= mFlavourText.Length)
        {
            mFlavourCount = 0;
        }
    }

    IEnumerator TypeText(string line)
    {
        string textBuffer = null;
        foreach (char c in line)
        {
            textBuffer += c;

            if (line.Contains("<Take>"))
            {
                line = line.Replace("<Take>", " ");
                //TakeItem
                if (mRequiredItems != null)
                {
                    for (int i = 0; i < mRequiredItems.Count; ++i)
                    {
                        if (mInventory.Items.Contains(mRequiredItems[i]))
                        {
                            mInventory.RemoveItemOrTool(mRequiredItems[i]);
                            //bHasRecieved = true;
                            mPlayer.PeopleHelped++;

                            if (mItemsGiven[i] != null)
                            {
                                mInventory.AddItemOrTool(mItemsGiven[i]);
                                bHasGiven = true;
                            }
                        }

                        //else if (!mInventory.Items.Contains(mRequiredItems[i]))
                        //{
                            //Debug.LogWarning($"Inventory does not contain {mRequiredItems[i]}");
                            //StartCoroutine(TextBox());
                            //StartCoroutine(TypeText(mItemMissing));
                        //}

                    }

                }
            }
            else if (!line.Contains("<Take>") && line.Contains("<Give>"))
            {
                line = line.Replace("<Give>", " ");
                if (mItemsGiven != null)
                {
                    foreach (var item in mItemsGiven)
                    {
                        //TODO: Item is Given too many times.
                        mInventory.AddItemOrTool(item);
                        Debug.Log("ItemGiven");
                        
                    }

                    mItemsGiven.RemoveAt(0);
                }
            }

            mTextBox.text = textBuffer;
            bDuringTalk = true;
            yield return new WaitForSeconds(1 / charactersPerSecond);
            bDuringTalk = false;

        }

        
    }

    private IEnumerator TextBox()
    {
        if (bTalking == true)
        {
            mTextAnimator.SetBool("Active", true);
            yield return new WaitForSeconds(1.0f);
        }
        else if (bTalking == false && bIsInside == false)
        {
            mTextAnimator.SetBool("Active", false);
            yield return new WaitForSeconds(1.0f);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bIsInside = true;
            mPlayer.IsNearby = bIsInside;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TextBox());
            bTalking = false;
            bIsInside = false;
            mPlayer.IsNearby = bIsInside;
            mInteractedCount = 0;
        }

    }

    public void OnInteract(SPlayer player)
    {
        if (bIsInside && mInteractedCount < mDialogue.Length)
        {
            mSFX.clip = mInteractSound;
            mSFX.Play();
            bTalking = true;
            var i = mInteractedCount;
            StartCoroutine(TextBox());
            StartCoroutine(TypeText(mDialogue[i]));
            mInteractedCount++;
        }

        else
        {
            bTalking = true;
            var i = mFlavourCount;
            StartCoroutine(TextBox());
            StartCoroutine(TypeText(mFlavourText[i]));
            mFlavourCount++;
        }
    }
}
