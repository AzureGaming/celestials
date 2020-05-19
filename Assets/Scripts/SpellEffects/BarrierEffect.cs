using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierEffect : SpellEffect {
    public override IEnumerator Activate() {
        Debug.Log("barrier effect activate");
        yield return StartCoroutine(base.Activate());
    }
}
