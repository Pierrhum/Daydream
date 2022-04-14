using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHand : MonoBehaviour
{
    public BezierCurve bezier;
    public List<UICard> UICards;

    private void Awake()
    {
        InitCardPos();
    }
    private void OnDrawGizmos()
    {
        InitCardPos();
    }

    private void InitCardPos()
    {
        float cardGap = 1.0f / (UICards.Count + 1);
        int id = 0;
        foreach (UICard card in UICards)
        {
            float gap = (id+1) * cardGap;
            int curveIndex = (int)((bezier.curve.Count) * gap);
            card.Setup(this, id, curveIndex);
            card.SetPositionOnCurve(curveIndex);
            id++;
        }
    }
    public void MoveCards(int cardId)
    {
        float cardGap = (bezier.curve.Count) / (UICards.Count);
        int rightIndex = (cardId == (UICards.Count - 1)) ? 0 : (int)(UICards[cardId + 1].initialPosOnCurve + (cardGap * 0.5f));
        int leftIndex = (cardId == 0) ? 0 : (int)(UICards[cardId - 1].initialPosOnCurve  - (cardGap * 0.5f));

        if (cardId == 0)
            UICards[cardId + 1].SetPositionOnCurve(rightIndex);
        else if(cardId == UICards.Count-1)
            UICards[cardId - 1].SetPositionOnCurve(leftIndex);
        else
        {
            UICards[cardId + 1].SetPositionOnCurve(rightIndex);
            UICards[cardId - 1].SetPositionOnCurve(leftIndex);
        }
    }
    public void ResetCardPos()
    {
        foreach (UICard card in UICards)
            card.SetPositionOnCurve(card.initialPosOnCurve);
    }

}
