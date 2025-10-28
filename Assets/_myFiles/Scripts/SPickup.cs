using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private SInventory mInventory;
    [SerializeField] private SPlayer mPlayer;

    [SerializeField] private SItemProfile mItemData;
    [SerializeField] private bool bIsInside = false;

    [Header("Flavour Text")]
    [SerializeField] private string[] mFlavourText;

    [Header("Texbox Info")]
    [SerializeField] private Animator mTextAnimator;
    [SerializeField] private TextMeshProUGUI mTextBox;

    [Header("Talking Variables")]
    [SerializeField] private int mFlavourCount;
    [SerializeField] private float charactersPerSecond = 10.0f;
    [SerializeField] private bool bTalking;
    [SerializeField] private bool bDuringTalk = false;
    [SerializeField] private bool bIsLocated = false;

    [Header("SoundEffects")]
    [SerializeField] private AudioSource mSFX;
    [SerializeField] private AudioClip mGrabbingSound;

    private void Start()
    {
        mInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<SInventory>();
        mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SPlayer>();
        mSFX = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (bIsInside || mInventory.IsStick) && bIsLocated == true)
        {
            ItemGrab();
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            bTalking = false;
            StartCoroutine(TextBox());
        }

        if (mFlavourCount >= mFlavourText.Length)
        {
            mFlavourCount = 0;
        }

        //for (int i = 0; i < mInventory.UISlot.Length; i++)
        //{

        //if (mInventory.bIsFull[i] == false)
        //{
        // mInventory.bIsFull[i] = true;
        // Instantiate(mItemImage, mInventory.UISlot[i].transform, false);
        //  mInventory.ItemNames[i] = mItemName;
        //  mInventory.UIItemNameDisplay[i].text = mItemName;
        //    Debug.Log($"Item Collected. Item is {mItemName}.)");
        //     Destroy(this.gameObject);
        //     break;
        // }

        //}

        else {  return; }



    }

    public void OnInteract(SPlayer player)
    {
        if (bIsInside || mInventory.IsStick)
        {
            mSFX.clip = mGrabbingSound;
            mSFX.Play();
            ItemGrab();
        }

        else if (!bDuringTalk)
        {
            bTalking = true;
            var i = mFlavourCount;
            StartCoroutine(TextBox());
            StartCoroutine(TypeText(mFlavourText[i]));
            mFlavourCount++;
            bIsLocated = true;
        }
    }
    private void ItemGrab()
    {
        if (mItemData != null)
        {
            mInventory.AddItemOrTool(mItemData);
        }
        Destroy(this.gameObject);
    }

    IEnumerator TypeText(string line)
    {
        string textBuffer = null;
        foreach (char c in line)
        {
            textBuffer += c;
            mTextBox.text = textBuffer;
            yield return new WaitForSeconds(1 / charactersPerSecond);
        }
    }

    private IEnumerator TextBox()
    {
        if (bTalking == true && bIsInside == false)
        {
            mTextAnimator.SetBool("Active", true);
            bDuringTalk = true;
            yield return new WaitForSeconds(1.0f);
        }
        else if (bTalking == false)
        {
            mTextAnimator.SetBool("Active", false);
            bDuringTalk = false;
            yield return new WaitForSeconds(1.0f);
        }

    }

    public string CheckItemProfile()
    {
        string ID = mItemData.ItemID;
        return ID;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bIsInside = true;
            mPlayer.IsNearby = bIsInside;
            Debug.Log($"Inside Collider.)");
            bIsLocated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bIsInside = false;
            mPlayer.IsNearby = bIsInside;
            Debug.Log($"Outside Collider.)");
        }
    }
}
