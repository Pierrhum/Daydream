using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Music music;

    void Start()
    {
        music.Play();
    }
}
