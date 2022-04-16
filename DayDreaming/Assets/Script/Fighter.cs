using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [System.NonSerialized]
    public Dictionary<int, List<Status>> status = new Dictionary<int, List<Status>>();
    [System.NonSerialized]
    public bool canPlay = false;

    public float CurrentHP = 10;
    public float MaxHP = 10;
    public List<CardAsset> Cards;

    public void Heal(int amount)
    {
        CurrentHP += amount;
        if (CurrentHP > MaxHP)
            CurrentHP = MaxHP;
    }

    public void Hurt(int amount)
    {
        CurrentHP -= amount;
        if (CurrentHP <= 0)
            Die();
    }

    public virtual void Die()
    {

    }

    public virtual void CanPlay(bool canPlay)
    {
        this.canPlay = canPlay;
        if(canPlay)
        {
            List<Status> StatusOfThisTurn;
            if (status.TryGetValue(GameManager.instance.uiManager.CardsFight.Turn, out StatusOfThisTurn))
                StatusOfThisTurn.ForEach(s => s.ApplyStatus());

            if(!this.canPlay)
            {
                if (this is Player) GameManager.instance.uiManager.CardsFight.Enemy.CanPlay(true);
                else if (this is Enemy) GameManager.instance.player.CanPlay(true);
            }

            status.Remove(GameManager.instance.uiManager.CardsFight.Turn);
        }
    }

    public bool CanPlay()
    {
        return canPlay;
    }
}
