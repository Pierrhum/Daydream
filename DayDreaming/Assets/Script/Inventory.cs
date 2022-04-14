using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Must be a singleton
public class Inventory : MonoBehaviour
{
    public List<CardAsset> cardInventory = new List<CardAsset>();
    public GameObject inventoryWindow;
    public TextMeshProUGUI textCategory;
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
        cardInventory.Add(cardToAdd);
    }

    public void openInventory()
    {
        Time.timeScale = 0;
        inventoryWindow.SetActive(true);
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
        }
        else{
            textCategory.SetText("Object");
        }

        choose++;
    }

    public void OnPointerEnter()
    {   
        Vector3 scaleChange = new Vector3(2.0f, 2.0f, .0f);
        GameObject.FindWithTag("TestCard").transform.localScale = scaleChange;
    }

    public void OnPointerExit()
    {
        Vector3 scaleChange = new Vector3(1.0f, 1.0f, .0f);
        GameObject.FindWithTag("TestCard").transform.localScale = scaleChange;  
    }
 
}
