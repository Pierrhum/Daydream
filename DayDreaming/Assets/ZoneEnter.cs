using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneEnter : MonoBehaviour
{

    public GameObject dialogScene;
    public bool InArea;

    public void showDialog()
    {
        dialogScene.SetActive(true);
    }

    public void closeDialog()
    {
        dialogScene.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.Equals(GameManager.instance.player.GetComponentInChildren<Collider2D>()))
        {
            showDialog();
            InArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.Equals(GameManager.instance.player.GetComponentInChildren<Collider2D>()))
        {
            closeDialog();
            InArea = false;
        }
    }

    public void enterNewZone()
    {
        if(InArea)
        {

        }
    }
}
