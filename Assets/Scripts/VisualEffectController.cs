using UnityEngine;

public class VisualEffectController : MonoBehaviour {
    public Animator revolverAnimator;
    public GameObject coinFlipEffect;
    public GameObject diceRollEffect;

    public void PlayChamberSpin() {
        if (revolverAnimator) revolverAnimator.SetTrigger("Spin");
    }

    public void PlayCoinFlip() {
        if (coinFlipEffect) {
            coinFlipEffect.SetActive(false);
            coinFlipEffect.SetActive(true);
        }
    }

    public void PlayDiceRoll() {
        if (diceRollEffect) {
            diceRollEffect.SetActive(false);
            diceRollEffect.SetActive(true);
        }
    }
}