using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Descritpion;

    public void Show(CardAsset Card)
    {
        gameObject.SetActive(true);

        Name.SetText(Card.Name);
        Descritpion.SetText(Card.description);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
