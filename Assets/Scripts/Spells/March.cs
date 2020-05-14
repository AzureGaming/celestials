using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class March : CardEffect {
    public GameObject effectPrefab;
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
        boardManager.DetectMoveableSummons();
        yield return new WaitUntil(() => boardManager.GetQueue().Count == 1);
        Summon summon = boardManager.GetQueue()[0].GetSummon();
        boardManager.SetNeutral();
        summon.SetEffect(effectPrefab);
        summon.Attack();
        yield return new WaitUntil(() => summon.DoneAttacking());
        summon.Walk();
        yield return new WaitUntil(() => summon.DoneMoving());
        summon.RemoveEffect();
        boardManager.ClearQueue();
        Destroy(gameObject);
        yield break;
    }
}
