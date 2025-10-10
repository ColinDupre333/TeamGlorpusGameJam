using System.Collections;
using UnityEngine;

public class SoundPlayerScript : MonoBehaviour
{
    bool SoundIsPlaying = false;

    public void PlaySound(AudioClip audio, Transform position, float volume)
    {
        if (!SoundIsPlaying)
        {
            StopAllCoroutines();
            StartCoroutine(PlaySoundCouroutine(audio, transform, volume));
        }
    }

    IEnumerator PlaySoundCouroutine(AudioClip audio, Transform position, float volume)
    {
        SoundIsPlaying = true;
        SFXManager.instance.PlaySFX(audio, position, volume);
        //if (delay > 0 && delay > audio.length)
        //{
        //    yield return new WaitForSeconds(delay);
        //}
        //else
        //{
            yield return new WaitForSeconds(audio.length);
      //  }
        SoundIsPlaying = false;
    }
}
