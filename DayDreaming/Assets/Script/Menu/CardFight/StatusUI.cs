using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public Image StatusImage;
    public TextMeshProUGUI TurnRemaining;

    public void UpdateStatusUI(Sprite StatusSprite, int Turn)
    {
        StatusImage.sprite = StatusSprite;
        TurnRemaining.SetText("" + Turn);
    }
}
