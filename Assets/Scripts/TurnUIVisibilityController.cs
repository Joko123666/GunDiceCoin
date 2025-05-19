using UnityEngine;

public class TurnUIVisibilityController : MonoBehaviour {
    public GameObject playerTriggerStack;
    public GameObject npcTriggerStack;

    public void UpdateUI(bool isPlayerTurn) {
        if (playerTriggerStack) playerTriggerStack.SetActive(isPlayerTurn);
        if (npcTriggerStack) npcTriggerStack.SetActive(!isPlayerTurn);
    }
}
