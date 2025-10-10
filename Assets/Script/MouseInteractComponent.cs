using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInteractComponent : MonoBehaviour
{
    bool isInteracting = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInteracting)
            Interact();
    }

    void Interact()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }

        if (hit.collider.CompareTag("Interactable")) 
            Destroy(hit.collider.gameObject);

        isInteracting = false;
    }

    public void InputSelect(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isInteracting = true;
            //Debug.Log("Interacting");
        }
        else if (ctx.canceled)
        {
            isInteracting = false;
            //Debug.Log("Stopped Interacting");
        }
    }
}
