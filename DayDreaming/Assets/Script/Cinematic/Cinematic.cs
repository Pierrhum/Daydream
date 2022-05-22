using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    public List<ActionCinematic> Actions;
    private bool isPlaying = false;

    public IEnumerator Play()
    {
        if(!isPlaying)
        {
            isPlaying = true;
            GameManager.instance.player.StopMoving();
            foreach (ActionCinematic action in Actions)
                yield return StartCoroutine(action.ProcessAction());
            GameManager.instance.player.CanMove = true;
            Destroy(gameObject);
        }
    }
}

