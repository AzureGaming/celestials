using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Summon))]
public class SummonController : MonoBehaviour {
    protected bool attackRoutineRunning = false;
    protected bool movementRoutineRunning = false;
    protected Animator animator;
    protected BoardManager boardManager;
    protected SpriteRenderer spriteRenderer;
    protected Color color;
    protected float movementSpeed = 2f;
    protected GameManager gameManager;
    protected Summon summon;

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        summon = GetComponent<Summon>();
        boardManager = FindObjectOfType<BoardManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start() {
        color = spriteRenderer.color;
    }

    public void Walk(int movementSpeed, int id) {
        StartCoroutine(WalkRoutine(movementSpeed, id));
    }

    public void Attack(int range, int id) {
        StartCoroutine(AttackRoutine(range, id));
    }

    public void Die() {
        StartCoroutine(DieRoutine());
    }

    public virtual void ExecuteAction() {

    }

    public void OnAttackAnimationEnd() {
        attackRoutineRunning = false;
    }

    public bool DoneMoving() {
        return !movementRoutineRunning ? true : false;
    }

    public bool DoneAttacking() {
        return !attackRoutineRunning ? true : false;
    }

    public void SetEffectPrefab(GameObject prefab) {
        foreach (Transform childTransform in transform.Find("Effect")) {
            Destroy(childTransform.gameObject);
        }
        Instantiate(prefab, transform.Find("Effect"));
    }

    public void RemoveEffectPrefab() {
        foreach (Transform childTransform in transform.Find("Effect")) {
            Destroy(childTransform.gameObject);
        }
    }

    public virtual IEnumerator WalkRoutine(int tiles, int id) {
        Tile tileToMoveTo = boardManager.GetDestination(id, tiles);
        if (tileToMoveTo?.type == TileType.Boss) {
            yield return StartCoroutine(DieRoutine());
        } else {
            yield return StartCoroutine(UpdatePositionRoutine(transform.position, tileToMoveTo));
        }
    }

    protected void SetParent(Tile parent) {
        transform.SetParent(parent.transform);
    }

    protected IEnumerator DieRoutine() {
        yield return StartCoroutine(FlashRed());
        yield return StartCoroutine(FadeOut());
        Destroy(transform.gameObject);
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
        yield break;
    }

    protected int GetSummonId() {
        return summon.GetId();
    }

    bool CheckWithinRange(int range, int id) {
        Tile tileToAttack = boardManager.GetDestination(id, range);
        if (tileToAttack?.type == TileType.Boss) {
            return true;
        }
        return false;
    }

    IEnumerator AttackRoutine(int range, int id) {
        if (CheckWithinRange(range, id)) {
            attackRoutineRunning = true;
            animator.SetTrigger("isAttacking");
            yield return new WaitUntil(() => !attackRoutineRunning);
        }
        yield break;
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
        yield break;
    }

    IEnumerator FadeOut() {
        float duration = 1f;
        for (float t = 0; t < duration; t += Time.deltaTime) {
            float alpha = Mathf.Lerp(spriteRenderer.color.a, 0, Mathf.Min(1, t / duration));
            Color newColor = spriteRenderer.color;
            newColor.a = alpha;
            spriteRenderer.color = newColor;
            yield return null;
        }
        yield break;
    }
}
