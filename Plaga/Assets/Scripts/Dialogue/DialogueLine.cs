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
    }

    public Choice[] choices;
}
