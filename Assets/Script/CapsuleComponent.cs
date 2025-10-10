using UnityEngine;

public class CapsuleComponent : MonoBehaviour
{
    public void Interact()
    {
        TargetManager.instance.TargetDestroyed();
        Destroy(gameObject);
    }

}
