using UnityEngine;

public class audio : MonoBehaviour
{
    public AudioSource music;
    public float musicVolume;

    public void Play()
    {
        if (!music.isPlaying)
            music.Play();
    }

    public void Stop()
    {
        if (music.isPlaying)
            music.Stop();
    }
}