using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPile : CardEffect {
    Summoner summoner;
    Player player;
    // boss misses the next turn

    private void Awake() {
        summoner = FindObjectOfType<Summoner>();
        player = FindObjectOfType<Player>();
    }

    public override IEnumerator Apply() {
        yield return StartCoroutine(StockPileRoutine());
    }

    IEnumerator StockPileRoutine() {
        yield return StartCoroutine(summoner.CastStockPile());
        player.SetReserveMana(player.GetMana());
    }
}
