using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeSong : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip newSong;
    public AudioClip oldSong;

    public void ChangeBossSong()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = newSong;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource not assigned.");
        }
    }

    public void ChangeToSceneSong()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = oldSong;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource not assigned.");
        }
    }

    public void StopSong()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        else
        {
            Debug.LogError("AudioSource not assigned.");
        }
    }
}
