using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    public CameraManager cameraManager;
    PlayerLocomotions playerLocomotions;

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
        playerLocomotions.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }
}

