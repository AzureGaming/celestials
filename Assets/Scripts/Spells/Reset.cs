using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : CardEffect {
    BoardManager boardManager;
    // move a celestial back to beginning of lane

    private void Awake() {
        boardManager = FindObjectOfType<BoardManager>();
    }

    public override IEnumerator Apply() {
        yield return StartCoroutine(ResetRoutine());
    }

    IEnumerator ResetRoutine() {
        boardManager.DetectSummons();
        yield return new WaitUntil(() => boardManager.GetQueue().Count == 1);
        Summon summon = boardManager.GetQueue()[0].GetSummon();
        boardManager.SetNeutral();
        summon.ActivateReset();
        boardManager.ClearQueue();
    }
}