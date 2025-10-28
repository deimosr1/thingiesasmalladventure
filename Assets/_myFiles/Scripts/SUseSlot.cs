using NUnit.Framework.Interfaces;
using UnityEngine;

public class SUseSlot : MonoBehaviour
{

    private SInventory mInventory;
    [Header("SoundEffects")]
    [SerializeField] private AudioSource mSFX;
    [SerializeField] private AudioClip mActivatePress;

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
            Debug.Log("Button Pressed");
            mInventory.DeactivateToolInSlot();
        }
    }
}
