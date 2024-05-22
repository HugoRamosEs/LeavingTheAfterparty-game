using UnityEngine;

/// <summary>
/// This script is used to change the song that is playing in the game.
/// </summary>
public class ChangeSong : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip newSong;
    public AudioClip oldSong;

    /// <summary>
    /// Changes the song that is playing to the boss song.
    /// </summary>
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

    /// <summary>
    /// Changes the boss fight song back to the original song.
    /// </summary>
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

    /// <summary>
    /// Stops the song that is playing.
    /// </summary>
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
