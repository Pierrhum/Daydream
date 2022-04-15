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
    private bool gizmos = true;

    private void Awake()
    {
        InitCardUI();
        InitCardPos();
    }
    private void OnDrawGizmos()
    {
        if(UICards.Count != player.Cards.Count)
        {
            foreach (UICard card in UICards)
            {
                UICards.Remove(card);
                DestroyImmediate(card.gameObject);
            }
            InitCardUI();
            InitCardPos();
        }


    }

    private void InitCardUI()
    {
        if(player==null)
            player = GameManager.instance.player;
        int id = 0;
        foreach (CardAsset card in player.Cards)
        {
            UICard uiCard = Instantiate(template, template.transform.position, template.transform.rotation, transform);
            uiCard.Init(this, id, card);
            id++;

            UICards.Add(uiCard);
        }
    }

    public void InitCardPos()
    {
        if(CanPlay && bezier.curve != null)
        {
            float cardGap = 1.0f / (UICards.Count + 1);
            foreach (UICard card in UICards)
            {
                float gap = (card.index + 1) * cardGap;
                int curveIndex = (int)((bezier.curve.Count) * gap);
                card.SetInitialPos(curveIndex);
                card.SetPositionOnCurve(curveIndex);
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
