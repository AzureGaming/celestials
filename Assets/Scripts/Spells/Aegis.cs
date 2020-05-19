using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aegis : CardEffect {
    BoardManager boardManager;

    private void Awake() {
        boardManager = FindObjectOfType<BoardManager>();
    }
    public override void Apply() {
        StartCoroutine(AegisRoutine());
    }

    IEnumerator AegisRoutine() {
        boardManager.DetectSummons();
        yield return new WaitUntil(() => boardManager.GetQueue().Count == 1);
        Summon summon = boardManager.GetQueue()[0].GetSummon();
        boardManager.SetNeutral();
        summon.ActivateBarrier();
        boardManager.ClearQueue();
    }
}
