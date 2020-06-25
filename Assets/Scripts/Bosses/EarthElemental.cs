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
    Summoner summoner;
    GameManager gameManager;
    BoardManager boardManager;
    CardManager cardManager;
    BlockingCrystal blockingCrystal;
    bool attackRoutineRunning = false;
    GameObject blockingCrystalRunTimeReference;
    bool doneSpawningBlockingCrystal = false;
    bool spawnedBlockingCrystal = false;
    List<Moves> attackQueue = new List<Moves>();

    public override void Awake() {
        base.Awake();
        summoner = FindObjectOfType<Summoner>();
        gameManager = FindObjectOfType<GameManager>();
        cardManager = FindObjectOfType<CardManager>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    public override void Start() {
        base.Start();
        QueueAttack();
        //QueueAttack();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            QueueAttack();
        }
    }

    public override IEnumerator RunTurnRoutine() {
        if (GetIsProtected()) {
            SetIsProtected(false);
        }

        if (spawnedBlockingCrystal) {
            BlockingCrystal blockingCrystal = FindObjectOfType<BlockingCrystal>().GetComponent<BlockingCrystal>();
            yield return StartCoroutine(blockingCrystal.Break());
            spawnedBlockingCrystal = false;
        }
        yield return StartCoroutine(Attack());
        yield return new WaitUntil(() => DoneActions());
        //QueueAttack();
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

    public void OnBlockingCrystalSpawnAnimationEnd() {
        doneSpawningBlockingCrystal = true;
    }

    protected override IEnumerator Attack() {
        int randomAttack = Random.Range(0, 5);
        if (attackQueue.Count > 0) {
            Moves attack = attackQueue.Last();
            yield return ExecuteAttack(attack);
            Debug.Log("Remove from queue");
            attackQueue.Remove(attack);
        } else {
            yield return ExecuteAttack((Moves)randomAttack);
        }
    }

    IEnumerator ExecuteAttack(Moves randomAttack) {
        attackRoutineRunning = true;
        if (randomAttack == Moves.PEBBLESTORM) {
            yield return StartCoroutine(PebbleStorm());
        } else if (randomAttack == Moves.BOULDERDROP) {
            yield return StartCoroutine(BoulderDrop());
        } else if (randomAttack == Moves.ROCKTHROW) {
            yield return StartCoroutine(rockThrow.CastSkill());
        } else if (randomAttack == Moves.CRYSTALBLOCK) {
            yield return StartCoroutine(CrystalBlock());
        } else if (randomAttack == Moves.CRYSTALIZE) {
            yield return StartCoroutine(Crystalize());
        }
    }

    void QueueAttack() {
        Debug.Log("Queue attack" + attackQueue.Count);
        if (attackQueue.Count >= 2) {
            Debug.LogWarning("Queue is full");
            return;
        }
        int randomAttack = Random.Range(0, 5);
        Moves nextAttack = /*(Moves)randomAttack*/ Moves.ROCKTHROW;
        attackQueue.Add(nextAttack);
        if (nextAttack == Moves.PEBBLESTORM) {
            //skillIndicators.SetPebbleStorm();
        } else if (nextAttack == Moves.BOULDERDROP) {
            //skillIndicators.SetBoulderDrop();
        } else if (nextAttack == Moves.ROCKTHROW) {
            rockThrow.QueueSkill();
            //QueueBoulderThrowTargets();
        } else if (nextAttack == Moves.CRYSTALBLOCK) {
            //skillIndicators.SetBlockingCrystal();
        } else if (nextAttack == Moves.CRYSTALIZE) {
            //skillIndicators.SetCrystalize();
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
        Instantiate(boulderDropPrefab, boulderSpawner.transform);
        yield return new WaitUntil(() => DoneBoulderDrop());
        yield return new WaitUntil(() => DoneActions());
        summoner.TakeDamage(3);
        yield break;
    }

    IEnumerator CrystalBlock() {
        animator.SetTrigger("Attack2");
        yield return new WaitUntil(() => DoneActions());
        List<Tile> validTiles = boardManager.GetSummonableTiles();
        int randomIndex = Random.Range(0, validTiles.Count);
        Tile tileToBlock = validTiles[randomIndex];
        spawnedBlockingCrystal = true;
        blockingCrystalRunTimeReference = Instantiate(blockingCrystalPrefab, tileToBlock.transform);
        BlockingCrystal blockingCrystal = blockingCrystalRunTimeReference.GetComponent<BlockingCrystal>();
        yield return new WaitUntil(() => !blockingCrystal.GetIsSpawning());
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

    bool DoneSpawningBlockingCrystal() {
        return doneSpawningBlockingCrystal;
    }

    bool GetIsProtected() {
        return animator.GetBool("IsProtected");
    }

    void SetIsProtected(bool value) {
        animator.SetBool("IsProtected", value);
    }
}
