using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSet", menuName = "Roulette/Dialogue Set", order = 0)]
public class DialogueSet : ScriptableObject {
    [TextArea] public string[] playerSelfAim;
    [TextArea] public string[] playerSafe;
    [TextArea] public string[] npcSelfAim;
    [TextArea] public string[] npcToPlayer;
    [TextArea] public string[] coinFlip;
    [TextArea] public string[] coinFail;
    [TextArea] public string[] death;

    public string RandomLine(string[] lines) => lines.Length > 0 ? lines[Random.Range(0, lines.Length)] : "";
}