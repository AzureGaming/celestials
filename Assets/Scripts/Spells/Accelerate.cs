using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerate : CardEffect {
    BoardManager boardManager;

    private void Awake() {
        boardManager = FindObjectOfType<BoardManager>();
    }

    public override IEnumerator Apply() {
        yield return StartCoroutine(AccelerateRoutine());
    }

    IEnumerator AccelerateRoutine() {
        // mve all units up 1 stage
        yield return StartCoroutine(boardManager.ResolveStagesRoutine());
    }
}
