using Unity.VisualScripting;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] ObjectPool pool;
    [SerializeField] GameObject audioPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip, Transform sourceTransform, float volume)
    {

        GameObject audioObject = pool.GetPooledObject(audioPrefab);
        audioObject.SetActive(true);
        AudioSource audio = audioObject.GetComponent<AudioSource>();
        audio.volume = volume;
        audio.clip = clip;
        audio.transform.position = sourceTransform.position;
        audio.Play();
        audioObject.GetComponent<RecycleEffect>().Recycle(audio.clip.length);
    }
}
