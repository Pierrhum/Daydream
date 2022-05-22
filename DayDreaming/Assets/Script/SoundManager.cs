using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private Music MainMusic;
    [SerializeField]
    private Music FightMusic;
    [SerializeField]
    private AudioSource SFXSource;

    private Music CurrentMusic;

    public enum MusicType { Main, Fight }

    public void PlayMusic(MusicType Type)
    {
        if (CurrentMusic != null)
            CurrentMusic.source.Stop();
        switch (Type)
        {
            case MusicType.Main:
                CurrentMusic = MainMusic;
                break;
            case MusicType.Fight:
                CurrentMusic = FightMusic;
                break;
        }
        CurrentMusic.Play();
    }
    public void PlayMusic(Music Music)
    {
        if(Music.source.Equals(CurrentMusic.source))
        {
            CurrentMusic.source.Stop();
            CurrentMusic = Music;
            CurrentMusic.Play();
        }
    }

    public void StopMusic(bool fadeOut)
    {
        CurrentMusic.Stop(fadeOut);
    }

    public void ResumeMusic()
    {
        CurrentMusic.source.Play();
    }

    public IEnumerator Play2DSFX(AudioClip clip, bool WaitEnd)
    {
        SFXSource.clip = clip;
        SFXSource.Play();
        if (WaitEnd)
            while (SFXSource.isPlaying)
                yield return new WaitForSeconds(Time.deltaTime);
    }
    // TODO : Bruitages, Modifier volume son, etc..
}
