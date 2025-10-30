using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class SItemManagement : MonoBehaviour
{
    [SerializeField] private SInventory mInventory;

    [SerializeField] private List<GameObject> mItemsInScene = new List<GameObject>();

    private void Start()
    {
        mInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<SInventory>();
        mItemsInScene = GameObject.FindGameObjectsWithTag("ITEM").OfType<GameObject>().ToList();

        if (mInventory.ItemPickedup != null)
        {
            Debug.Log("Pickup not null");
            foreach (SItemProfile item in mInventory.ItemPickedup)
            {
                string itemID = item.ItemID;
                Debug.Log($"Item ID is {itemID}");

                foreach (GameObject groundItem in mItemsInScene)
                {
                    if (groundItem == null){continue;}

                    SPickup it = groundItem.GetComponent<SPickup>();
                    string ID = it.CheckItemProfile();

                    if (ID == itemID)
                    {
                        Debug.Log($"Deleting Item with ID {ID}");
                        Destroy(groundItem);
                    }
                    Debug.Log($"Reached End of Foreach loop of ground Items");
                }
                Debug.Log($"Reached End of Foreach loop");
                mItemsInScene = GameObject.FindGameObjectsWithTag("ITEM").OfType<GameObject>().ToList();
            }
        }
    }
}
