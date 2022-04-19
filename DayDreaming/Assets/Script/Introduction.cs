using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Introduction : MonoBehaviour
{
    [SerializeField] private string nextScene;

    public void loadScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
