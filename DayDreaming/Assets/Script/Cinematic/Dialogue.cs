using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "QuestSystem/Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<Talk> talks;

    [System.Serializable]
    public class Talk
    {
        public string Text;
        public Sprite sprite;
    }
}
