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

    // summon is must have a unoccupied tile (boss tiles are unoccupied) to move to for march to be played on them
    // march triggers attack phase and movement phase
    IEnumerator MarchRoutine() {
        player.LoseMana(card.GetManaCost());
        boardManager.DetectSummons();
        Debug.Log("waiting...");
        yield return new WaitUntil(() => boardManager.GetQueue().Count == 1);
        Summon summon = boardManager.GetQueue()[0].GetSummon();
        boardManager.SetNeutral();
        summon.SetEffect(effectPrefab);
        summon.Walk();
        yield return new WaitUntil(() => summon.DoneMoving());
        summon.RemoveEffect();
        boardManager.ClearQueue();
        Destroy(gameObject);
        yield break;
    }
}
