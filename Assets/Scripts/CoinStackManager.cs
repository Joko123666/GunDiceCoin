using UnityEngine;

public class CoinStackManager : MonoBehaviour {
    public static CoinStackManager Instance;

    private int playerCoins = 3;
    private int npcCoins = 3;

    private void Awake() {
        if (Instance == null) Instance = this;
    }

    public int GetPlayerStackCount() => playerCoins;
    public int GetNPCStackCount() => npcCoins;

    public void UsePlayerStack() {
        if (playerCoins > 0) playerCoins--;
    }

    public void UseNPCStack() {
        if (npcCoins > 0) npcCoins--;
    }

    public void ResetStacks(int count = 3) {
        playerCoins = count;
        npcCoins = count;
    }
}
