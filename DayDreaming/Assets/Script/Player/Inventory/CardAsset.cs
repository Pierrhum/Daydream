using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardAsset : ScriptableObject
{
    public enum Type { COMMON, RARE, LEGENDARY, UNIQUE };

    public string Name;
    public Sprite Sprite;
    public Type type;

}
