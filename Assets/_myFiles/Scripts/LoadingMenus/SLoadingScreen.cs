using System.Collections.Concurrent;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;
using UnityEngine.UI;

public class SLoadingScreen : MonoBehaviour
{
    public Animator TransitionAnim;
    [SerializeField] private GameObject mScreen;

    private void Start()
    {
        
        TransitionAnim = GameObject.FindGameObjectWithTag("TransitionAnim").GetComponent<Animator>();
        mScreen = GameObject.FindGameObjectWithTag("TransitionBlock");
        mScreen.SetActive(true);
        mScreen.SetActive(false);
    }

    public void LocateScreen()
    {
        TransitionAnim = GameObject.FindGameObjectWithTag("TransitionAnim").GetComponent<Animator>();
        mScreen = GameObject.FindGameObjectWithTag("TransitionBlock");
    }

    public void TransitionTrue()
    {
        TransitionAnim.SetBool("Transition", true);
    }

    public void TransitionFalse()
    {
        TransitionAnim.SetBool("Transition", false);
    }

    public void BlockScreen()
    {
        mScreen.SetActive(true);
    }
}
