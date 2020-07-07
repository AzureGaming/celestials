using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronosGuardianController : SummonController {
    protected override IEnumerator DyingWish() {
        Debug.Log("dying wsh");
        boardManager.GetRandomSummonInStage(GetComponentInParent<Tile>().column).ActivateBarrier();
        yield break;
    }
}
