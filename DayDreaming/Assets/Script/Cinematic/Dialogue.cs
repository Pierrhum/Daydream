using MyBox;
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
        public enum SpriteDisplay { Left, Right, Both }

        public string Text;
        public SpriteDisplay Display = SpriteDisplay.Left;
        public Sprite sprite;
        [ConditionalField(nameof(Display), false, SpriteDisplay.Both)]
        public Sprite Right;
    }
}
