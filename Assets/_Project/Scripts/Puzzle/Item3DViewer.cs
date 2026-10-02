using UnityEngine;
using UnityEngine.EventSystems;

public class Item3DViewer : MonoBehaviour, IDragHandler
{
    public Transform currentItemPivot;
    
    public float rotationSpeed = 0.5f;

    private Quaternion defaultRotation;

    private void Awake()
    {
        if (currentItemPivot != null)
        {
            defaultRotation = currentItemPivot.rotation;
        }
    }

    private void OnEnable()
    {
        if (currentItemPivot != null)
        {
            currentItemPivot.rotation = defaultRotation;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentItemPivot != null)
        {
            float rotX = eventData.delta.x * rotationSpeed;
            float rotY = eventData.delta.y * rotationSpeed;

            currentItemPivot.Rotate(Vector3.up, -rotX, Space.World);
            currentItemPivot.Rotate(Vector3.right, rotY, Space.World);
        }
    }


    public void SetupNewItem(Transform newItem)
    {
        if (currentItemPivot != null)
        {
            currentItemPivot.gameObject.SetActive(false);
        }

        currentItemPivot = newItem;
        currentItemPivot.gameObject.SetActive(true);
        defaultRotation = currentItemPivot.rotation;
    }
}