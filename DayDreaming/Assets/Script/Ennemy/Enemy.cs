using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

            CardsFightUI = GameManager.instance.uiManager.CardsFight;
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
        else ai.Stop();
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

    public override IEnumerator Attack(Fighter other) 
    {
        if(CanPlay() && FightCards.Count > 0)
        {
            int random = Random.Range(0, FightCards.Count);
            CardAsset card = FightCards[random];
            FightCards.Remove(card);

            yield return StartCoroutine(GameManager.instance.uiManager.CardsFight.ShowEnemyCard(card));

            // Apply card effect
            if (card.rarity == CardAsset.Rarity.UNIQUE)
            {
                if (card.AnimationID != -1)
                {
                    var template = CardsFightUI.ImagesManifold[card.AnimationID];
                    var AnimationImage = Instantiate<Image>(template, template.transform.position, template.transform.rotation, CardsFightUI.transform);
                    yield return StartCoroutine(CardsFightUI.CurvesManifold[card.AnimationID].FollowCurve(AnimationImage, true));
                }
            }

            //GameManager.instance.uiManager.CardsFight.UpdateProgressBars();
            yield return StartCoroutine(Attack(card, other));
        }
        GameManager.instance.player.CanPlay(true);
    }

}
