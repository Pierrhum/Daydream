using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardAsset : ScriptableObject
{
    public enum Rarity { COMMON, RARE, LEGENDARY, UNIQUE };

    public string Name;
    public Sprite Sprite;
    public Rarity rarity;
    public string description;

    public void ApplyEffect(Fighter fighter, Fighter opponent)
    {
        // asset filename
        switch(name)
        {
            case "FlyingCarpet":
                Debug.Log("Todo : Flying Carpet");
                break;

            case "Resting":
                fighter.Heal(5);
                break;

            case "Loneliness":
                Debug.Log("Todo : Loneliness");
                break;

            case "Rooted":
                Debug.Log("Todo : Rooted");
                break;

            case "SinisterPath":
                Debug.Log("Todo : Sinister Path");
                break;

            case "Watched":
                Debug.Log("Todo : Watched");
                break;

            default:
                Debug.Log(name + " - card effect uninitialized");
                break;
        }
    }
}
