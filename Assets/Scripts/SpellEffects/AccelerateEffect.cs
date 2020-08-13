using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateEffect : SpellEffect {
    public override IEnumerator Activate() {
        gameObject.SetActive(true);
        StartCoroutine(base.Activate());
        yield break;
    }

    public override void Deactivate() {
        base.Deactivate();
        gameObject.SetActive(false);
    }

    public bool IsDone() {
        return gameObject.activeSelf ? false : true;
    }
}
