using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierEffect : SpellEffect {
    public override IEnumerator Activate() {
        yield return StartCoroutine(base.Activate());
    }
}
