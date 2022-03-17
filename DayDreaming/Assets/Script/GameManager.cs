using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script de game manager
// TODO SINGLETON !!!!
// @author Maxime
public class GameManager : MonoBehaviour
{

    public GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        // TODO 
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
