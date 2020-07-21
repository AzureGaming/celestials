using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEffect : SpellEffect {
    Summon summon;
    BoardManager boardManager;

    private void Awake() {
        summon = GetComponentInParent<Summon>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    public override IEnumerator Activate() {
        yield return StartCoroutine(base.Activate());
        Tile firstTile = boardManager.GetTile(0, summon.GetComponentInParent<Tile>().row);
        summon.Walk(firstTile);
        yield return new WaitUntil(() => summon.DoneMoving());
        gameObject.SetActive(false);
        base.Deactivate();
    }
}
