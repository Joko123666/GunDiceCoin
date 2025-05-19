using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {
    public int money;
    public int betAmount = 100;
    public int riskLevel = 5; // 1~10 위험 성향
    public AudioSource audioSource;
    public AudioClip heartbeatClip;
    public AudioClip breathingClip;
    public AudioClip gunshotClip;
    public AudioClip sighClip;
    public AudioClip coinClip;
    public AudioClip diceClip;
    public AudioClip coinFailClip;

    private int triggerTokens;
    private int pot = 0;

    // 탄이 발사될 확률 계산
    private float CalculateBulletChance() {
        int remainingChambers = StageManager.Instance.chamber.RemainingChambers;
        int remainingBullets = StageManager.Instance.chamber.RemainingBullets;
        return (float)remainingBullets / remainingChambers;
    }

    // 코인스택 고려한 공격 성향 가중치 적용
    private float AdjustedAggressiveThreshold() {
        int selfCoins = CoinStackManager.Instance.GetNPCStackCount();
        int playerCoins = CoinStackManager.Instance.GetPlayerStackCount();

        float adjustment = Mathf.Clamp01((float)(playerCoins - selfCoins) / 3f);
        return riskLevel * 0.1f + adjustment * 0.2f; // 최대 +20% 가중치
    }

    // NPC 턴 시작 (딜레이 포함)
    public IEnumerator StartTurnWithDelay(int tokens) {
        yield return new WaitForSeconds(Random.Range(1f, 2.5f));
        triggerTokens = tokens;
        StageManager.Instance.visualFX.PlayDiceRoll(); // 주사위 굴리는 효과
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ExecuteTurn());
    }

    // NPC 턴 로직
    private IEnumerator ExecuteTurn() {
        while (triggerTokens > 0) {
            yield return new WaitForSeconds(Random.Range(0.8f, 1.5f));

            float bulletChance = CalculateBulletChance();
            float threshold = AdjustedAggressiveThreshold();

            bool hasCoinStack = CoinStackManager.Instance.GetNPCStackCount() > 0;

            if (bulletChance >= 0.5f && hasCoinStack && Random.value < 0.5f) {
                CoinStackManager.Instance.UseNPCStack();
                audioSource.PlayOneShot(coinClip);
                StageManager.Instance.visualFX.PlayCoinFlip(); // 코인 던지는 효과
                yield return new WaitForSeconds(0.8f);

                bool coinHeads = Random.value > 0.5f;
                bool fired = StageManager.Instance.chamber.Fire();
                triggerTokens--;

                if (fired && coinHeads) {
                    audioSource.PlayOneShot(gunshotClip);
                    GameManager.Instance.EndGame(true);
                    yield break;
                } else {
                    audioSource.PlayOneShot(coinHeads ? sighClip : coinFailClip);
                    yield return new WaitForSeconds(0.5f);
                    continue;
                }
            }

            bool targetOpponent = bulletChance < threshold;

            bool firedNormal = StageManager.Instance.chamber.Fire();
            triggerTokens--;

            if (firedNormal) {
                audioSource.PlayOneShot(gunshotClip);
                GameManager.Instance.EndGame(true);
                yield break;
            } else {
                audioSource.PlayOneShot(sighClip);
                yield return new WaitForSeconds(0.5f);
                audioSource.PlayOneShot(coinClip);

                if (targetOpponent) {
                    money += betAmount * 2;
                    StageManager.Instance.player.money -= betAmount * 2;
                    pot += betAmount * 2;
                } else {
                    StageManager.Instance.player.money += betAmount;
                    money -= betAmount;
                    pot += betAmount;
                }
            }
        }

        StageManager.Instance.EndTurn();
    }
}
