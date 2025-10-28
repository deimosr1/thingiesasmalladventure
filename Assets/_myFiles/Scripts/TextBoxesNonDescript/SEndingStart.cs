using UnityEngine;

public class SEndingStart : MonoBehaviour
{
    [SerializeField] private int TotalNPCs;
    [SerializeField] private SPlayer mPlayer;
    [SerializeField] private string mScene;

    private void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mPlayer.PeopleHelped >= TotalNPCs)
            {
                SPlayerSpawnLocation.Instance.LoadScene(mScene);
            }
            else { return; }
        }
    }
}
