using UnityEngine;

public class AutoConnector : MonoBehaviour {
    public StageManager stageManager;
    public PlayerController player;
    public NPCController npc;
    public GunChamber chamber;
    public VisualEffectController fx;
    public LogSystem log;
    public DialogueSet dialogueSet;

    public void Connect() {
        stageManager.player = player;
        stageManager.npc = npc;
        stageManager.chamber = chamber;
        stageManager.visualFX = fx;
        stageManager.logSystem = log;
        stageManager.dialogueSet = dialogueSet;
    }

    private void Reset() {
        Connect();
    }
}