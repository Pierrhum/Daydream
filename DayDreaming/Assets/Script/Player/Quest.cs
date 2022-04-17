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

    public Cinematic Cinematic;

    public Vector3 GetTriggerPos()
    {
        return Trigger.gameObject.transform.position;
    }
}
