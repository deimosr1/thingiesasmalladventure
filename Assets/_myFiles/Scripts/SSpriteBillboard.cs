using UnityEngine;

public class SSpriteBillboard : MonoBehaviour
{
    [SerializeField] bool bFreezeXZAxis = true;

    private void LateUpdate()
    {
        if (bFreezeXZAxis)
        {
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
