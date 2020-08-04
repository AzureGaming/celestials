using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeerController : SummonController {
    CardManager cardManager;

    protected override void Awake() {
        base.Awake();
        cardManager = FindObjectOfType<CardManager>();
    }

    public override void UseHowl() {
        base.UseHowl();
        StartCoroutine(Howl());
    }

    IEnumerator Howl() {
        yield return StartCoroutine(cardManager.DrawToHand());
        howlRoutineRunning = false;
    }
}
