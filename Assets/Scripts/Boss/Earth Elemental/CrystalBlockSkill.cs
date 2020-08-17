using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBlockSkill : MonoBehaviour {
    public EarthElementalSkillIndicators skillIndicators;
    public AttackQueueManager queueManager;
    public Animator animator;
    CardManager cardManager;
    GameManager gameManager;
    public GameObject crystal;
    Summoner summoner;
    BoardManager boardManager;

    bool isCasting;
    GameObject instance;

    private void Awake() {
        cardManager = FindObjectOfType<CardManager>();
        gameManager = FindObjectOfType<GameManager>();
        summoner = FindObjectOfType<Summoner>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    public void QueueSkill() {
        skillIndicators.SetBlockingCrystal();
        queueManager.Queue(new AttackQueueManager.AttackCommand(EarthElemental.Moves.CRYSTALBLOCK));
    }

    public void OnCastAnimationEnd() {
        isCasting = false;
    }

    public IEnumerator CastSkill() {
        isCasting = true;
        animator.SetTrigger("Attack2");
        yield return new WaitUntil(() => !isCasting);
        yield return StartCoroutine(CrystalBlock());
        skillIndicators.ClearIndicator();
    }

    IEnumerator CrystalBlock() {
        List<Tile> validTiles = boardManager.GetSummonableTiles();
        int randomIndex = Random.Range(0, validTiles.Count);
        Tile tileToBlock = validTiles[randomIndex];
        instance = Instantiate(crystal, tileToBlock.transform);
        BlockingCrystal blockingCrystal = instance.GetComponent<BlockingCrystal>();
        yield return new WaitUntil(() => !blockingCrystal.GetIsSpawning());
    }
}
