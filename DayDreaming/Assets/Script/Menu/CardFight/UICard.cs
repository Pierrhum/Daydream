using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Coffee.UIEffects;
using UnityEngine.UI;
using System.Reflection;

public class UICard : MonoBehaviour
{
    public CardsFight CardsFightUI;
    public CardInfo InfoPopUp;
    public UIShiny uIShiny;
    public Texture DestroyTransition;

    public int initialPosOnCurve;
    public int index;
    private CardAsset card;

    private Coroutine lastC;
    private static bool isDragging = false;
    [System.NonSerialized]
    public bool isSelected = false;
    private PlayerHand hand;
    private Player player;

    public void Init(CardsFight CardsFightUI, CardAsset card)
    {
        uIShiny.enabled = card.rarity == CardAsset.Rarity.UNIQUE;
        this.CardsFightUI = CardsFightUI;
        this.card = card;
        this.hand = CardsFightUI.PlayerHand;
        this.player = hand.player;
        this.InfoPopUp.Hide();

        GetComponent<Image>().sprite = card.Sprite;

        if (uIShiny.enabled)
            StartCoroutine(Utils.UI.BrightCoroutine(this, 1f));

    }

    public void SetInitialPos(int index, int initialPosOnCurve)
    {
        this.index = index;
        this.initialPosOnCurve = initialPosOnCurve;
        BezierCurve bezier = hand.bezier;

        transform.position = new Vector3(bezier.curve[initialPosOnCurve].x, bezier.curve[initialPosOnCurve].y, 0);
        transform.eulerAngles = new Vector3(0, 0, bezier.angles[initialPosOnCurve]);
    }
    // Ecarter les deux cartes autour
    public void OnCardHover()
    {
        if(player.CanPlay() && !isDragging)
        {
            InfoPopUp.Show(card);
            hand.MoveCards(index);
            uIShiny.enabled = true;
        }
    }

    // Ecarter les deux cartes autour
    public void OnCardExit()
    {
        if (player.CanPlay() && !isDragging)
        {
            InfoPopUp.Hide();
            hand.ResetCardPos();
            uIShiny.enabled = card.rarity == CardAsset.Rarity.UNIQUE;
        }
    }
    public void OnClick()
    {
        if (player.CanPlay() && !isDragging)
            Debug.Log("click");
    }

    public void OnDrag(BaseEventData data)
    {
        if(player.CanPlay())
        {
            InfoPopUp.Hide();
            isDragging = true;
            if (player.CanPlay() && !isSelected)
                StartCoroutine(CardSelected());

            PointerEventData pointerData = data as PointerEventData;
            transform.position += new Vector3(pointerData.delta.x, pointerData.delta.y, 0);
        }
    }

    public void OnDrop(BaseEventData data)
    {
        if (player.CanPlay())
        {
            isDragging = false;
            isSelected = false;
            if (RectTransformUtility.RectangleContainsScreenPoint(hand.dropArea as RectTransform, transform.position))
                StartCoroutine(UseCard(0.5f));
            else
            {
                uIShiny.width = 0.8f;
                SetPositionOnCurve(initialPosOnCurve);
            }
        }
    }

    public void SetPositionOnCurve(int index)
    {        
        lastC = StartCoroutine(MoveCardCoroutine(new Vector3(hand.bezier.curve[index].x, hand.bezier.curve[index].y, 0),
                                        new Vector3(0, 0, hand.bezier.angles[index]), 0.2f));

    }

    private IEnumerator MoveCardCoroutine(Vector3 position, Vector3 rotation, float duration)
    {
        if (!position.Equals(transform.position))
        {
            // Correction de la rotation
            float currentZ = transform.eulerAngles.z;
            if (Mathf.Abs(rotation.z - transform.eulerAngles.z) > 50f)
            {
                if (rotation.z > transform.eulerAngles.z) currentZ += 360;
                else rotation = new Vector3(rotation.x, rotation.y, rotation.z + 360);
            }

            if (lastC != null)
                StopCoroutine(lastC);

            float timer = 0f;
            AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });
            while (timer <= duration)
            {
                timer += Time.deltaTime;

                // Linear interpolation
                transform.position = Vector3.Lerp(transform.position, position, smoothCurve.Evaluate(timer / duration));
                // Angular interpolation
                float z = Mathf.Lerp(currentZ, rotation.z, smoothCurve.Evaluate(timer / duration));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
    private IEnumerator CardSelected()
    {
        isSelected = true;
        AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });
        uIShiny.effectFactor = 0.5f;
        while (isDragging)
        {
            while (uIShiny.width < 1)
            {
                uIShiny.width += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return new WaitForSeconds(0.1f); 
            while (uIShiny.width > 0.5f)
            {
                uIShiny.width -= Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    private IEnumerator UseCard(float duration)
    {
        player.CanPlay(false);
        hand.UICards.Remove(this);
        Destroy(uIShiny);
        yield return new WaitForSeconds(Time.deltaTime);
        UIDissolve dissolve = gameObject.AddComponent<UIDissolve>();
        dissolve.transitionTexture = DestroyTransition;
        dissolve.effectFactor = 0;

        float timer = 0f;
        AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            dissolve.effectFactor = Mathf.Lerp(0f, 1f, smoothCurve.Evaluate(timer / duration));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        // Apply card effect
        if (card.rarity == CardAsset.Rarity.UNIQUE)
        {
            if(card.AnimationID != -1)
            {
                var template = CardsFightUI.ImagesManifold[card.AnimationID];
                var AnimationImage = Instantiate<Image>(template, template.transform.position, template.transform.rotation, CardsFightUI.transform);
                yield return StartCoroutine(Animate(CardsFightUI.CurvesManifold[card.AnimationID], AnimationImage));
            }

        }
        card.ApplyEffect(hand.player, CardsFightUI.Enemy);
        CardsFightUI.UpdateProgressBars();
        hand.InitCardPos();

        CardsFightUI.Enemy.CanPlay(true);
        CardsFightUI.Enemy.Attack();
        CardsFightUI.UpdateProgressBars();

        CardsFightUI.EndTurn();
        Destroy(gameObject);
    }


    private IEnumerator Animate(BezierCurve Bezier, Image AnimationImage)
    {
        int pos = 0;
        while (pos < Bezier.curve.Count)
        {
            Debug.Log(pos);
            AnimationImage.transform.position = Bezier.curve[pos];
            AnimationImage.transform.eulerAngles = new Vector3(0,0,30 * pos);
            pos +=1;
            yield return new WaitForSeconds(Time.deltaTime * 3f);
        }
        Destroy(AnimationImage);
        yield return null;
    }
}
