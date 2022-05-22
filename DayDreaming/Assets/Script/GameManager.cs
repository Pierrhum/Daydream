using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    // Scène
    public enum Scene { City, House }
    public Scene currentScene;

    private void Awake()
    {
        instance = this;
        currentScene = Scene.City;
    }
    private void Start()
    {
        soundManager.PlayMusic(SoundManager.MusicType.Main);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
    }

    public void ChangeScene(Scene from, Scene to)
    {
        foreach(TilemapRenderer tilemapRenderer in GameObject.FindGameObjectWithTag(to.ToString()).GetComponentsInChildren<TilemapRenderer>())
        {
            tilemapRenderer.enabled = true;
        }

        foreach (TilemapRenderer tilemapRenderer in GameObject.FindGameObjectWithTag(from.ToString()).GetComponentsInChildren<TilemapRenderer>())
        {
            tilemapRenderer.enabled = false;
        }
    }
}
