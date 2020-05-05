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

    override public void Apply() {
        StartCoroutine(MarchRoutine());
    }

    IEnumerator MarchRoutine() {
        player.LoseMana(card.GetManaCost());
        boardManager.DetectSummons();
        Debug.Log("waiting...");
        yield return new WaitUntil(() => boardManager.GetQueue().Count == 1);
        boardManager.SetNeutral();
        //boardManager.AdvanceSummon(boardManager.GetQueue()[0].GetComponent<Summon>());
        boardManager.ClearQueue();
        Destroy(gameObject);
        yield break;
    }
}
