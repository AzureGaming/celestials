using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerate : CardEffect {
    BoardManager boardManager;
    Summoner summoner;

    private void Awake() {
        boardManager = FindObjectOfType<BoardManager>();
        summoner = FindObjectOfType<Summoner>();
    }

    public override IEnumerator Apply() {
        yield return StartCoroutine(AccelerateRoutine());
    }

    IEnumerator AccelerateRoutine() {
        // mve all units up 1 stage
        StartCoroutine(summoner.CastAccelerate());
        yield return StartCoroutine(boardManager.ResolveStagesRoutine());
        FindObjectOfType<AttackQueueManager>().RefreshIndicators(true);
    }
}
