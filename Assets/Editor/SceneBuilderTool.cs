#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SceneBuilderTool {
    [MenuItem("Tools/Generate Base Game Scene")] 
    public static void GenerateScene() {
        GameObject root = new GameObject("GameManager");

        var stageManager = root.AddComponent<StageManager>();
        var connector = root.AddComponent<AutoConnector>();

        var chamber = new GameObject("GunChamber").AddComponent<GunChamber>();
        var logGO = new GameObject("LogSystem");
        var logSystem = logGO.AddComponent<LogSystem>();
        var logText = new GameObject("LogText").AddComponent<Text>();
        logText.transform.SetParent(logGO.transform);
        logSystem.logText = logText;

        var fx = new GameObject("VisualEffectController").AddComponent<VisualEffectController>();

        var coinStackMgr = new GameObject("CoinStackManager").AddComponent<CoinStackManager>();

        var playerGO = new GameObject("Player");
        playerGO.AddComponent<AudioSource>();
        var player = playerGO.AddComponent<PlayerController>();

        var npcGO = new GameObject("NPC");
        npcGO.AddComponent<AudioSource>();
        npcGO.AddComponent<Animator>();
        var npc = npcGO.AddComponent<NPCController>();

        stageManager.chamber = chamber;
        stageManager.player = player;
        stageManager.npc = npc;
        stageManager.visualFX = fx;
        stageManager.logSystem = logSystem;

        connector.stageManager = stageManager;
        connector.chamber = chamber;
        connector.fx = fx;
        connector.player = player;
        connector.npc = npc;
        connector.log = logSystem;
        connector.dialogueSet = null;

        Debug.Log("✅ 기본 게임 오브젝트 구조가 생성되었습니다. 필요시 UI/Prefab 연결만 추가하세요.");
    }
}
#endif
