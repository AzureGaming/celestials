using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPileEffect : SpellEffect {
    public override IEnumerator Activate() {
        gameObject.SetActive(true);
        yield break;
    }

    public override void Deactivate() {
        gameObject.SetActive(false);
    }
}
