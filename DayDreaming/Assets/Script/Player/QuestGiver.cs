using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest Quest;
    public enum Interaction { ENTER_COLLIDER, TALK, KILL};

    public Interaction TriggerType;




    private void Awake()
    {
        Quest.Trigger = this;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(TriggerType == Interaction.ENTER_COLLIDER)
        {
            Player player = col.gameObject.GetComponentInParent<Player>();
            if(player != null && player.Quest.Equals(Quest))
            {
                Quest.End();
                player.NextQuest();
            }
        }
    }

}
