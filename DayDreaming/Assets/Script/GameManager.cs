using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    [System.NonSerialized]
    public static GameManager instance;

    // Game
    public Player player;

    // UI
    public GameObject gameOverMenu;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
    }

    // Ouvre le game over menu 
    // Pour le moment ce fait quand on clique sur
    public void OpenGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
}
