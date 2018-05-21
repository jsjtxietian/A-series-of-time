using UnityEngine;

public class audio : MonoBehaviour
{
    public AudioSource Music;
    public AudioSource TickTock;
    public float musicVolume;

    public void PlayMusic()
    {
        if (!Music.isPlaying)
            Music.Play();
    }

    public void StopMusic()
    {
        if (Music.isPlaying)
            Music.Stop();
    }

    public void PlayTick()
    {
        TickTock.Play();
    }
}