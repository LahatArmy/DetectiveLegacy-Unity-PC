using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerManager playerManager;
    PlayerControls playerControls;
    AnimatorManager animatorManager;

    [Header("Movement Input")]
    public Vector2 movementInput;
    public Vector2 cameraInput;

    [Header("Camera Input")]
    public float cameraInputX;
    public float cameraInputY;

    [Header("Movement Amount")]
    private float moveAmount;
    public float horizontalInput;
    public float verticalInput;

    [Header("Button Inputs")]
    public bool interactInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerAction.Interact.performed += i => interactInput = true;

        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleInteractInput();
    }


    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    private void HandleInteractInput()
    {
        if (interactInput)
        {
            if(!playerManager.canInteract)
            {
                interactInput = false;
                return;
            }

        }
    }

}
