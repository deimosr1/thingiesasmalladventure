using UnityEngine;

public class SInventorySlot : MonoBehaviour
{
    private SInventory mInventory;
    public int SlotIndex = 0;
    public SItemProfile ItemData;
    [SerializeField] private SItemProfile DataItem;

    [Header("SoundEffects")]
    [SerializeField] private AudioSource mSFX;
    [SerializeField] private AudioClip mActivatePress;
    public void InitializeInventorySlot(SInventory inventory, int slotIndex)
    {
        mInventory = inventory;
        SlotIndex = slotIndex;
    }

    private void Start()
    {
        mInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<SInventory>();
        mSFX = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();

    }
    public void OnButtonPress()
    {

        Debug.Log(mInventory);

        if (mInventory != null)
        {
            mSFX.clip = mActivatePress;
            mSFX.Play();
            DataItem = ItemData;
            Debug.Log("Button Pressed");
            Debug.Log(ItemData);
            mInventory.UseToolInSlot(DataItem);

            switch (DataItem.ToolType)
            {
                case EToolType.Sword:
                    mInventory.IsSword = true;
                    mInventory.IsStick = false;
                    mInventory.IsSpyglass = false;
                    break;

                case EToolType.Stick:
            
                    mInventory.IsSword = false;
                    mInventory.IsStick = true;
                    mInventory.IsSpyglass = false;
                    break;

                case EToolType.Spyglass:
           
                    mInventory.IsSword = false;
                    mInventory.IsStick = false;
                    mInventory.IsSpyglass = true;
                    break;
            }
        }
    }
}
