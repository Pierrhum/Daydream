using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Introduction : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private Music Music;

    private void Awake()
    {
        if (Music != null)
            Music.Play();
    }

    public void loadScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
