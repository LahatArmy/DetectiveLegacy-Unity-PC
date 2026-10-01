using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public InputManager inputManager;

    public Transform targetTransform;
    public Transform cameraPivot;
    public Transform cameraTransform;
    public LayerMask collisionLayers;
    private float defaultPosition;
    private float targetCameraPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;

    public float cameraCollisionOffset = 0.2f;
    public float minimumCollisionOffset = 0.2f;
    public float cameraCollisionRadius = 1f;
    public float cameraFollowSpeed = 0.1f;
    public float cameraLookSpeed = 1.0f;
    public float cameraPivotSpeed = 1.0f;

    public float lookAngle;
    public float pivotAngle;
    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35;

    private void Awake()
    {
        defaultPosition = cameraTransform.localPosition.z;
        inputManager = FindFirstObjectByType<InputManager>();
        targetTransform = FindFirstObjectByType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        

        
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

      private void HandleCameraCollisions()
    {
        targetCameraPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if(Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetCameraPosition =- (distance - cameraCollisionRadius);
        }

        if(Mathf.Abs(targetCameraPosition) < cameraCollisionRadius)
        {
            targetCameraPosition =- cameraCollisionRadius;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetCameraPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
