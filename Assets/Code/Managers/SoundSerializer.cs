using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//exists solely to serialize sound clips that will be used by a SoundManager attached to the same game object
public class SoundSerializer : MonoBehaviour
{
    [SerializeField] AudioClip[] music;
    [SerializeField] float[] musicVolumes;
    [SerializeField] AudioClip[] sfx;
    [SerializeField] float[] sfxVolumes;

    private void Awake()
    {
        if (music.Length != musicVolumes.Length || sfx.Length != sfxVolumes.Length)
            Debug.Log("ERROR: correlating sound and volume arrays in SoundSerializer should match length");
    }

    //returns the song serialized in the music array at the given index
    public AudioClip GetSong(int id)
    {
        return music[id];
    }

    //returns the intended volume level for the given song as specified in the volumes array
    public float GetSongVolume(int id)
    {
        return musicVolumes[id];
    }

    //returns the sound effect serialized in the SFX array at the given index
    public AudioClip GetSFX(int id)
    {
        return sfx[id];
    }

    //returns the intended volume level for the given sound effect as specified in the volumes array
    public float GetSFXVolume(int id)
    {
        return sfxVolumes[id];
    }
}
