using UnityEngine;

public class SPlayer : MonoBehaviour, IDataPersistence
{
    [Header("WASD Movement")]
    [SerializeField] private CharacterController mController;
    [SerializeField] private Vector3 mPlayerVelocity;
    [SerializeField] private float mPlayerSpeed = 15.0f;
    [SerializeField] private float mSetSpeed = 15.0f;
    [SerializeField] private float mSprintSpeed = 5.0f;
    [SerializeField] private float mGravity = -9.81f;
    [SerializeField] private float mJumpPower = 3.0f;

    [Header("Interaction System")]
    public bool bCanInteract = true;
    private Camera mCamera;
    [SerializeField] private float InteractRange = 100.0f;
    [SerializeField] private float MinInteractRange = 100.0f;
    public bool IsNearby = false;

    [Header("CharacterSprites")]
    [SerializeField] private bool bIsFacingRight = true;
    [SerializeField] private bool bIsFacingCamera = true;
    [SerializeField] private Animator mAnimator;

    // TODO: Get connection to inventory
    [Header("InventoryManagement")]
    [SerializeField] private SInventory mInventory;
    public int PeopleHelped = 0;

    [Header("MouseCursors")]
    public Texture2D DefaultCursor;
    public Texture2D NpcCursor;
    public Texture2D ItemCursor;
    public Texture2D FlavourCursor;
    public Texture2D ToolCursor;
    [SerializeField] private bool bUseTool = false;
    [SerializeField] private Texture2D mActiveCursor;

    [Header("SoundEffects")]
    [SerializeField] private AudioSource mSFX;
    [SerializeField] private AudioClip mWalkSound;

    private void Start()
    {
        mController = this.gameObject.GetComponent<CharacterController>();
        mCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mAnimator = GetComponentInChildren<Animator>();
        mInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<SInventory>();
        mSFX = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Gravity();
        Move();

        if (horizontalInput < 0f && bIsFacingRight == true)
        {
            Flip();
        }
        else if (horizontalInput > 0f && bIsFacingRight == false)
        {
            Flip();
        }

        if (mPlayerVelocity.x == 0 && mPlayerVelocity.z == 0 && mPlayerVelocity.y <= 0)
        {
            mAnimator.SetBool("Moving", false);
        }
        else
        {
            mAnimator.SetBool("Moving", true);
        }
    }

    private void Update()
    {
        InteractionSystem();

        if (Input.GetButtonDown("Jump") && mController.isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            mPlayerSpeed += mSprintSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            mPlayerSpeed = mSetSpeed;
        }
    }

    public void SetUpCursor(SItemProfile cursor)
    {
        ToolCursor = cursor.MouseIcon;
        bUseTool = true;
    }

    public void GetDefaultCursor()
    {
        bUseTool = false;
    }


    public void InteractionSystem()
    {

        if (mInventory.IsSpyglass || mInventory.IsStick)
        {
            InteractRange = 500.0f;
        }
        else if (mInventory.IsSpyglass == false || mInventory.IsStick == false)
        {
            InteractRange = MinInteractRange;
        }
        if (bCanInteract == true)
        {
            //Debug Draw
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = InteractRange;
            mousePosition = mCamera.ScreenToWorldPoint(mousePosition);
            Debug.DrawRay(mCamera.transform.position, mousePosition - mCamera.transform.position, Color.blue);
            //

            Vector3 mousePos = Input.mousePosition;
            Ray ray = mCamera.ScreenPointToRay(mousePos);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, InteractRange))
            {

                if (hit.transform.TryGetComponent<IInteractable>(out var item))
                {
                    MouseCursorChange(hit.transform.gameObject);

                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log(hit.transform.name);
                        item.OnInteract(this);
                    }
                }
            }
        }
    }

    public void MouseCursorChange(GameObject objectHit)
    {
        string tag = objectHit.tag;

        switch (tag)
        {
            case "NPC":
                if (IsNearby)
                {
                    Cursor.SetCursor(NpcCursor, Vector2.zero, CursorMode.Auto);
                    mActiveCursor = NpcCursor;
                }
                else if (tag == "NPC")
                {
                    Cursor.SetCursor(FlavourCursor, Vector2.zero, CursorMode.Auto);
                    mActiveCursor = FlavourCursor;
                }
                    break;

            case "ITEM":
               
                
                if (IsNearby)
                {
                    Cursor.SetCursor(ItemCursor, Vector2.zero, CursorMode.Auto);
                    mActiveCursor = ItemCursor;
                }
                else if (tag == "ITEM")
                {
                    Cursor.SetCursor(FlavourCursor, Vector2.zero, CursorMode.Auto);
                    mActiveCursor = FlavourCursor;
                }
                break;

            default:
                if (bUseTool)
                {
                    Cursor.SetCursor(ToolCursor, Vector2.zero, CursorMode.Auto);
                    mActiveCursor = ToolCursor;
                }

                else
                {
                    Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.Auto);
                    mActiveCursor = DefaultCursor;
                }
                break;

        }
    }

    public void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal") * mPlayerSpeed, 0, Input.GetAxis("Vertical") * mPlayerSpeed);

        mPlayerVelocity = new Vector3(move.x, mPlayerVelocity.y, move.z);

        if (mController.enabled == true)
        {
            mController.Move(mPlayerVelocity * Time.fixedDeltaTime);
        }
    }

    public void Jump()
    {
        mPlayerVelocity.y += Mathf.Sqrt(mJumpPower * -2.0f * mGravity);
    }

    private void Gravity()
    {
        if (mController.isGrounded && mPlayerVelocity.y < 0)
        {
            mPlayerVelocity.y = 0;
        }

        mPlayerVelocity.y += mGravity * Time.fixedDeltaTime;
    }

    private void Flip()
    {
        bIsFacingRight = !bIsFacingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    public void LoadData(SGameData data)
    {
        PeopleHelped = data.PeopleHelped;
    }

    public void SaveData(ref SGameData data)
    {
        data.PeopleHelped = PeopleHelped;
    }

    /*
    private void Update()
    {
        if (mPlayerVelocity.y < 0)
        {
            mPlayerVelocity.y = 0;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //mController.Move(move * Time.deltaTime * mPlayerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        mPlayerVelocity.y += mGravity * Time.deltaTime;
        mController.Move(mPlayerVelocity * Time.deltaTime);
    }
    */
}
