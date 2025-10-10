using UnityEngine;

public class CapsuleComponent : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;
    public void Interact()
    {
        //SFXManager.instance.PlaySFX(collectSound, transform, 0.5f);
        TargetManager.instance.TargetDestroyed();
        Destroy(gameObject);
    }

}
