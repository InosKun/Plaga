using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Line")]
public class DialogueLine : ScriptableObject
{
    [TextArea(2, 5)]
    public string lineText;

    public string speakerName;

    public Sprite portrait;

    [System.Serializable]
    public class Choice
    {
        public string choiceText;
        public DialogueLine nextLine;

        [Tooltip("If >= 0, this frame index will be applied after this choice.")]
        public int frameIndexToSet = -1; // -1 means no frame change
    }


    public Choice[] choices;
}
