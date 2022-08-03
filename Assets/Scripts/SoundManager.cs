using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public AudioSource uiSfx;
    public AudioSource bgMusic;

    public float fadeOutDuration;
    public float targetVol;
    float defaultVol;
    private void Start()
    {
        uiSfx = GetComponent<AudioSource>();
        defaultVol = uiSfx.volume;
    }

    public void PlayUISound()
    {
        uiSfx.Play();
    }
    public void FadeOutMusic()
    {
        StartCoroutine(PauseMenuMusic(targetVol));
    }

    public void FadeInMusic()
    {
        StartCoroutine(PauseMenuMusic(defaultVol));
    }
    
    IEnumerator PauseMenuMusic(float targetVol)
    {
        float currentTime = 0;
        float start = bgMusic.volume;
        while (currentTime < fadeOutDuration)
        {
            currentTime += Time.unscaledDeltaTime;
            bgMusic.volume = Mathf.Lerp(start, targetVol, currentTime / fadeOutDuration);
            yield return null;
        }
        yield break;
    }
}
