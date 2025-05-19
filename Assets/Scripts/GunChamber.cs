using UnityEngine;
using System.Collections.Generic;

public class GunChamber : MonoBehaviour {
    private List<bool> chambers;
    private int currentIndex = 0;

    public int RemainingChambers => chambers.Count;
    public int RemainingBullets => chambers.Count;

    // 총알을 무작위로 리볼버에 장전
    public void LoadBullet(int maxChambers, int bulletCount) {
        chambers = new List<bool>(new bool[maxChambers]);

        for (int i = 0; i < bulletCount; i++) {
            int pos;
            do {
                pos = Random.Range(0, maxChambers);
            } while (chambers[pos]);

            chambers[pos] = true;
        }

        currentIndex = Random.Range(0, maxChambers); // 스핀 효과
    }

    // 발사 결과 반환 (true면 탄 발사됨)
    public bool Fire() {
        bool result = chambers[currentIndex];
        currentIndex = (currentIndex + 1) % chambers.Count;
        return result;
    }

    // 리볼버가 비었는지 확인
    public bool IsEmpty() {
        return !chambers.Contains(true);
    }
}