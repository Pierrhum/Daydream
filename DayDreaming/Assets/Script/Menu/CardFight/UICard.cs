using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICard : MonoBehaviour
{
    public PlayerHand hand;
    private int index;
    public int initialPosOnCurve;

    private Coroutine lastC;
    private static bool isDragging = false;

    public void Setup(PlayerHand hand, int index, int initialPosOnCurve)
    {
        this.hand = hand;
        this.index = index;
        this.initialPosOnCurve = initialPosOnCurve;

        transform.position = new Vector3(hand.bezier.curve[initialPosOnCurve].x, hand.bezier.curve[initialPosOnCurve].y, 0);
        transform.eulerAngles = new Vector3(0, 0, hand.bezier.angles[initialPosOnCurve]);
    }
    // Ecarter les deux cartes autour
    public void OnCardHover()
    {
        if(!isDragging)
            hand.MoveCards(index);
    }

    // Ecarter les deux cartes autour
    public void OnCardExit()
    {
        if (!isDragging)
            hand.ResetCardPos();
    }
    public void OnClick()
    {
        if (!isDragging)
            Debug.Log("click");
    }

    public void OnDrag(BaseEventData data)
    {
        isDragging = true;
        PointerEventData pointerData = data as PointerEventData;
        transform.position += new Vector3(pointerData.delta.x, pointerData.delta.y, 0);
    }

    public void OnDrop(BaseEventData data)
    {
        isDragging = false;
        SetPositionOnCurve(initialPosOnCurve);
    }

    public void SetPositionOnCurve(int index)
    {
        lastC = StartCoroutine(MoveCardCoroutine(new Vector3(hand.bezier.curve[index].x, hand.bezier.curve[index].y, 0),
                                        new Vector3(0, 0, hand.bezier.angles[index]), 0.2f));

    }

    private IEnumerator MoveCardCoroutine(Vector3 position, Vector3 rotation, float duration)
    {
        if (lastC != null)
            StopCoroutine(lastC);
        if (initialPosOnCurve == (hand.bezier.curve.Count - 1) * 0.5f && rotation.z > 180f)
        {
            transform.eulerAngles = new Vector3(0, 0, 360f);
        }
        float timer = 0f;
        AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });
        while (timer <= duration)
        {
            timer += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, position, smoothCurve.Evaluate(timer / duration));
            float z = Mathf.Lerp(transform.eulerAngles.z, rotation.z, smoothCurve.Evaluate(timer / duration));
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
