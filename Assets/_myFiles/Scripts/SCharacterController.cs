using UnityEngine;

public class SCharacterController : MonoBehaviour
{
    [SerializeField] private CharacterController mController;
    [SerializeField] private Vector3 mPlayerVelocity;
    [SerializeField] private float mPlayerSpeed = 2.0f;
    [SerializeField] private float mGravity = -9.81f;

    private void Start()
    {
        mController = gameObject.AddComponent<CharacterController>();
    }

    private void Update()
    {
        if (mPlayerVelocity.y < 0)
        {
            mPlayerVelocity.y = 0;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        mController.Move(move * Time.deltaTime * mPlayerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        mPlayerVelocity.y += mGravity * Time.deltaTime;
        mController.Move(mPlayerVelocity * Time.deltaTime);


    }
}
