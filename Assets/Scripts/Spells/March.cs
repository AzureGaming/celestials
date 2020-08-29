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
        yield return StartCoroutine(MarchRoutine());
    }

    IEnumerator MarchRoutine() {
        player.LoseMana(card.GetManaCost());
        boardManager.DetectMoveableSummons();
        yield return new WaitUntil(() => boardManager.GetQueue().Count == 1);
        Summon summon = boardManager.GetQueue()[0].GetSummon();
        boardManager.SetNeutral();
        yield return StartCoroutine(summon.ActivateMarch());
        FindObjectOfType<AttackQueueManager>().RefreshIndicators(true);
        boardManager.ClearQueue();
        yield break;
    }
}
