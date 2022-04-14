using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

// Must be a singleton
public class Inventory : MonoBehaviour
{
    public List<CardAsset> cardInventory = new List<CardAsset>();
    public GameObject inventoryWindow;
    public TextMeshProUGUI textCategory;
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
        if(choose%2 == 0){
            textCategory.SetText("Card");
        }
        else{
            textCategory.SetText("Object");
        }

        choose++;
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
}
