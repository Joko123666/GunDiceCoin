// /Editor/GamePrefabGenerator.cs
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GamePrefabGenerator {
    [MenuItem("Tools/Create Game UI Prefabs")] 
    public static void GeneratePrefabs() {
        string folderPath = "Assets/Prefabs/Generated";
        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets/Prefabs", "Generated");

        // TriggerTokenPrefab
        GameObject trigger = new GameObject("TriggerToken", typeof(Image));
        trigger.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
        SavePrefab(trigger, folderPath + "/TriggerTokenPrefab.prefab");

        // CoinStackPrefab
        GameObject coin = new GameObject("CoinStack", typeof(Image));
        coin.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
        SavePrefab(coin, folderPath + "/CoinStackPrefab.prefab");

        Debug.Log("✅ 프리팹 생성 완료: TriggerToken, CoinStack");
    }

    private static void SavePrefab(GameObject go, string path) {
        PrefabUtility.SaveAsPrefabAsset(go, path);
        GameObject.DestroyImmediate(go);
    }
}
#endif

// /Editor/StageConfigGenerator.cs
#if UNITY_EDITOR
//using UnityEngine;
//using UnityEditor;

[CreateAssetMenu(fileName = "StageConfig", menuName = "Roulette/StageConfig", order = 1)]
public class StageConfig : ScriptableObject {
    public string OpponentName;
    public int MaxChamber;
    public int BulletCount;
    public int MinTriggerPerTurn;
    public int MaxTriggerPerTurn;
    public int InitialMoney;
}

public class StageConfigGenerator {
    [MenuItem("Tools/Create Sample StageConfig")]
    public static void GenerateConfig() {
        StageConfig config = ScriptableObject.CreateInstance<StageConfig>();
        config.OpponentName = "로이";
        config.MaxChamber = 6;
        config.BulletCount = 1;
        config.MinTriggerPerTurn = 2;
        config.MaxTriggerPerTurn = 6;
        config.InitialMoney = 1000;

        AssetDatabase.CreateAsset(config, "Assets/StageConfig_로이.asset");
        AssetDatabase.SaveAssets();
        Selection.activeObject = config;

        Debug.Log("✅ StageConfig_로이.asset 생성 완료");
    }
}
#endif
