using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIEffects;

public class CardsFight : MonoBehaviour
{
    public Music music;
    public Image Overlay;
    public Image PlayerSprite;
    public Image EnemySprite;
    public PlayerHand PlayerHand;
    public Enemy Enemy;

    public ProgressBar PlayerBar;
    public ProgressBar EnemyBar;
    public int Turn;

    [SerializeField]
    private List<Image> fadeImages;
    private UIDissolve dissolve;

    public void Start()
    {
        dissolve = Overlay.GetComponent<UIDissolve>();
        dissolve.effectFactor = 1.0f;
        foreach (UICard card in PlayerHand.UICards)
            fadeImages.Add(card.GetComponent<Image>());
        SetOpacity(0.0f);
        gameObject.SetActive(false);
    }

    public void Open(Enemy Enemy)
    {
        this.Enemy = Enemy;
        this.Turn = 0;
        gameObject.SetActive(true);

        UpdateProgressBars();
        music.Play();
        StartCoroutine(ShowCoroutine());
    }

    public void Close()
    {
        music.Stop(true);
        StartCoroutine(HideCoroutine());
    }

    public void UpdateProgressBars()
    {
        PlayerBar.SetFill(PlayerHand.player.CurrentHP / PlayerHand.player.MaxHP);
        EnemyBar.SetFill(Enemy.CurrentHP / Enemy.MaxHP);
    }

    private IEnumerator ShowCoroutine()
    {
        yield return StartCoroutine(Utils.UI.Dissolve(dissolve, 1.0f, 0.0f, 1.0f));
        
        yield return StartCoroutine(Utils.UI.Fade(fadeImages, 0.0f, 1.0f, 0.2f));
        PlayerBar.SetOpacity(1.0f);
        EnemyBar.SetOpacity(1.0f);
        PlayerHand.player.CanPlay(true);
    }

    private IEnumerator HideCoroutine()
    {
        fadeImages.Clear();
        foreach (UICard card in PlayerHand.UICards)
            fadeImages.Add(card.GetComponent<Image>());

        yield return StartCoroutine(Utils.UI.Fade(fadeImages, 1.0f, 0.0f, 0.2f));
        PlayerBar.SetOpacity(0.0f);
        EnemyBar.SetOpacity(0.0f);
        yield return StartCoroutine(Utils.UI.Fade(new List<Image>() { PlayerSprite, EnemySprite }, 1.0f, 0.0f, 0.5f));

        yield return StartCoroutine(Utils.UI.Dissolve(dissolve, 0.0f, 1.0f, 1.0f));

        GameManager.instance.soundManager.music.Play();
        gameObject.SetActive(false);
    }

    private void SetOpacity(float a)
    {
        foreach (Image img in fadeImages)
            Utils.UI.SetImageOpacity(img, a);
        PlayerBar.SetOpacity(a);
        EnemyBar.SetOpacity(a);
    }

    public void EndTurn()
    {
        Turn++;
        // Application des status associés au nouveau tour
        List<Status> PlayerStatus;
        List<Status> EnemyStatus;
        if (PlayerHand.player.status.TryGetValue(Turn, out PlayerStatus))
            PlayerStatus.ForEach(s => s.ApplyStatus());
        if (Enemy.status.TryGetValue(Turn, out EnemyStatus))
            EnemyStatus.ForEach(s => s.ApplyStatus());
    }
}
