#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class DialogueSetGenerator {
    [MenuItem("Tools/Create Sample DialogueSet")]
    public static void CreateDialogueSet() {
        var asset = ScriptableObject.CreateInstance<DialogueSet>();

        asset.playerSelfAim = new[] {
            "당신은 숨을 죽이며 총구를 자신의 이마에 댔다.",
            "당신의 손끝이 떨리기 시작했다..."
        };

        asset.playerSafe = new[] {
            "총성이 울리지 않았다. 당신은 안도의 숨을 내쉰다.",
            "그 순간의 공포는 허무하게 지나갔다."
        };

        asset.npcSelfAim = new[] {
            "상대는 자신의 머리에 총구를 들이댔다.",
            "그는 미동 없이 방아쇠를 당겼다."
        };

        asset.npcToPlayer = new[] {
            "상대는 당신을 바라보며 방아쇠를 당겼다!",
            "공포가 당신을 덮쳐왔다." 
        };

        asset.coinFlip = new[] {
            "코인은 하늘을 가르며 회전했다...",
            "운명이 담긴 동전이 천천히 떨어졌다."
        };

        asset.coinFail = new[] {
            "뒷면이 나왔다... 그의 표정이 굳어졌다.",
            "운이 따라주지 않았다."
        };

        asset.death = new[] {
            "총성이 울리며, 모든 것이 끝났다.",
            "피가 튀며 쓰러졌다." 
        };

        AssetDatabase.CreateAsset(asset, "Assets/DialogueSet.asset");
        AssetDatabase.SaveAssets();
        Selection.activeObject = asset;
    }
}
#endif