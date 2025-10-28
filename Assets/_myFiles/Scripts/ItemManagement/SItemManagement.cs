using System.Collections.Generic;
using UnityEngine;

public class SItemManagement : MonoBehaviour
{
    [SerializeField] private SInventory mInventory;

    private void Start()
    {
        mInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<SInventory>();

        if (mInventory.ItemPickedup != null)
        {
            Debug.Log("Pickup not null");
            foreach (SItemProfile item in mInventory.ItemPickedup)
            {
                string itemID = item.ItemID;
                var it = GameObject.FindGameObjectWithTag("ITEM").GetComponentInChildren<SPickup>();
                string ID = it.CheckItemProfile();
                Debug.Log($"Item ID is {itemID}");
                Debug.Log(it);
                Debug.Log(ID);

                if (ID == itemID)
                {
                    Debug.Log($"Deleting Item with ID {ID}");
                    Destroy(it.gameObject);
                }

                else
                {
                    Destroy(it.gameObject);
                }
            }
        }
    }
}
