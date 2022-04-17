using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

public class ActionKinematic : MonoBehaviour
{    public enum Type { None, Dialogue }
    public Type ActionType = Type.None;

    private List<Dialogue> Dialogues;


    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(ActionKinematic))]
    public class ActionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ActionKinematic action = (ActionKinematic)target;

            EditorGUILayout.Space();
            switch(action.ActionType)
            {
                case Type.Dialogue:
                    EditorGUILayout.LabelField("Dialogue");
                    {
                        List<Dialogue> dialogues = action.Dialogues;
                        int size = Mathf.Max(0, EditorGUILayout.IntField("Size", dialogues.Count));
                        while (size > dialogues.Count)
                            dialogues.Add(null);
                        while (size < dialogues.Count)
                            dialogues.RemoveAt(dialogues.Count - 1);
                        for (int i = 0; i < dialogues.Count; i++)
                            dialogues[i] = EditorGUILayout.ObjectField("Element " + i, dialogues[i], typeof(Dialogue), true) as Dialogue;
                    }
                    break;
            }
        }
    }
#endif
    #endregion

}
