using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    [System.NonSerialized]
    public Dictionary<int, List<Status>> status = new Dictionary<int, List<Status>>();
    [System.NonSerialized]
    public bool canPlay = false;
    public bool isStunned = false;

    public FightFeedback Feedback;
    public float CurrentHP = 10;
    public float MaxHP = 10;
    public List<CardAsset> FightCards;

    protected CardsFight CardsFightUI;

    public abstract IEnumerator Attack(Fighter other);

    protected IEnumerator Attack(CardAsset card, Fighter other)
    {
        card.ApplyEffect(this, other);
        while (Feedback.isAnimating || other.Feedback.isAnimating)
            yield return new WaitForSeconds(Time.deltaTime);
    }

    public void Heal(int amount)
    {
        if(CurrentHP != amount)
        {
            StartCoroutine(Feedback.Heal(1.0f));
            CurrentHP += amount;
            if (CurrentHP > MaxHP)
                CurrentHP = MaxHP;
        }
    }

    public void Hurt(int amount)
    {
        CurrentHP -= amount;
        if (CurrentHP <= 0)
            Die();
        else
            StartCoroutine(Feedback.Hurt(0.5f));
    }

    public void Skip()
    {
        StartCoroutine(Feedback.Stun(1.0f, false));
        canPlay = false;
        isStunned = true;
    }

    public virtual void Die()
    {

    }

    public virtual void CanPlay(bool canPlay)
    {
        this.canPlay = canPlay;
        if(canPlay)
        {
            if (isStunned)
            {
                isStunned = false;
                StartCoroutine(Feedback.Stun(1.0f, true));
            }
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
