using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHand : MonoBehaviour
{
    public Player player;
    public BezierCurve bezier;
    public CardsFight CardsFightUI;
    public List<UICard> UICards;

    public UICard template;

    public Transform dropArea;

    public bool DebugCards = true;

    public void LoadCards()
    {
        UICard[] cards = GetComponentsInChildren<UICard>();
        if (cards.Length != 0)
        {
            foreach (UICard card in cards)
                if (!card.Equals(template))
                    DestroyImmediate(card.gameObject);
            UICards.Clear();
        }
        InitCardUI();
        InitCardPos();
    }

    private void Awake()
    {
        LoadCards();
    }
    private void OnDrawGizmos()
    {
        if(DebugCards)
        {
            UICard[] cards = GetComponentsInChildren<UICard>();
            if (cards.Length != player.FightCards.Count)
            {
                foreach (UICard card in cards)
                {
                    if (!card.Equals(template))
                    {
                        UICards.Remove(card);
                        DestroyImmediate(card.gameObject);
                    }
                }
                InitCardUI();
                InitCardPos();
            }
        }


    }

    private void InitCardUI()
    {
        if(player==null)
            player = GameManager.instance.player;
        foreach (CardAsset card in player.FightCards)
        {
            UICard uiCard = Instantiate(template, template.transform.position, template.transform.rotation, transform);

            uiCard.Init(CardsFightUI, card);

            UICards.Add(uiCard);
        }
    }

    public void InitCardPos()
    {
        if(bezier.curve != null)
        {
            int id = 0;
            float cardGap = 1.0f / (UICards.Count + 1);
            foreach (UICard card in UICards)
            {
                float gap = (id + 1) * cardGap;
                int curveIndex = (int)((bezier.curve.Count) * gap);
                card.SetInitialPos(id, curveIndex);
                card.SetPositionOnCurve(curveIndex);
                id++;
            }
        }
    }
    public void MoveCards(int cardId)
    {
        if(player.CanPlay() && UICards.Count > 1)
        {
            float cardGap = (bezier.curve.Count) / (UICards.Count);
            int rightIndex = (cardId == (UICards.Count - 1)) ? 0 : (int)(UICards[cardId + 1].initialPosOnCurve + (cardGap * 0.5f));
            int leftIndex = (cardId == 0) ? 0 : (int)(UICards[cardId - 1].initialPosOnCurve - (cardGap * 0.5f));

            if (cardId == 0)
                UICards[cardId + 1].SetPositionOnCurve(rightIndex);
            else if (cardId == UICards.Count - 1)
                UICards[cardId - 1].SetPositionOnCurve(leftIndex);
            else
            {
                UICards[cardId + 1].SetPositionOnCurve(rightIndex);
                UICards[cardId - 1].SetPositionOnCurve(leftIndex);
            }
        }
    }
    public void ResetCardPos()
    {
        foreach (UICard card in UICards)
            card.SetPositionOnCurve(card.initialPosOnCurve);
    }

}
