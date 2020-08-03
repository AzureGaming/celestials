using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisEffect : SpellEffect {
    public override IEnumerator Activate() {
        gameObject.SetActive(true);
        yield break;
    }

    public override void Deactivate() {
        gameObject.SetActive(false);
    }

    public bool IsDone() {
        return gameObject.activeSelf ? false : true;
    }
}
