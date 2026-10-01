using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public InputManager inputManager;
    public CameraManager cameraManager;
    PlayerLocomotions playerLocomotions;

    public bool canInteract;
    public bool isInteracting;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotions = GetComponent<PlayerLocomotions>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        if (!isInteracting)
        {
            playerLocomotions.HandleAllMovement();
        }
        else
        {
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero; 
        }
    }

    private void LateUpdate()
    {
        if (!isInteracting) 
        {
            cameraManager.HandleAllCameraMovement();
        }
    }
}

