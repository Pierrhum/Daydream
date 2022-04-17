using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "QuestSystem/Quest")]
public class Quest : ScriptableObject
{
    public string Name;
    public string Description;
    public Quest Next;

    public QuestGiver Trigger;

    public List<Dialogue> Dialogues;

    public void End()
    {
        // Peut être associer une cinématique ou un dialogue ici
        Debug.Log(Name + " ended");
    }

    public Vector3 GetTriggerPos()
    {
        return Trigger.gameObject.transform.position;
    }
}

[System.Serializable]
public class Dialogue : Object
{
    public string Text;

    public Dialogue(string Text)
    {
        this.Text = Text;
    }
}
