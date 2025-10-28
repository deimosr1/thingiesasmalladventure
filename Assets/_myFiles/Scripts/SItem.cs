using UnityEngine;

public class SItem : MonoBehaviour, IInteractable
{
    public SItemProfile ItemProfile;

    public float PickupDistance = 5.0f;

    public void OnInteract(SPlayer player)
    {
        float distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        if (distanceFromPlayer <= PickupDistance)
        {
            Debug.Log("Pickup Item!");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, PickupDistance);
    }
}
