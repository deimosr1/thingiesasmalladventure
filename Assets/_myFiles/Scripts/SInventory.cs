using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System.Collections;

public class SInventory : MonoBehaviour, IDataPersistence
{
    //[Header("Old")]
    //public GameObject[] UISlot;
    //public GameObject[] UIToolSlot;
    //public GameObject UIActiveSlot;
    //public string[] ItemNames;
    //public TextMeshProUGUI[] UIItemNameDisplay;
    //public string[] ToolNames;
    //public TextMeshProUGUI[] UIToolNameDisplay;
    //public string ActiveName;

    [Header("New")]
    public bool bIsFull;
    public bool bToolIsFull;
    public bool bActiveIsFull;

    public GameObject UIInventoryPanelItems;
    public GameObject UIInventoryPanelTools;
    public GameObject UIUseItemSquare;

    public GameObject UIItemImage;
    public GameObject UIToolImage;
    public GameObject UIUseItemImage;
    // TODO: Item Icon Prefab to Spawn

    /// <summary>
    /// //////////////////////////////////////////////
    /// </summary>

    public static SInventory Instance;
    public SItemProfile UseSlot;
    public List<SItemProfile> Items = new List<SItemProfile>();
    public List<SItemProfile> ItemPickedup = new List<SItemProfile>();
    public List<SItemProfile> Tools = new List<SItemProfile>();
    public List<Texture2D> ToolCursors = new List<Texture2D>();
    public int CurrentItemsAdded = 0;
    public int CurrentToolsAdded = 0;
    public int MaxItemAmount = 12;
    public int MaxToolAmount = 3;

    public int toolSlotIndex = 0;

    [Header("ToolsBools")]
    public bool IsSword = false;
    public bool IsStick = false;
    public bool IsSpyglass = false;


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
    }

    public void SearchingPanels()
    {
        UIInventoryPanelItems = GameObject.FindGameObjectWithTag("ItemsPanel");
        UIInventoryPanelTools = GameObject.FindGameObjectWithTag("ToolsPanel");
        UIUseItemSquare = GameObject.FindGameObjectWithTag("UsePanel");
        Debug.Log("Finding...");
    }

    public bool AddItemOrTool(SItemProfile itemProfile)
    {
        if (CurrentItemsAdded >= MaxItemAmount || CurrentToolsAdded >= MaxToolAmount) { return false; }

        if (itemProfile != null && itemProfile.ItemType == EItemType.Item)
        {
            Items.Add(itemProfile);
            ItemPickedup.Add(itemProfile);
            ++CurrentItemsAdded;
            UpdateInventoryUI();
            return true;
        }
        else if(itemProfile != null && itemProfile.ItemType == EItemType.Tool)
        {
            Tools.Add(itemProfile);
            ++CurrentToolsAdded;
            UpdateToolUI();
            return true;
        }

        return false;
    }

    public bool RemoveItemOrTool(SItemProfile itemProfile)
    {
        var itemToRemove = Items.SingleOrDefault(item => item.ItemID == itemProfile.ItemID);

        if (itemProfile.ItemType == EItemType.Item && itemToRemove.ItemID != string.Empty)
        {
            Items.Remove(itemToRemove);
            --CurrentItemsAdded;
            UpdateInventoryUI();
            return true;
        }
        else if (itemProfile.ItemType == EItemType.Tool && itemToRemove.ItemID != string.Empty)
        {
            Tools.Remove(itemToRemove);
            --CurrentItemsAdded;
            UpdateToolUI();
            return true;
        }

        return false;
    }

    public void UseToolInSlot(SItemProfile Item)
    {

        Transform allChildren = UIUseItemSquare.GetComponent<Transform>();
        foreach (Transform child in allChildren)
        {
            Destroy(child.gameObject);
            Debug.Log(child.name);
            Debug.Log("All Items Deleted.");
        }

        GameObject tooluseObject = Instantiate(UIUseItemImage, UIUseItemSquare.gameObject.transform, false);
        tooluseObject.GetComponent<Image>().sprite = Item.ItemIcon;
        SPlayer mouseScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SPlayer>();
        mouseScript.SetUpCursor(Item);
        Debug.Log("Use generated");
        // TODO: clean up previous children

    }

    public void DeactivateToolInSlot()
    {

        SPlayer mouseScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SPlayer>();
        mouseScript.GetDefaultCursor();

        IsStick = false;
        IsSword = false;
        IsSpyglass = false;

        Transform allChildren = UIUseItemSquare.GetComponent<Transform>();
        foreach (Transform child in allChildren)
        {
            Destroy(child.gameObject);
            Debug.Log(child.name);
            Debug.Log("All Items Deleted.");
        }
    }

    public void UpdateInventoryUI()
    {
        Transform allChildren = UIInventoryPanelItems.GetComponent<Transform>();
        foreach (Transform child in allChildren)
        {
            Destroy(child.gameObject);
            Debug.Log(child.name);
            Debug.Log("All Items Deleted.");
        }

        //yield return new WaitForSeconds(1.0f);

        foreach (var item in Items)
        {
            //TODO: Items Spawning with same Icon no matter what.
            GameObject itemObject = Instantiate(UIItemImage, UIInventoryPanelItems.gameObject.transform, false);
            itemObject.GetComponent<Image>().sprite = item.ItemIcon;
            Debug.Log("Item Generated.");
        }
    }

    public void UpdateToolUI()
    {
        Transform allChildren = UIInventoryPanelTools.GetComponent<Transform>();
        foreach (Transform child in allChildren)
        {
            Destroy(child.gameObject);
            Debug.Log(child.name);
            Debug.Log("All Tools Deleted.");
        }

        //yield return new WaitForSeconds(1.0f);
        
        foreach (var tool in Tools)
        {
            //TODO: Spawning with same Icon no matter what
            GameObject toolObject = Instantiate(UIToolImage, UIInventoryPanelTools.gameObject.transform, false);
            toolObject.GetComponent<Image>().sprite = tool.ItemIcon;
            toolObject.GetComponent<SInventorySlot>().SlotIndex = toolSlotIndex;
            toolObject.GetComponent<SInventorySlot>().ItemData = tool;
            toolSlotIndex++;
            Debug.Log("ToolGenerated");
        }
    }

    public void LoadData(SGameData data)
    {
        Items.Clear();
        Tools.Clear();
        ItemPickedup.Clear();

        Items = data.ItemData;
        Tools = data.ToolData;

        Debug.Log($"{data.ItemData} and {data.ToolData} and {data.ItemPickedData}");

        ItemPickedup = data.ItemPickedData;

        UpdateInventoryUI();
        UpdateToolUI();
    }

    public void SaveData(ref SGameData data)
    {
        //data.ItemData.Clear();
        //data.ToolData.Clear();
        //data.ItemPickedData.Clear();

        data.ItemData = Items;
        data.ToolData = Tools;

        data.ItemPickedData = ItemPickedup;
    }

    /*
    public void AddItemOrToolToUseSlot(SItemProfile)
    {

    }

    public void RemoveItemOrToolToUseSlot()
    {

    }
    */
}
