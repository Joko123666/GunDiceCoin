// /Editor/UICanvasBuilder.cs
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UICanvasBuilder {
    [MenuItem("Tools/Build Game UI Canvas")] 
    public static void BuildCanvas() {
        GameObject canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        var canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // 좌상단 (상대 인터페이스)
        GameObject npcPanel = CreateUIGroup("NPCPanel", canvasGO.transform, new Vector2(0, 1), new Vector2(0, 1), new Vector2(10, -10));
        var npcMoneyText = CreateText("NPCMoneyText", npcPanel.transform, "₩ 9999");
        GameObject npcCoinStack = CreateUIGroup("NPCCoinStack", npcPanel.transform);
        GameObject npcTriggerStack = CreateUIGroup("NPCTriggerStack", npcPanel.transform);

        // 우상단 (탄창)
        GameObject chamberPanel = CreateUIGroup("ChamberPanel", canvasGO.transform, new Vector2(1, 1), new Vector2(1, 1), new Vector2(-10, -10));
        for (int i = 0; i < 6; i++) CreateImage("BulletSlot" + i, chamberPanel.transform);

        // 중앙 좌측 (주사위 + 코인 결과)
        GameObject diceCoinPanel = CreateUIGroup("DiceCoinPanel", canvasGO.transform, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector2(10, 0));
        CreateImage("DiceResult", diceCoinPanel.transform);
        CreateImage("CoinResult", diceCoinPanel.transform);

        // 하단 중앙 (플레이어 버튼 영역)
        GameObject playerActionPanel = CreateUIGroup("PlayerActionPanel", canvasGO.transform, new Vector2(0.5f, 0), new Vector2(0.5f, 0), new Vector2(0, 10));
        var btnSelf = CreateButton("BtnSelf", playerActionPanel.transform, "자신 겨누기");
        var btnOpponent = CreateButton("BtnOpponent", playerActionPanel.transform, "상대 겨누기");
        var btnCoin = CreateButton("BtnCoin", playerActionPanel.transform, "코인 던지기");

        // 우하단 (플레이어 인터페이스)
        GameObject playerPanel = CreateUIGroup("PlayerPanel", canvasGO.transform, new Vector2(1, 0), new Vector2(1, 0), new Vector2(-10, 10));
        var playerMoneyText = CreateText("PlayerMoneyText", playerPanel.transform, "₩ 1234");
        GameObject playerCoinStack = CreateUIGroup("PlayerCoinStack", playerPanel.transform);
        GameObject playerTriggerStack = CreateUIGroup("PlayerTriggerStack", playerPanel.transform);

        // 좌하단 (로그창)
        GameObject logPanel = CreateUIGroup("LogBox", canvasGO.transform, new Vector2(0, 0), new Vector2(0, 0), new Vector2(10, 10));
        var logText = CreateText("LogText", logPanel.transform, "로그 출력");
        logText.alignment = TextAnchor.LowerLeft;
        logText.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 200);

        // 자동 컴포넌트 붙이기
        var logSystem = canvasGO.AddComponent<LogSystem>();
        logSystem.logText = logText;

        var playerUI = canvasGO.AddComponent<PlayerUI>();
        playerUI.btnShootSelf = btnSelf;
        playerUI.btnShootEnemy = btnOpponent;
        playerUI.btnUseCoin = btnCoin;

        var uiVisibility = canvasGO.AddComponent<TurnUIVisibilityController>();
        uiVisibility.playerTriggerStack = playerTriggerStack;
        uiVisibility.npcTriggerStack = npcTriggerStack;

        Debug.Log("✅ Canvas UI 구조 + 스크립트 자동 연결 + 턴 전환 트리거 UI 연결 완료");
    }

    private static GameObject CreateUIGroup(string name, Transform parent, Vector2 anchorMin = default, Vector2 anchorMax = default, Vector2 anchoredPos = default) {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent);
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin == default ? Vector2.zero : anchorMin;
        rt.anchorMax = anchorMax == default ? Vector2.zero : anchorMax;
        rt.pivot = anchorMin == default ? Vector2.zero : anchorMin;
        rt.anchoredPosition = anchoredPos;
        rt.sizeDelta = new Vector2(200, 100);
        return go;
    }

    private static Text CreateText(string name, Transform parent, string content) {
        GameObject go = new GameObject(name, typeof(Text));
        go.transform.SetParent(parent);
        var txt = go.GetComponent<Text>();
        txt.text = content;
        txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        txt.fontSize = 18;
        txt.color = Color.black;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.horizontalOverflow = HorizontalWrapMode.Overflow;
        txt.verticalOverflow = VerticalWrapMode.Overflow;
        txt.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
        return txt;
    }

    private static GameObject CreateImage(string name, Transform parent) {
        GameObject go = new GameObject(name, typeof(Image));
        go.transform.SetParent(parent);
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
        return go;
    }

    private static Button CreateButton(string name, Transform parent, string label) {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Button), typeof(Image));
        go.transform.SetParent(parent);
        var btn = go.GetComponent<Button>();
        var txt = CreateText("Text", go.transform, label);
        txt.alignment = TextAnchor.MiddleCenter;
        txt.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        return btn;
    }
}
#endif
