using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EarthElemental : Boss {
    public enum Moves {
        PEBBLESTORM,
        BOULDERDROP,
        ROCKTHROW,
        CRYSTALBLOCK,
        CRYSTALIZE
    }
    public GameObject boulderDropPrefab;
    public GameObject boulderThrowPrefab;
    public GameObject boulderSpawner;
    public GameObject boulderThrowSpawner;
    public GameObject pebbleStormCardPrefab;
    public GameObject blockingCrystalPrefab;
    public AttackQueueManager attackQueueManager;

    public ThrowBoulderSkill rockThrow;
    public PebbleStormSkill pebbleStorm;
    public BoulderDropSkill boulderDrop;
    public CrystalizeSkill crystalize;
    public CrystalBlockSkill crystalBlock;

    Summoner summoner;
    GameManager gameManager;
    BoardManager boardManager;
    CardManager cardManager;
    BlockingCrystal blockingCrystal;
    bool attackRoutineRunning = false;
    GameObject blockingCrystalRunTimeReference;

    public override void Awake() {
        base.Awake();
        summoner = FindObjectOfType<Summoner>();
        gameManager = FindObjectOfType<GameManager>();
        cardManager = FindObjectOfType<CardManager>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    public override void Start() {
        base.Start();
    }

    public override IEnumerator RunTurnRoutine() {
        if (GetIsProtected()) {
            animator.SetBool("IsProtected", false);
        }

        if (FindObjectOfType<BlockingCrystal>()) {
            BlockingCrystal blockingCrystal = FindObjectOfType<BlockingCrystal>().GetComponent<BlockingCrystal>();
            yield return StartCoroutine(blockingCrystal.Break());
        }
        yield return StartCoroutine(Attack());
        yield return new WaitUntil(() => DoneActions());
        QueueAttack();
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

    protected override IEnumerator Attack() {
        yield return StartCoroutine(ExecuteNextCommand());
    }

    public override void Initialize() {
        QueueAttack();
        QueueAttack();
    }

    void QueueAttack() {
        Moves randomAttack = (Moves)Random.Range(0, 5);

        if (randomAttack == Moves.PEBBLESTORM) {
            pebbleStorm.QueueSkill();
        } else if (randomAttack == Moves.BOULDERDROP) {
            boulderDrop.QueueSkill();
        } else if (randomAttack == Moves.ROCKTHROW) {
            rockThrow.QueueSkill();
        } else if (randomAttack == Moves.CRYSTALBLOCK) {
            crystalBlock.QueueSkill();
        } else if (randomAttack == Moves.CRYSTALIZE) {
            crystalize.QueueSkill();
        }
    }

    IEnumerator ExecuteNextCommand() {
        yield return StartCoroutine(attackQueueManager.ProcessNextAttack());
    }

    bool DoneActions() {
        return attackRoutineRunning ? false : true;
    }

    bool GetIsProtected() {
        return animator.GetBool("IsProtected");
    }
}
