// /Scripts/GameManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 이동 시 유지
        } else {
            Destroy(gameObject);
        }
    }

    public void EndGame(bool playerWon) {
        Debug.Log(playerWon ? "🎉 승리!" : "💀 패배...");
        // 추후 결과 씬 전환 예정
        SceneManager.LoadScene("StageSelectScene");
    }
}
