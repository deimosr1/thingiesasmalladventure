using UnityEngine;

public class SSwordInteract : MonoBehaviour, IInteractable
{
    private SInventory mInventory;
    private STextBoxNonSpecific mTextBox;

    [SerializeField] private string[] mNoSword;

    [Header("SoundEffects")]
    [SerializeField] private AudioSource mSFX;
    [SerializeField] private AudioClip mDestroySound;

    private void Start()
    {
        mInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<SInventory>();
        mTextBox = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<STextBoxNonSpecific>();
        mSFX = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }

    public void OnInteract(SPlayer player)
    {
        if (mInventory.IsSword)
        {
            Destroy(this.gameObject, 1.0f);
        }

        else
        {
            if(mNoSword != null && mTextBox.DuringTalk == false)
            {
                StartCoroutine(mTextBox.ActivateTextBox(mNoSword));
            }
                
        }
    }
}
