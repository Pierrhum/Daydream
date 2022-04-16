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
            CardsFight CardsFightUI = GameManager.instance.uiManager.CardsFight;

            List <Status> StatusOfThisTurn;
            if (status.TryGetValue(CardsFightUI.Turn, out StatusOfThisTurn))
                StatusOfThisTurn.ForEach(s => s.ApplyStatus());

            status.Remove(CardsFightUI.Turn);

            if (this is Player)
            {
                CardsFightUI.PlayerBar.SetStatus(this);
                if (!this.canPlay) CardsFightUI.Enemy.CanPlay(true);
            }

            else if(this is Enemy)
            {
                CardsFightUI.EnemyBar.SetStatus(this);
                if (!this.canPlay) CardsFightUI.PlayerHand.player.CanPlay(true);
            }
        }
    }

    public bool CanPlay()
    {
        return canPlay;
    }
}
