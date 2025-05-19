using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public int money;
    public int betAmount = 100;
    public Animator opponentAnimator; // 상대방 애니메이터
    public AudioSource audioSource;
    public AudioClip heartbeatClip;
    public AudioClip breathingClip;
    public AudioClip gunshotClip;
    public AudioClip sighClip;
    public AudioClip coinClip;

    public Transform triggerTokenContainer; // 트리거 토큰 UI 부모 오브젝트
    public GameObject triggerTokenPrefab; // 토큰 프리팹

    public Transform coinStackContainer; // 코인 스택 UI 부모 오브젝트
    public GameObject coinStackPrefab; // 코인 스택 프리팹

    public Text potText; // 판돈 표시 텍스트

    private int triggerTokens;
    private int coinStack = 3; // 기본 코인 스택 수
    private int pot = 0; // 현재 판돈

    public AudioClip coinFailClip;


    // 플레이어 턴 시작
    public void StartTurn(int tokens) {
        triggerTokens = tokens;
        UpdateTriggerTokenUI();
    }

    // 트리거 토큰 UI 업데이트
    private void UpdateTriggerTokenUI() {
        foreach (Transform child in triggerTokenContainer)
            Destroy(child.gameObject);

        for (int i = 0; i < triggerTokens; i++) {
            Instantiate(triggerTokenPrefab, triggerTokenContainer);
        }
    }

    // 코인 스택 UI 초기화
    public void InitCoinStacks() {
        foreach (Transform child in coinStackContainer)
            Destroy(child.gameObject);

        for (int i = 0; i < coinStack; i++) {
            Instantiate(coinStackPrefab, coinStackContainer);
        }
    }

    // 판돈 UI 업데이트
    private void UpdatePotUI() {
        potText.text = $"₩ {pot}";
    }

    // 상대방에게 총을 겨눌 때 호출
    public void AimAtOpponent() {
        opponentAnimator.SetTrigger("Tension"); // 긴장 애니메이션 실행
        AttemptTrigger(isTargetOpponent: true);
    }

    // 자신에게 총을 겨눌 때 호출
    public void AimAtSelf() {
        audioSource.PlayOneShot(heartbeatClip); // 심장 소리
        audioSource.PlayOneShot(breathingClip); // 호흡 소리
        AttemptTrigger(isTargetOpponent: false);
    }

    // 트리거를 당기는 시도
    private void AttemptTrigger(bool isTargetOpponent) {
        if (triggerTokens <= 0) return;

        bool fired = StageManager.Instance.chamber.Fire();
        triggerTokens--;
        UpdateTriggerTokenUI();

        if (fired) {
            audioSource.PlayOneShot(gunshotClip); // 총성 효과음
            GameManager.Instance.EndGame(false); // 플레이어 사망 처리
        } else {
            StartCoroutine(TriggerSuccessEffect(isTargetOpponent));
        }

        if (triggerTokens == 0)
            StageManager.Instance.EndTurn();
    }
    
    //코인 사용 연출용 함수
    public void UseCoinStack() {
    if (CoinStackManager.Instance.GetPlayerStackCount() <= 0) return;

    CoinStackManager.Instance.UsePlayerStack();
    audioSource.PlayOneShot(coinClip);
    StageManager.Instance.visualFX.PlayCoinFlip();
    StageManager.Instance.logSystem.AddEntry(StageManager.Instance.dialogueSet.RandomLine(StageManager.Instance.dialogueSet.coinFlip));

    bool coinHeads = Random.value > 0.5f;
    bool fired = StageManager.Instance.chamber.Fire();
    triggerTokens--;

    if (fired && coinHeads) {
        audioSource.PlayOneShot(gunshotClip);
        StageManager.Instance.logSystem.AddEntry(StageManager.Instance.dialogueSet.RandomLine(StageManager.Instance.dialogueSet.death));
        GameManager.Instance.EndGame(false);
    } else {
        audioSource.PlayOneShot(coinHeads ? sighClip : coinFailClip);
        StageManager.Instance.logSystem.AddEntry(
            coinHeads
            ? StageManager.Instance.dialogueSet.RandomLine(StageManager.Instance.dialogueSet.playerSafe)
            : StageManager.Instance.dialogueSet.RandomLine(StageManager.Instance.dialogueSet.coinFail)
        );
    }

    if (triggerTokens <= 0)
        StageManager.Instance.EndTurn();
}


    // 발사 실패 효과 처리 + 베팅 반영
    private IEnumerator TriggerSuccessEffect(bool isTargetOpponent) {
        audioSource.PlayOneShot(sighClip); // 휴~ 하는 소리
        yield return new WaitForSeconds(0.5f);

        audioSource.PlayOneShot(coinClip); // 코인 효과음

        if (isTargetOpponent) {
            money += betAmount * 2;
            StageManager.Instance.npc.money -= betAmount * 2;
            pot += betAmount * 2;
        } else {
            StageManager.Instance.npc.money += betAmount;
            money -= betAmount;
            pot += betAmount;
        }

        UpdatePotUI();
    }
}