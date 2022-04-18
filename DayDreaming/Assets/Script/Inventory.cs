using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Must be a singleton
public class Inventory : MonoBehaviour
{
    public Player player;
    public GameObject inventoryWindow;
    public TextMeshProUGUI textCategory;
    public GameObject textName;
    public GameObject textDescription;
    public GameObject textClick;
    public GameObject cardPannel;
    public GameObject objectPannel;   

    private int choose = 0;

    // Implementation of singleton instance
    public static Inventory instance;

    private void Awake()
    {
        // Vérifie si c'est bien un singleton
        if(instance != null){
             Debug.LogWarning("Il y a plus d'une instance d'Inventory dans la scène !");
             return; 
        } 

        instance = this;
    }

    // Permet d'ajouter une carte à l'inventaire
    public void addCard(CardAsset cardToAdd)
    {
        player.FightCards.Add(cardToAdd);
    }

    public void openInventory()
    {
        Time.timeScale = 0;
        inventoryWindow.SetActive(true);

        for(int i = 0; i < 5; i++){
            GameObject card = GameObject.Find(i.ToString());
            if(i < player.FightCards.Count){
                card.GetComponent<Button>().image.sprite = player.FightCards[i].Sprite;
            }
            else{
                card.SetActive(false);
            }
        } 
    }

    public void exitMenu()
    {
        Time.timeScale = 1;
        inventoryWindow.SetActive(false);
    }

    public void rightChoose()
    {
        choose++;
        if(choose%2 == 0){
            textCategory.SetText("Card");
            cardPannel.SetActive(true);
            objectPannel.SetActive(false);
        }
        else{
            textCategory.SetText("Object");
            cardPannel.SetActive(false);
            objectPannel.SetActive(true);
        }

        
    }

    public void leftChoose()
    {
        if(choose%2 == 0){
            textCategory.SetText("Card");
            cardPannel.SetActive(true);
            objectPannel.SetActive(false);
        }
        else{
            textCategory.SetText("Object");
            cardPannel.SetActive(false);
            objectPannel.SetActive(true);
        }

        choose++;
    }

    public void OnPointerEnter(int nbButton)
    {   
        Vector3 scaleChange = new Vector3(1.2f, 1.2f, 1.0f);
        Debug.Log("test");
        GameObject.Find(nbButton.ToString()).transform.localScale = scaleChange;
    }

    public void OnPointerExit(int nbButton)
    {
        Vector3 scaleChange = new Vector3(1.0f, 1.0f, 1.0f);
        GameObject.Find(nbButton.ToString()).transform.localScale = scaleChange;
    }

    public void setNameAndDesc()
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        textName.SetActive(true);
        textDescription.SetActive(true);
        textClick.SetActive(false);
        textName.GetComponent<TextMeshProUGUI>().SetText(player.FightCards[Int32.Parse(EventSystem.current.currentSelectedGameObject.name)].Name);
        textDescription.GetComponent<TextMeshProUGUI>().SetText(player.FightCards[Int32.Parse(EventSystem.current.currentSelectedGameObject.name)].description);
    }
 
}
