using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureSightEffect : SpellEffect {
    public SpriteRenderer spriteRenderer;

    public override IEnumerator Activate() {
        gameObject.SetActive(true);
        StartCoroutine(base.Activate());
        yield break;
    }

    public override void Deactivate() {
        StartCoroutine(DeactivateRoutine());
    }

    IEnumerator DeactivateRoutine() {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(1.25f);
        base.Deactivate();
        spriteRenderer.enabled = true;
    }
}
