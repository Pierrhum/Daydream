using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private Music MainMusic;
    [SerializeField]
    private Music FightMusic;

    private Music CurrentMusic;

    public enum MusicType { Main, Fight }

    public void PlayMusic(MusicType Type)
    {
        if(CurrentMusic != null)
            CurrentMusic.Stop(false);
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

    public void StopMusic(bool fadeOut)
    {
        CurrentMusic.Stop(fadeOut);
    }

    // TODO : Bruitages, Modifier volume son, etc..
}
