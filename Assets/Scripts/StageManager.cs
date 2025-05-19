using UnityEngine;
using System;

public class StageManager : MonoBehaviour {
    public static StageManager Instance;

    public StageConfig currentConfig;
    public GunChamber chamber;
    public PlayerController player;
    public NPCController npc;
    public bool isPlayerTurn;
    public VisualEffectController visualFX;
    public LogSystem logSystem;
    public DialogueSet dialogueSet;

    private void Awake() {
        Instance = this;
    }

    public string GetNPCName() {
        return currentConfig != null ? currentConfig.OpponentName : "상대";
    }

    public string GetPlayerName() {
        return "당신";
    }

    public void StartStage(StageConfig config) {
        currentConfig = config;
        chamber.LoadBullet(config.MaxChamber, config.BulletCount);

        isPlayerTurn = UnityEngine.Random.value > 0.5f;
        visualFX.PlayChamberSpin();
        StartTurn();
    }

    public void StartTurn() {
        int triggerTokens = UnityEngine.Random.Range(
            currentConfig.MinTriggerPerTurn,
            currentConfig.MaxTriggerPerTurn + 1
        );

        if (isPlayerTurn)
            player.StartTurn(triggerTokens);
        else
            StartCoroutine(npc.StartTurnWithDelay(triggerTokens));
    }

    public void EndTurn() {
        if (player.money <= 0 || npc.money <= 0) {
            GameManager.Instance.EndGame(player.money > 0);
            return;
        }

        isPlayerTurn = !isPlayerTurn;
        visualFX.PlayChamberSpin();
        StartTurn();
    }
}
