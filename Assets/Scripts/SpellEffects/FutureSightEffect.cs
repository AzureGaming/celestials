using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureSightEffect : SpellEffect {
    public override IEnumerator Activate() {
        gameObject.SetActive(true);
        StartCoroutine(base.Activate());
        yield break;
    }

    public override void Deactivate() {
        gameObject.SetActive(false);
    }
}
