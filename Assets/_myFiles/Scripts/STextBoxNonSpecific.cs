using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class STextBoxNonSpecific : MonoBehaviour
{
    [Header("Texbox")]
    [SerializeField] private Animator mTextAnimator;
    [SerializeField] private TextMeshProUGUI mTextBox;
    public bool DuringTalk = false;
    [SerializeField] private bool bTalking = false;
    [SerializeField] private float charactersPerSecond = 10.0f;
    [SerializeField] private int mInteractedCount = 0;

    IEnumerator TypeText(string line)
    {
        string textBuffer = null;
        foreach (char c in line)
        {
            textBuffer += c;
            mTextBox.text = textBuffer;
            yield return new WaitForSeconds(1 / charactersPerSecond);
        }

        yield return new WaitForSeconds(1.0f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            bTalking = false;
            StartCoroutine(TextBox());
        }

        
    }

    private IEnumerator TextBox()
    {
        if (bTalking == true)
        {
            mTextAnimator.SetBool("Active", true);
            yield return new WaitForSeconds(1.0f);
        }
        else if (bTalking == false)
        {
            mTextAnimator.SetBool("Active", false);
            yield return new WaitForSeconds(1.0f);
        }

    }

    public IEnumerator ActivateTextBox(string[] dialogue)
    {

        //foreach (string d in dialogue)
        //{

        //}
        for (int i = mInteractedCount; i < dialogue.Length; ++i)
        {
            bTalking = true;
            StartCoroutine(TextBox());
            StartCoroutine(TypeText(dialogue[i]));
            yield return new WaitForSeconds(5.0f);
            mInteractedCount++;
        }

            

        if (mInteractedCount >= dialogue.Length && bTalking == false)
        {
            mInteractedCount = 0;
        }
    }
}
