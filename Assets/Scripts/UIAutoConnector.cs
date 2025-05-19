// /Scripts/UIAutoConnector.cs
using UnityEngine;
using UnityEngine.UI;

public class UIAutoConnector : MonoBehaviour {
    public UIController ui;

    public Button btnShootSelf;
    public Button btnShootEnemy;
    public Button btnUseCoin;
    public GameObject playerTriggerStack;
    public GameObject playerCoinStack;
    public Text playerMoneyText;

    public GameObject npcTriggerStack;
    public GameObject npcCoinStack;
    public Text npcMoneyText;

    public Image diceResultImage;
    public Image coinResultImage;
    public Text potText;

    public Image[] bulletSlots;
    public Text logText;

    [ContextMenu("Apply UI Connections")]
    public void Apply() {
        if (ui == null) ui = GetComponent<UIController>();

        ui.btnShootSelf = btnShootSelf;
        ui.btnShootEnemy = btnShootEnemy;
        ui.btnUseCoin = btnUseCoin;

        ui.playerTriggerStack = playerTriggerStack;
        ui.playerCoinStack = playerCoinStack;
        ui.playerMoneyText = playerMoneyText;

        ui.npcTriggerStack = npcTriggerStack;
        ui.npcCoinStack = npcCoinStack;
        ui.npcMoneyText = npcMoneyText;

        ui.diceResultImage = diceResultImage;
        ui.coinResultImage = coinResultImage;
        ui.potText = potText;

        ui.bulletSlots = bulletSlots;
        ui.logText = logText;

        Debug.Log("✅ UIController 연결이 완료되었습니다.");
    }
} // end