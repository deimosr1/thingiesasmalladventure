using System.Collections;
using UnityEngine;

public class SStartCutscene : MonoBehaviour
{
    [SerializeField] private STextBoxNonSpecific mTextBox;

    [SerializeField] private string[] mIntro;

    [SerializeField] private SPlayer mPlayer;

    [SerializeField] private SPlayerSpawnLocation mStart;

    public static SStartCutscene CutsceneStartInstance { get; private set; }

    private void Awake()
    {
        if (CutsceneStartInstance != null && CutsceneStartInstance != this)
        {
            Destroy(this.gameObject);
            Debug.Log("Found more than one Start Cutscene Manager in scene.");
        }
        CutsceneStartInstance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        mStart = GameObject.FindGameObjectWithTag("PersistentObject").GetComponent<SPlayerSpawnLocation>();
        if (mStart.firstOpening == true)
        {
            mTextBox = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<STextBoxNonSpecific>();
            mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SPlayer>();
            var controller = mPlayer.gameObject.GetComponent<CharacterController>();
            mPlayer.enabled = false;
            controller.enabled = false;
            StartCoroutine(Intro());
        }

        if (mStart.firstOpening == false)
        {
            mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SPlayer>();
            var controller = mPlayer.gameObject.GetComponent<CharacterController>();
            mPlayer.enabled = true;
            controller.enabled = true;
            mStart.firstOpening = false;
        }
    }

    private IEnumerator Intro()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(mTextBox.ActivateTextBox(mIntro));
        yield return new WaitForSeconds(35.0f);
        var controller = mPlayer.gameObject.GetComponent<CharacterController>();
        mPlayer.enabled = true;
        controller.enabled = true;
        mStart.firstOpening = false;
    }
}
