using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Summon))]
public class SummonController : MonoBehaviour {
    public GameObject barrierPrefab;
    public GameObject marchPrefab;
    public GameObject resetPrefab;
    public Entity entity;
    public AudioSource walkAudio;
    public AudioSource deathAudio;
    public float deathAudioStart = 0;
    public float deathAudioEnd = 0;
    public AudioSource attackAudio;
    public AudioSource powerAudio;
    public AudioSource spawnAudio;
    protected bool attackRoutineRunning = false;
    protected bool movementRoutineRunning = false;
    protected bool howlRoutineRunning = false;
    protected bool powerRoutineRunning = false;
    protected bool hasBarrier = false;
    protected Animator animator;
    protected BoardManager boardManager;
    protected SpriteRenderer spriteRenderer;
    protected Color color;
    protected float movementSpeed = 2f;
    protected GameManager gameManager;
    protected Summon summon;
    protected Nullable<int> id;
    protected int order;
    protected Boss boss;
    protected bool doneCasting;
    public int attack;

    protected virtual void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        summon = GetComponent<Summon>();
        boardManager = FindObjectOfType<BoardManager>();
        gameManager = FindObjectOfType<GameManager>();
        boss = FindObjectOfType<Boss>();
        resetPrefab = GetComponentInChildren<ResetEffect>().gameObject;
        barrierPrefab = GetComponentInChildren<BarrierEffect>().gameObject;
        marchPrefab = GetComponentInChildren<MarchEffect>().gameObject;
    }

    private void Start() {
        color = spriteRenderer.color;

        resetPrefab.SetActive(false);
        barrierPrefab.SetActive(false);
        marchPrefab.SetActive(false);

        SetOrder(gameManager.GetNextCardOrder());

        spawnAudio.Play();
    }

    public IEnumerator Walk() {
        yield return StartCoroutine(WalkRoutine(entity.movementSpeed, GetId()));
    }

    public void Walk(Tile tile) {
        StartCoroutine(WalkRoutine(tile, GetId()));
    }

    public void Attack() {
        StartCoroutine(AttackRoutine(entity.range, GetId()));
    }

    public IEnumerator Die(bool dyingWish) {
        yield return StartCoroutine(DieRoutine(dyingWish));
    }

    public virtual void UseHowl() {
        howlRoutineRunning = true;
    }

    public virtual void UsePower() {
        //powerRoutineRunning = true;
    }

    void SetOrder(int value) {
        order = value;
    }

    public int GetOrder() {
        return order;
    }

    public int GetId() {
        if (id == null) {
            id = gameManager.GetNextEntityId();
        }
        return (int)id;
    }

    public int GetRange() {
        return entity.range;
    }

    public virtual void OnAttackAnimationEnd() {
        attackRoutineRunning = false;
    }

    public void OnCastAnimationEnd() {
        doneCasting = true;
    }

    public bool DoneMoving() {
        return !movementRoutineRunning ? true : false;
    }

    public bool DoneAttacking() {
        return !attackRoutineRunning ? true : false;
    }

    public bool DoneHowl() {
        return !howlRoutineRunning ? true : false;
    }

    public bool DonePower() {
        return !powerRoutineRunning ? true : false;
    }

    public virtual IEnumerator WalkRoutine(int tiles, int id) {
        Tile tileToMoveTo = boardManager.GetDestination(id, tiles);
        if (tileToMoveTo?.type == TileType.Boss) {
            yield return StartCoroutine(DieRoutine(false));
        } else if (tileToMoveTo.GetComponentInChildren<BlockingCrystal>()) {
            yield break;
        } else {
            walkAudio.loop = true;
            walkAudio.Play();
            yield return StartCoroutine(UpdatePositionRoutine(transform.position, tileToMoveTo));
            walkAudio.Stop();
        }
    }

    public virtual IEnumerator WalkRoutine(Tile tile, int id) {
        if (tile?.type == TileType.Boss) {
            yield return StartCoroutine(DieRoutine(false));
        } else {
            walkAudio.loop = true;
            walkAudio.Play();
            yield return StartCoroutine(UpdatePositionRoutine(transform.position, tile));
            walkAudio.Stop();
        }
    }

    public IEnumerator TakeDamage() {
        if (hasBarrier) {
            GetComponentInChildren<BarrierEffect>().Deactivate();
            hasBarrier = false;
            // timing?
        } else {
            yield return StartCoroutine(Die(true));
        }
    }

    public IEnumerator ActivateBarrier() {
        barrierPrefab.SetActive(true);
        yield return StartCoroutine(barrierPrefab.GetComponent<BarrierEffect>().Activate());
        hasBarrier = true;
    }

    public IEnumerator ActivateMarch() {
        marchPrefab.SetActive(true);
        yield return StartCoroutine(marchPrefab.GetComponent<MarchEffect>().Activate());
    }

    public IEnumerator ActivateReset() {
        resetPrefab.SetActive(true);
        yield return StartCoroutine(resetPrefab.GetComponent<ResetEffect>().Activate());
    }

    protected void SetParent(Tile parent) {
        transform.SetParent(parent.transform);
    }

    protected IEnumerator DieRoutine(bool dyingWish) {
        deathAudio.time = deathAudioStart;
        deathAudio.Play();
        deathAudio.SetScheduledEndTime(AudioSettings.dspTime + (deathAudioEnd - deathAudioStart));
        yield return StartCoroutine(FlashRed());
        yield return StartCoroutine(FadeOut());
        if (dyingWish) {
            yield return StartCoroutine(DyingWish());
        }
        Destroy(transform.gameObject);
    }

    protected virtual IEnumerator DyingWish() {
        yield break;
    }

    protected virtual IEnumerator UpdatePositionRoutine(Vector3 currentPos, Tile tileToMoveTo) {
        movementRoutineRunning = true;
        animator.SetBool("isWalking", true);
        for (float t = 0; t < movementSpeed; t += Time.deltaTime) {
            Vector3 lerpedPos = Vector3.Lerp(currentPos, tileToMoveTo.transform.position, Mathf.Min(1, t / movementSpeed));
            transform.position = lerpedPos;
            yield return null;
        }
        SetParent(tileToMoveTo);
        animator.SetBool("isWalking", false);
        movementRoutineRunning = false;
    }

    protected bool CheckWithinRange(int range, int id) {
        Tile tileToAttack = boardManager.GetDestination(id, range);
        if (tileToAttack?.type == TileType.Boss) {
            return true;
        }
        return false;
    }

    public virtual IEnumerator AttackRoutine(int range, int id) {
        if (CheckWithinRange(range, id)) {
            attackRoutineRunning = true;
            animator.SetTrigger("isAttacking");
            attackAudio.Play();
            yield return new WaitUntil(() => !attackRoutineRunning);
            yield return StartCoroutine(boss.TakeDamage(attack));
        }
    }

    IEnumerator FlashRed() {
        int duration = 2;
        for (int t = 0; t < duration; t++) {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.color = color;
    }

    IEnumerator FadeOut() {
        float duration = 1f;
        for (float t = 0; t < duration; t += Time.deltaTime) {
            float alpha = Mathf.Lerp(spriteRenderer.color.a, 0, Mathf.Min(1, t / duration));
            if (alpha < 0.1) {
                yield break;
            }
            Color newColor = spriteRenderer.color;
            newColor.a = alpha;
            spriteRenderer.color = newColor;
            yield return null;
        }
    }
}
