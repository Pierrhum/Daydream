using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHand : MonoBehaviour
{
    public Player player;
    public BezierCurve bezier;
    public List<UICard> UICards;

    public UICard template;

    public Transform dropArea;

    public bool CanPlay = true;
    public bool DebugCards = true;

    private void Awake()
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
    private void OnDrawGizmos()
    {
        if(DebugCards)
        {
            UICard[] cards = GetComponentsInChildren<UICard>();
            if (cards.Length != player.Cards.Count)
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
        foreach (CardAsset card in player.Cards)
        {
            UICard uiCard = Instantiate(template, template.transform.position, template.transform.rotation, transform);

            CardsFight CardsFightUI = transform.parent.GetComponent<CardsFight>();
            uiCard.Init(CardsFightUI, card);

            UICards.Add(uiCard);
        }
    }

    public void InitCardPos()
    {
        if(CanPlay && bezier.curve != null)
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
        if(CanPlay && UICards.Count > 1)
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
        if(CanPlay)
        {
            foreach (UICard card in UICards)
                card.SetPositionOnCurve(card.initialPosOnCurve);
        }
    }

}
