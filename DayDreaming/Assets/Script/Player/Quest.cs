using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string Name;
    public string Description;
    public Quest Next;

    public QuestGiver Trigger;

    public void End()
    {
        // Peut �tre associer une cin�matique ou un dialogue ici
        Debug.Log(Name + " ended");
    }

    public Vector3 GetTriggerPos()
    {
        return Trigger.gameObject.transform.position;
    }
}