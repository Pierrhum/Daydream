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

    public List<GameObject> CardsUI;
    public List<Toggle> Toggles;

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

    public void openInventory()
    {
        if(!inventoryWindow.activeSelf){
            Time.timeScale = 0;
            inventoryWindow.SetActive(true);
            HUD.SetActive(false);

            if(pauseMenu.activeSelf){
                pauseMenu.SetActive(false);
            }
            Debug.Log(player.InventoryCards.Count%5);
            numberOfPages = player.InventoryCards.Count / 5;
            if (player.InventoryCards.Count % 5 > 0)
                numberOfPages++;
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
        CardsUI[nbButton].transform.localScale = scaleChange;
    }

    // Lorsqu'on arrete de survoler une carte
    public void OnPointerExit(int nbButton)
    {
        Vector3 scaleChange = new Vector3(1.0f, 1.0f, 1.0f);
        CardsUI[nbButton].transform.localScale = scaleChange;
    }

    // Affiche le nom de la carte et sa description
    public void setNameAndDesc()
    {
        textName.SetActive(true);
        textDescription.SetActive(true);
        textClick.SetActive(false);

        textName.GetComponent<TextMeshProUGUI>().SetText(player.InventoryCards[Int32.Parse(EventSystem.current.currentSelectedGameObject.name) + pageNumber * 5].Name);
        textDescription.GetComponent<TextMeshProUGUI>().SetText(player.InventoryCards[Int32.Parse(EventSystem.current.currentSelectedGameObject.name) + pageNumber * 5].description);
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

    bool SkipToggleUpdate = false;
    List<int> checkedToggle = new List<int>();

    // Mise à jour de l'inventaire
    private void updateInventory()
    {
        nbCard.SetText((pageNumber+1).ToString() + " / " + numberOfPages.ToString());
        for(int i = 0; i < 5; i++){
            GameObject card = CardsUI[i];
            Toggle toggle = Toggles[i];
            toggle.gameObject.SetActive(true);
            card.SetActive(true);

            if (i + pageNumber * 5 < player.InventoryCards.Count){
                card.GetComponent<Button>().image.sprite = player.InventoryCards[i + pageNumber * 5].Sprite;

                if (checkedToggle.Contains(i + pageNumber * 5))
                {
                    SkipToggleUpdate = true;
                    toggle.isOn = true;
                    SkipToggleUpdate = false;
                }
                else{
                    SkipToggleUpdate = true;
                    toggle.isOn = false;
                    SkipToggleUpdate = false;
                } 

            }
            else if(toggle != null && card != null){
                toggle.gameObject.SetActive(false);
                card.SetActive(false);
            }
        } 
    }

    // Gestion du choix des cartes
    public void ChangeToggle(int id)
    {
        if (SkipToggleUpdate)
            return;

        // Lorsqu'on coche
        if (Toggles[id].isOn){
            if(countCardChosen < 5)
            {
                if (!player.FightCards.Contains(player.InventoryCards[id + pageNumber * 5]))
                {
                    player.FightCards.Add(player.InventoryCards[id + pageNumber * 5]);
                    countCardChosen++;
                    checkedToggle.Add(id + pageNumber * 5);
                }
            }
            else{
                SkipToggleUpdate = true;
                Toggles[id].isOn = false;
                SkipToggleUpdate = false;
                OpenPopup();
            }
        }
        else{ // Lorsqu'on decoche
            if(countCardChosen > 0)
            {
                if(player.FightCards.Contains(player.InventoryCards[id + pageNumber * 5])){
                    player.FightCards.Remove(player.InventoryCards[id + pageNumber * 5]);
                    countCardChosen--;
                    checkedToggle.Remove(id + pageNumber * 5);
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
