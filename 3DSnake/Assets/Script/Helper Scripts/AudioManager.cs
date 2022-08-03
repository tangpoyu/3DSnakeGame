using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip pickUp_Sound, dead_Sound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayPickUpSound()
    {
        AudioSource.PlayClipAtPoint(pickUp_Sound, transform.position);
    }

    public void PlayDeadSound()
    {
        AudioSource.PlayClipAtPoint(dead_Sound, transform.position);
    }

}

  