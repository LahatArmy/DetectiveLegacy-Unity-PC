using UnityEngine;

public class BillboardWorldUI : MonoBehaviour
{
    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (mainCamera != null)
    {
        transform.rotation = Quaternion.LookRotation(
            transform.position - mainCamera.transform.position
        );
    }
    }
}
