using TMPro;
using UnityEngine;
using System.Collections;

public class SSignPost : MonoBehaviour, IInteractable
{

    [SerializeField] private string[] mSignPost;

    [SerializeField] private bool bIsInside = false;


    [Header("Texbox")]
    [SerializeField] private Animator mTextAnimator;
    [SerializeField] private TextMeshProUGUI mTextBox;
    [SerializeField] private float charactersPerSecond = 10.0f;
    [SerializeField] private bool bDuringTalk;
    [SerializeField] private bool bTalking;
    [SerializeField] private int mSignCount;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && bIsInside == true && mSignPost != null)
        {
            bTalking = true;
            var i = mSignCount;
            StartCoroutine(TextBox());
            StartCoroutine(TypeText(mSignPost[i]));
            mSignCount++;

        }

        if (mSignCount >= mSignPost.Length)
        {
            mSignCount = 0;
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
        if (bTalking == true && bIsInside == true)
        {
            mTextAnimator.SetBool("Active", true);
            yield return new WaitForSeconds(1.0f);
        }
        else if (bTalking == false && bIsInside == false)
        {
            mTextAnimator.SetBool("Active", false);
            yield return new WaitForSeconds(1.0f);
        }

    }
    public void OnInteract(SPlayer player)
    {
        if (bIsInside == true && mSignPost != null)
        {
            bTalking = true;
            var i = mSignCount;
            StartCoroutine(TextBox());
            StartCoroutine(TypeText(mSignPost[i]));
            mSignCount++;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bIsInside = true;
        }
    }
    private void OnTriggerExit(Collider other)
    { 
        if (other.gameObject.CompareTag("Player"))
        {
            bDuringTalk = false;
            bIsInside = false;
        }
    }
}
