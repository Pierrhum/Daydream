using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    public List<ActionCinematic> Actions;

    public IEnumerator Play()
    {
        foreach (ActionCinematic action in Actions)
            yield return StartCoroutine(action.ProcessAction());
    }
}

