using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int CurrentHP = 10;
    public int MaxHP = 10;
    public List<CardAsset> Cards;

    public void Heal(int amount)
    {
        CurrentHP += amount;
        if (CurrentHP > MaxHP)
            CurrentHP = MaxHP;
    }
}
