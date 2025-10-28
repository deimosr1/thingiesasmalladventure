using UnityEngine;

public class SInsideManager : MonoBehaviour
{
    private SPlayer mPlayer;

    private void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SPlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC") || other.gameObject.CompareTag("ITEM"))
        {
            mPlayer.IsNearby = true;
        }

        else
        {
            mPlayer.IsNearby = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC") || other.gameObject.CompareTag("ITEM"))
        {
            mPlayer.IsNearby = false;
        }
    }
}
