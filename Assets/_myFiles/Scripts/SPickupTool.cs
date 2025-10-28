using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SPickupTool : MonoBehaviour, IInteractable
{
    [SerializeField] private SInventory mInventory;

    [SerializeField] private SItemProfile mToolData;
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

    private void Start()
    {
        mInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<SInventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && bIsInside)
        {
            ToolGrab();
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

        else { return; }
    }

    public void OnInteract(SPlayer player)
    {
        if (bIsInside)
        {
            ToolGrab();
        }

        else if (mFlavourText.Length > 0)
        {
            bTalking = true;
            var i = mFlavourCount;
            StartCoroutine(TextBox());
            StartCoroutine(TypeText(mFlavourText[i]));
            mFlavourCount++;
        }
    }
    private void ToolGrab()
    {

        if (mToolData != null)
        {
            if (mInventory.AddItemOrTool(mToolData))
            {
                Destroy(this.gameObject);
            }
            else
            {
                // Tell the player the inventory is full
            }
        }
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
            yield return new WaitForSeconds(1.0f);
        }
        else if (bTalking == false)
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
            Debug.Log($"Inside Collider.)");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bIsInside = false;
            Debug.Log($"Outside Collider.)");
        }
    }
}
