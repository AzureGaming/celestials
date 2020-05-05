using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class March : CardEffect {
    BoardManager boardManager;
    Card card;
    Player player;

    private void Awake() {
        card = GetComponent<Card>();
        player = FindObjectOfType<Player>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    override public IEnumerator Apply() {
        player.LoseMana(card.GetManaCost());
        boardManager.DetectSummons();
        yield return new WaitUntil(() => boardManager.GetQueue().Count == 1);
        boardManager.SetNeutral();
        //boardManager.AdvanceSummon(boardManager.GetQueue()[0].GetComponent<Summon>());
        boardManager.ClearQueue();
    }
}
