using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVolume : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetInt("Volume") / 100f;
    }
    public void ChangeVolume()
    {
        audioSource.volume = PlayerPrefs.GetInt("Volume") / 100f;
    }
}
