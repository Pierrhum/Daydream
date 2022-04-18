using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public MusicAsset asset;

    public AudioSource source;
    private void Start()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
    }
    public void Play()
    {
        StartCoroutine(MusicCoroutine());
    }

    private IEnumerator MusicCoroutine()
    {
        source.clip = asset.begin;
        source.Play();
        while (source.isPlaying)
            yield return new WaitForSeconds(Time.deltaTime);
        source.loop = true;
        source.clip = asset.loop;
        source.Play();
    }

    public void Stop(bool FadeOut)
    {
        source.loop = false;
        if (FadeOut)
        {
            source.clip = asset.end;
            source.Play();
        }
        else
            source.Stop();
    }
}
