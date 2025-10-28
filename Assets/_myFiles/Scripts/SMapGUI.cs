using UnityEngine;
using System.Collections;

public class SMapGUI : MonoBehaviour
{
    [SerializeField] private GameObject mMapMenu;
    [SerializeField] private bool bPaused;
    [SerializeField] private Animator mMapAnim;

    [Header("SoundEffects")]
    [SerializeField] private AudioSource mSFX;
    [SerializeField] private AudioClip mActivateSound;


    private void Start()
    {
        mMapAnim = mMapMenu.GetComponent<Animator>();
        mSFX = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }

    private IEnumerator Map()
    {
        if (bPaused == false)
        {
            mSFX.clip = mActivateSound;
            mSFX.Play();
            bPaused = true;
            mMapAnim.SetBool("Active", true);
            yield return new WaitForSeconds(1.0f);
            Paused();
        }
        else if (bPaused == true)
        {
            mSFX.clip = mActivateSound;
            mSFX.Play();
            bPaused = false;
            Paused();
            mMapAnim.SetBool("Active", false);
            yield return new WaitForSeconds(0.0f);
            
        }

    }


    public void MapButton()
    {
        StartCoroutine(Map());
    }

    public void Paused()
    {
        if (bPaused == true)
        {
            Time.timeScale = 0.0f;
            Debug.Log($"Game Paused.");
        }
        else
        {
            Time.timeScale = 1.0f;
            Debug.Log($"Game Unpaused.");
        }
    }
}
