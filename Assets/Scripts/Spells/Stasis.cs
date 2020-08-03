using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stasis : CardEffect {
    Boss boss;
    Summoner summoner;
    // boss misses the next turn

    private void Awake() {
        boss = FindObjectOfType<Boss>();
        summoner = FindObjectOfType<Summoner>();
    }

    public override IEnumerator Apply() {
        yield return StartCoroutine(StasisRoutine());
    }

    IEnumerator StasisRoutine() {
        yield return StartCoroutine(summoner.CastStasis());
        boss.SkipTurn();
    }
}
