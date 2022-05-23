using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Outro : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        Debug.Log(GameManager.instance.player.GetComponentInChildren<Collider2D>());
        if(other==GameManager.instance.player.GetComponentInChildren<Collider2D>())
            SceneManager.LoadScene("MainMenu");
    }
}
