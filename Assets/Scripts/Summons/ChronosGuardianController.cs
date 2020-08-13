using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronosGuardianController : SummonController {
    protected override IEnumerator DyingWish() {
        boardManager.GetRandomSummonInStage(GetComponentInParent<Tile>().column).ActivateBarrier();
        yield break;
    }
}
