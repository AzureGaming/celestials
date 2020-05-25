using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElemental : Boss {
    public GameObject boulderPrefab;
    public GameObject boulderSpawner;
    public GameObject pebbleStormCardPrefab;
    Summoner summoner;
    GameManager gameManager;
    BoardManager boardManager;
    CardManager cardManager;
    bool attackRoutineRunning = false;

    public override void Awake() {
        base.Awake();
        summoner = FindObjectOfType<Summoner>();
        gameManager = FindObjectOfType<GameManager>();
        cardManager = FindObjectOfType<CardManager>();
    }

    public override void Start() {
        base.Start();
    }

    public override IEnumerator RunTurnRoutine() {
        yield return StartCoroutine(Attack());
    }

    public void OnAttackAnimationEnd() {
        attackRoutineRunning = false;
    }
    // rock throw attacks
    // pebble storm -> gives deck 2 pebbles
    // boulder drop => drops boulder on pix, deals flat damage
    // rock throw => throw 2 rocks at 2 different lanes, hitting the first summon
    // lane block
    // block a tile for a turn
    // crystalize
    // does not take damage for the turn

    protected override IEnumerator Attack() {
        int randomAttack = Random.Range(0, 5);
        attackRoutineRunning = true;
        ExecuteAttack(0);
        yield break;
    }

    void ExecuteAttack(int randomAttack) {
        if (randomAttack == 0) {
            StartCoroutine(PebbleStorm());
        } else if (randomAttack == 1) {
            StartCoroutine(BoulderDrop());
        } else if (randomAttack == 2) {
            RockThrow();
        } else if (randomAttack == 3) {
            CrystalBlock();
        } else if (randomAttack == 4) {
            Crystalize();
        }
    }

    IEnumerator PebbleStorm() {
        animator.SetTrigger("Attack1");
        cardManager.AddToDeck(Instantiate(pebbleStormCardPrefab).GetComponent<Card>());
        cardManager.AddToDeck(Instantiate(pebbleStormCardPrefab).GetComponent<Card>());
        yield return new WaitUntil(() => DoneActions());
        yield return StartCoroutine(cardManager.HandleCardDraw());
    }

    IEnumerator BoulderDrop() {
        animator.SetTrigger("Attack1");
        gameManager.SetWaitForCompletion(true);
        Instantiate(boulderPrefab, boulderSpawner.transform);
        yield return new WaitUntil(() => DoneBoulderDrop());
        yield return new WaitUntil(() => DoneActions());
        summoner.TakeDamage();
    }

    void RockThrow() {
        animator.SetTrigger("Attack1");
    }

    void CrystalBlock() {
        animator.SetTrigger("Attack2");
    }

    void Crystalize() {
        animator.SetTrigger("Attack3");
    }

    bool DoneActions() {
        return attackRoutineRunning ? false : true;
    }

    bool DoneBoulderDrop() {
        return !gameManager.GetWaitForCompletion();
    }
}
