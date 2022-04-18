using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    // Serialized Fields
    public float RangeOfAggression = 1.5f;
    public EnemyAssets asset;
    public AI ai;

    private void Awake()
    {
        if(asset != null)
        {
            ai.Speed = asset.Speed;
            GetComponentInChildren<SpriteRenderer>().sprite = asset.Sprite;
            RangeOfAggression = asset.RangeOfAggression;
        }
    }
    void Update()
    {
        if (!GameManager.instance.player.isFighting)
        {
            Vector2 PlayerDirection = GameManager.instance.player.transform.position - this.transform.position;

            float PlayerDistance = PlayerDirection.magnitude;

            if (PlayerDistance < this.RangeOfAggression)
            {

                if (ai.Collider.IsTouching(ai.PlayerCollider)) LoadFight();

                else ai.Move(PlayerDirection);
            }

            else ai.Stop();
        }
    }

    void LoadFight()
    {
        ai.Stop();
        GameManager.instance.player.isFighting = true;
        GameManager.instance.uiManager.OpenFightMenu(this);
    }

    public override void Die()
    {
        base.Die();
        GameManager.instance.uiManager.CloseFightMenu();
        Destroy(gameObject);
    }

    public void Attack()
    {
        if(CanPlay() && Cards.Count > 0)
        {
            int random = Random.Range(0, Cards.Count);
            CardAsset card = Cards[random];
            card.ApplyEffect(this, GameManager.instance.player);
            Cards.Remove(card);

            GameManager.instance.uiManager.CardsFight.UpdateProgressBars();
            StartCoroutine(GameManager.instance.uiManager.CardsFight.ShowEnemyCard(card));
            GameManager.instance.player.CanPlay(true);
        }
    }

}
