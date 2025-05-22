using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Line")]
public class DialogueLine : ScriptableObject
{
    [TextArea(2, 5)]
    public string lineText;

    public string speakerName;

    public Sprite portrait; // New field for expressions
}
