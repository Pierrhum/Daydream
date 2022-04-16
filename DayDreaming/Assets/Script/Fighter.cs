using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float CurrentHP = 10;
    public float MaxHP = 10;
    public List<CardAsset> Cards;

    public void Heal(int amount)
    {
        CurrentHP += amount;
        if (CurrentHP > MaxHP)
            CurrentHP = MaxHP;
    }
}
