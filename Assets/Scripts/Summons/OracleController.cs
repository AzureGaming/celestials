using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OracleController : SummonController {
    CardManager cardManager;

    protected override void Awake() {
        base.Awake();
        cardManager = FindObjectOfType<CardManager>();
    }

    public override void UsePower() {
        powerRoutineRunning = true;
        StartCoroutine(PowerRoutine());
    }

    IEnumerator PowerRoutine() {
        // draw card
        yield return StartCoroutine(cardManager.DrawToHand());
        powerRoutineRunning = false;
    }
}
