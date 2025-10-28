using System.Collections;
using UnityEngine;

public class SEnding : MonoBehaviour
{
    [SerializeField] private STextBoxNonSpecific mTextBox;

    [SerializeField] private string[] mOutro;

    [SerializeField] private SPlayerSpawnLocation mStart;

    public ECurrentLevel LevelEntryID;
    [SerializeField] private string mScene;

    private void Start()
    {
        mTextBox = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<STextBoxNonSpecific>();
        mStart = GameObject.FindGameObjectWithTag("PersistentObject").GetComponent<SPlayerSpawnLocation>();

        StartCoroutine(Outro());
    }

    private IEnumerator Outro()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(mTextBox.ActivateTextBox(mOutro));
        yield return new WaitForSeconds(31.0f);
        ExitScene();
    }

    private void ExitScene()
    {
        SPlayerSpawnLocation.Instance.LoadScene(mScene);
    }
}
