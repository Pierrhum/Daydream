using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardAsset : ScriptableObject
{
    public enum Rarity { COMMON, RARE, LEGENDARY, UNIQUE };

    public string Name;
    public Sprite Sprite;
    public Rarity rarity;
    public int AnimationID = -1;
    public string description;

    public void ApplyEffect(Fighter fighter, Fighter opponent)
    {
        int Turn = GameManager.instance.uiManager.CardsFight.Turn;

        // asset filename
        switch (name)
        {
            // Debuff
            case "FlyingCarpet":
                fighter.status.Clear();
                break;

            // Cure 20% of your health
            case "Resting":
                fighter.Heal((int)(fighter.MaxHP * 0.2f));
                break;

            // Deal 5% damage, then 10%, and 15%
            case "Loneliness":
                opponent.Hurt((int)(opponent.MaxHP * 0.05f));
                AddStatus(opponent, new Status(this, opponent, Status.Type.Hurt, (int)(opponent.MaxHP * 0.10f)), Turn + 1);
                AddStatus(opponent, new Status(this, opponent, Status.Type.Hurt, (int)(opponent.MaxHP * 0.15f)), Turn + 2);
                break;

            // Skip next turn
            case "Rooted":
                AddStatus(opponent, new Status(this, opponent, Status.Type.Skip), Turn + 1);
                break;

            // Take 5 damages, but deal 15 to the opponent next turn
            case "SinisterPath":
                fighter.Hurt(5);
                AddStatus(opponent, new Status(this, opponent, Status.Type.Hurt, 15), Turn + 1);
                break;

            case "Boomerang":
                opponent.Hurt(5);
                AddStatus(opponent, new Status(this, opponent, Status.Type.Hurt, 15), Turn + 1);
                break;

            case "Watched":
                Debug.Log("Todo : Watched");
                break;

            default:
                Debug.Log(name + " - card effect uninitialized");
                break;
        }
    }

    private void AddStatus(Fighter fighter, Status status, int Turn)
    {
        List<Status> fighterStatus;

        // Si le combattant a déjà des status pour ce tour
        if(fighter.status.TryGetValue(Turn, out fighterStatus))
            fighterStatus.Add(status);
        // Sinon, création d'une nouvelle liste pour ce tour
        else
            fighter.status.Add(Turn, new List<Status>() { status });
    }
}
