using UnityEngine;

public class PlayerInteractionManager : InteractableObjects
{
    [Header("Object Interaction UI Settings")]
    [SerializeField] private GameObject objectInteractCanvas;

    protected override void Interact(PlayerManager playerManager)
    {
        FreezePlayer();

        if (objectInteractCanvas != null)
        {
            objectInteractCanvas.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Object Interaction Canvas null" + gameObject.name);
        }
    }

    public void CloseUI()
    {
        if (objectInteractCanvas != null)
        {
            objectInteractCanvas.SetActive(false);
        }

        UnfreezePlayer();
    }
}
