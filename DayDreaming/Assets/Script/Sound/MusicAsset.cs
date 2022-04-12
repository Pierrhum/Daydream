using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Music", menuName = "Music")]
public class MusicAsset : ScriptableObject
{
    public AudioClip begin;
    public AudioClip loop;
    public AudioClip end;

}
