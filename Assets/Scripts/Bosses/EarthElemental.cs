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
        if (GetIsProtected()) {
            SetIsProtected(false);
        }
        yield return StartCoroutine(Attack());
    }

    public override IEnumerator TakeDamage(int damage) {
        if (GetIsProtected()) {
            yield break;
        } else {
            yield return StartCoroutine(base.TakeDamage(damage));
        }
    }

    public void OnAttackAnimationEnd() {
        attackRoutineRunning = false;
    }
    // rock throw => throw 2 rocks at 2 different lanes, hitting the first summon
    // lane block
    // block a tile for a turn

    protected override IEnumerator Attack() {
        int randomAttack = Random.Range(0, 5);
        ExecuteAttack(4);
        yield break;
    }

    void ExecuteAttack(int randomAttack) {
        attackRoutineRunning = true;
        if (randomAttack == 0) {
            StartCoroutine(PebbleStorm());
        } else if (randomAttack == 1) {
            StartCoroutine(BoulderDrop());
        } else if (randomAttack == 2) {
            RockThrow();
        } else if (randomAttack == 3) {
            CrystalBlock();
        } else if (randomAttack == 4) {
            StartCoroutine(Crystalize());
        }
    }

    IEnumerator PebbleStorm() {
        animator.SetTrigger("Attack1");
        cardManager.AddToDeck(Instantiate(pebbleStormCardPrefab).GetComponent<Card>());
        cardManager.AddToDeck(Instantiate(pebbleStormCardPrefab).GetComponent<Card>());
        yield return new WaitUntil(() => DoneActions());
        yield return StartCoroutine(cardManager.DrawToHand());
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

    IEnumerator Crystalize() {
        animator.SetTrigger("Attack3");
        yield return new WaitUntil(() => DoneActions());
        SetIsProtected(true);
    }

    bool DoneActions() {
        return attackRoutineRunning ? false : true;
    }

    bool DoneBoulderDrop() {
        return !gameManager.GetWaitForCompletion();
    }

    bool GetIsProtected() {
        return animator.GetBool("IsProtected");
    }

    void SetIsProtected(bool value) {
        animator.SetBool("IsProtected", value);
    }
}
