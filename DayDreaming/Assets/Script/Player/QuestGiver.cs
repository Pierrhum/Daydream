using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest Quest;
    public Cinematic Cinematic;
    public enum Interaction { ENTER_COLLIDER, TALK, KILL};

    public Interaction TriggerType;




    private void Awake()
    {
        Quest.Trigger = this;
        if (Cinematic == null) Quest.Cinematic = GetComponent<Cinematic>();
        else Quest.Cinematic = Cinematic;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(TriggerType == Interaction.ENTER_COLLIDER)
        {
            Player player = col.gameObject.GetComponentInParent<Player>();
            if(player != null && player.Quest.Equals(Quest))
            {
                StartCoroutine(EndQuest(player));
            }
        }
    }

    private IEnumerator EndQuest(Player player)
    {
        yield return StartCoroutine(Quest.Cinematic.Play());
        player.NextQuest();
    }

}
