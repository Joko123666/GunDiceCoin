// /Scripts/UIController.cs
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [Header("Player UI")]
    public Button btnShootSelf;
    public Button btnShootEnemy;
    public Button btnUseCoin;
    public GameObject playerTriggerStack;
    public GameObject playerCoinStack;
    public Text playerMoneyText;

    [Header("NPC UI")]
    public GameObject npcTriggerStack;
    public GameObject npcCoinStack;
    public Text npcMoneyText;

    [Header("Middle UI")]
    public Image diceResultImage;
    public Image coinResultImage;
    public Text potText;

    [Header("Chamber UI")]
    public Image[] bulletSlots;

    [Header("Log")]
    public Text logText;

    public void ToggleTriggerUI(bool isPlayerTurn) {
        playerTriggerStack.SetActive(isPlayerTurn);
        npcTriggerStack.SetActive(!isPlayerTurn);
    }

    public void UpdateMoney(bool isPlayer, int amount) {
        if (isPlayer) playerMoneyText.text = $"₩ {amount}";
        else npcMoneyText.text = $"₩ {amount}";
    }

    public void AddLog(string text) {
        logText.text += "\n" + text;
    }

    public void SetPot(int amount) {
        potText.text = $"판돈: ₩ {amount}";
    }

    public void SetDiceResult(Sprite result) {
        diceResultImage.sprite = result;
    }

    public void SetCoinResult(Sprite result) {
        coinResultImage.sprite = result;
    }
}
