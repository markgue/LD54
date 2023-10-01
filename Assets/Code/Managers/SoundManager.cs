using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the soundtrack and sound effects
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] AudioSource musicChannel;
    [SerializeField] AudioSource[] sfxChannels;
    [SerializeField] int startingSong;
    SoundSerializer ser;
    Coroutine fadingRoutine;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
        ser = GetComponent<SoundSerializer>();
        if (startingSong >= 0)
        {
            PlaySong(startingSong);
        }
    }

    //calls a coroutine to fade out the soundtrack over a set period of time
    public void FadeOutMusic(float time)
    {
        if (fadingRoutine != null)
            StopCoroutine(fadingRoutine);
        fadingRoutine = StartCoroutine(FadeOutMusicRoutine(time));
    }
    private IEnumerator FadeOutMusicRoutine(float time)
    {
        float interval = time / 100;
        float start = musicChannel.volume;
        for (int i = 0; i < 100; i++)
        {
            musicChannel.volume = Mathf.Lerp(start, 0f, i / 100f);
            yield return new WaitForSeconds(interval);
        }
        fadingRoutine = null;
    }

    //makes a song from the soundtrack play at its intended volume starting from the beginning
    public void PlaySong(int id)
    {
        if (fadingRoutine != null)
            StopCoroutine(fadingRoutine);
        musicChannel.volume = ser.GetSongVolume(startingSong);
        musicChannel.clip = ser.GetSong(startingSong);
        musicChannel.loop = true;
        musicChannel.Play();
    }

    //TODO: implement sfx
}
