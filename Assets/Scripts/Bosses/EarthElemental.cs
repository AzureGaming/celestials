using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElemental : Boss {
    public GameObject boulderPrefab;
    public GameObject boulderSpawner;
    Summoner summoner;
    GameManager gameManager;
    bool attackRoutineRunning = false;

    public override void Awake() {
        base.Awake();
        summoner = FindObjectOfType<Summoner>();
        gameManager = FindObjectOfType<GameManager>();
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
        ExecuteAttack(1);
        yield break;
    }

    void ExecuteAttack(int randomAttack) {
        if (randomAttack == 0) {
            PebbleStorm();
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

    void PebbleStorm() {
        animator.SetTrigger("Attack1");
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
