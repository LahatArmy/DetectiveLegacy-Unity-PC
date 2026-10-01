using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    protected PlayerManager playerManager;
    [SerializeField] private GameObject interactableUIGameObject;
    [SerializeField] private Outline outline;
    protected Collider interactableCollider;

    protected virtual void Awake()
    {
        if(outline != null)
        {
            outline.enabled = false;
        }
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        if(outline != null)
        {
            outline.enabled = true;
        }
        if(playerManager == null)
        {
            playerManager = other.GetComponent<PlayerManager>();
        }

        if(playerManager != null && !playerManager.isInteracting)
        {
            interactableUIGameObject.SetActive(true);
            playerManager.canInteract = true;
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {

        if(playerManager != null && !playerManager.isInteracting)
        {
            if(playerManager.inputManager.interactInput)
            {
                Interact(playerManager);
                playerManager.inputManager.interactInput = false;
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if(outline != null)
        {
            outline.enabled = false;
        }
        if(playerManager == null)
        {
            playerManager = other.GetComponent<PlayerManager>();
        }
        if(playerManager != null)
        {
            interactableUIGameObject.SetActive(false);
            playerManager.canInteract = false;
        }
    }


    protected virtual void Interact(PlayerManager playerManager)
    {
        Debug.Log("Interacting with " + transform.name);
    }

    protected void FreezePlayer()
    {
        if (playerManager != null)
        {
            playerManager.isInteracting = true; 
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            interactableUIGameObject.SetActive(false);
            playerManager.canInteract = false;
        }
    }

    protected void UnfreezePlayer()
    {
        if (playerManager != null)
        {
            playerManager.isInteracting = false; 
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            interactableUIGameObject.SetActive(true);
            playerManager.canInteract = true;
        }
    }
}
