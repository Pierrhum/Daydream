using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Must be a singleton
public class Inventory : MonoBehaviour
{
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

    // Gestion des pages
    private int pageNumber = 0;
    private int numberOfPages; 
    private int choose = 0;

    // Choix des cartes
    private int countCardChosen = 0;
    public GameObject popup;

    // Implementation of singleton instance
    public static Inventory instance;

    private Player player;

    private void Awake()
    {
        // Vérifie si c'est bien un singleton
        if(instance != null){
             Debug.LogWarning("Il y a plus d'une instance d'Inventory dans la scène !");
             return; 
        } 


        instance = this;
    }

    private void Start()
    {
        player = GameManager.instance.player;
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
        
            numberOfPages = player.InventoryCards.Count / 5;
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
        Vector3 scaleChange = new Vector3(1.15f, 1.15f, 1.0f);
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
        textName.GetComponent<TextMeshProUGUI>().SetText(player.InventoryCards[Int32.Parse(EventSystem.current.currentSelectedGameObject.name)].Name);
        textDescription.GetComponent<TextMeshProUGUI>().SetText(player.InventoryCards[Int32.Parse(EventSystem.current.currentSelectedGameObject.name)].description);
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

    // Mise à jour de l'inventaire
    private void updateInventory()
    {
        nbCard.SetText((pageNumber+1).ToString() + " / " + numberOfPages.ToString());
        for(int i = 0; i < 5; i++){
            GameObject card = GameObject.Find(i.ToString());
            if(i < player.InventoryCards.Count){
                card.GetComponent<Button>().image.sprite = player.InventoryCards[i + pageNumber * 5].Sprite;

                if (player.FightCards.Contains(player.InventoryCards[i + pageNumber * 5])) 
                    GameObject.Find("T"+i).GetComponent<Toggle>().isOn = true;
                else GameObject.Find("T"+i).GetComponent<Toggle>().isOn = false;

            }
            else{
                card.SetActive(false);
            }
        } 
    }

    // Gestion du choix des cartes
    public void ChangeToggle(string name)
    {
        int id = Int32.Parse(name.Substring(1)) + pageNumber * 5;
        // Lorsqu'on coche
        if (GameObject.Find(name).GetComponent<Toggle>().isOn){
            if(countCardChosen < 5)
            {
                player.FightCards.Add(player.InventoryCards[id]);
                countCardChosen++;
            }
            else{
                if(!player.FightCards.Contains(player.InventoryCards[id])){
                    GameObject.Find(name).GetComponent<Toggle>().isOn = false;
                    OpenPopup();
                }
            }
        }
        else{ // Lorsqu'on decoche
            if(countCardChosen > 0)
            {
                if(player.FightCards.Contains(player.InventoryCards[id])){
                    player.FightCards.Remove(player.InventoryCards[id]);
                    countCardChosen--;
                }
            }
        }
        
    }

    private void OpenPopup(){
        popup.SetActive(true);
    }
    
    public void ClosePopup(){
        popup.SetActive(false);
    }
}
