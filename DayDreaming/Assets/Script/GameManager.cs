using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    [System.NonSerialized]
    public static GameManager instance;

    // Managers
    public SoundManager soundManager;
    public UIManager uiManager;

    // Game
    public Player player;

    private void Awake()
    {
        instance = this;
        soundManager.music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
    }
}
