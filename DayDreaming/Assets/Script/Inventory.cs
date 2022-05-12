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
    public GameObject HUD;
    public GameObject pauseMenu;
    public TextMeshProUGUI nbCard; 
    public GameObject leftCard;
    public GameObject rightCard;
    public int pageNumber = 0;
    public int numberOfPages;

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
        if(!inventoryWindow.activeSelf){
            Time.timeScale = 0;
            inventoryWindow.SetActive(true);
            HUD.SetActive(false);

            if(pauseMenu.activeSelf){
                pauseMenu.SetActive(false);
            }
        
            numberOfPages = player.FightCards.Count / 5;
            pageNumber = 0;
            updateInventory();
        }
        else{
            exitMenu();
        }
    }

    public void exitMenu()
    {
        Time.timeScale = 1;
        HUD.SetActive(true);
        inventoryWindow.SetActive(false);
    }

    // Change de catégorie de menu
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

    // Change de catégorie de menu
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

    // Lorsqu'on survole une carte
    public void OnPointerEnter(int nbButton)
    {   
        Vector3 scaleChange = new Vector3(1.2f, 1.2f, 1.0f);
        GameObject.Find(nbButton.ToString()).transform.localScale = scaleChange;
    }

    // Lorsqu'on arrete de survoler une carte
    public void OnPointerExit(int nbButton)
    {
        Vector3 scaleChange = new Vector3(1.0f, 1.0f, 1.0f);
        GameObject.Find(nbButton.ToString()).transform.localScale = scaleChange;
    }

    // Affiche le nom de la carte et sa description
    public void setNameAndDesc()
    {
        textName.SetActive(true);
        textDescription.SetActive(true);
        textClick.SetActive(false);
        textName.GetComponent<TextMeshProUGUI>().SetText(player.FightCards[Int32.Parse(EventSystem.current.currentSelectedGameObject.name)].Name);
        textDescription.GetComponent<TextMeshProUGUI>().SetText(player.FightCards[Int32.Parse(EventSystem.current.currentSelectedGameObject.name)].description);
    }

    // Passe à la page suivante
    public void nextPage()
    {
        if(pageNumber < numberOfPages-1){
            pageNumber++;
            updateInventory();
        }
    }

    // Reviens à la page précédente
    public void previousPage()
    {
        if(pageNumber > 0){
            pageNumber--;
            updateInventory();
        }
    }

    private void updateInventory()
    {
        nbCard.SetText((pageNumber+1).ToString() + " / " + numberOfPages.ToString());
        for(int i = 0; i < 5; i++){
            GameObject card = GameObject.Find(i.ToString());
            if(i < player.FightCards.Count){
                card.GetComponent<Button>().image.sprite = player.FightCards[i + pageNumber].Sprite;
            }
            else{
                card.SetActive(false);
            }
        } 
    }
 
}
