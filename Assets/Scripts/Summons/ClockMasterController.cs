using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockMasterController : SummonController {
    protected override void Awake() {
        base.Awake();
    }

    public override void UsePower() {
        powerRoutineRunning = true;
        StartCoroutine(PowerRoutine());
    }

    IEnumerator PowerRoutine() {
        Tile parent = GetComponentInParent<Tile>();
        Tile upTile = boardManager.GetTile(parent.column, parent.row - 1);
        Tile botTile = boardManager.GetTile(parent.column, parent.row + 1);
        if (upTile) {
            yield return StartCoroutine(boardManager.ResolveTurnForSummon(upTile.GetComponentInChildren<Summon>()));
        }
        if (botTile) {
            yield return StartCoroutine(boardManager.ResolveTurnForSummon(botTile.GetComponentInChildren <Summon>()));
        }
        //move adjacent summons 1 stage forward
        powerRoutineRunning = false;
    }
}
