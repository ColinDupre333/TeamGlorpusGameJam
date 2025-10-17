using UnityEngine;

public class CapsuleComponent : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;
    [SerializeField] float soundVolume = 0.5f;
    public void Interact()
    {
        SFXManager.instance.PlaySFX(collectSound, transform, soundVolume);
        TargetManager.instance.TargetDestroyed();
        Destroy(gameObject);
    }

}
