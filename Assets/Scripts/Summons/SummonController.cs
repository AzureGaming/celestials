
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class SummonController : MonoBehaviour {
    bool attackRoutineRunning = false;
    bool movementRoutineRunning = false;
    Animator animator;
    BoardManager boardManager;
    SpriteRenderer spriteRenderer;
    Color color;
    float movementSpeed = 2f;
    GameManager gameManager;

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    public void ExecuteAction() {

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

    bool CheckWithinRange(int range, int id) {
        Tile tileToAttack = boardManager.GetDestination(id, range);
        if (tileToAttack?.type == TileType.Boss) {
            return true;
        }
        return false;
    }

    void SetParent(Tile parent) {
        transform.SetParent(parent.transform);
    }

    IEnumerator WalkRoutine(int tiles, int id) {
        Tile tileToMoveTo = boardManager.GetDestination(id, tiles);
        if (tileToMoveTo?.type == TileType.Boss) {
            yield return StartCoroutine(DieRoutine());
            yield break;
        }
        movementRoutineRunning = true;
        Vector3 currentPos = transform.position;
        Vector3 endPos = tileToMoveTo.transform.position;
        animator.SetBool("isWalking", true);
        yield return StartCoroutine(UpdatePositionRoutine(currentPos, endPos));
        animator.SetBool("isWalking", false);
        SetParent(tileToMoveTo);
        movementRoutineRunning = false;
        yield break;
    }

    IEnumerator AttackRoutine(int range, int id) {
        if (CheckWithinRange(range, id)) {
            attackRoutineRunning = true;
            animator.SetTrigger("isAttacking");
            yield return new WaitUntil(() => !attackRoutineRunning);
        }
        yield break;
    }

    IEnumerator DieRoutine() {
        yield return StartCoroutine(FlashRed());
        yield return StartCoroutine(FadeOut());
        Destroy(transform.gameObject);
    }

    IEnumerator UpdatePositionRoutine(Vector3 currentPos, Vector3 endPos) {
        for (float t = 0; t < movementSpeed; t += Time.deltaTime) {
            Vector3 lerpedPos = Vector3.Lerp(currentPos, endPos, Mathf.Min(1, t / movementSpeed));
            transform.position = lerpedPos;
            yield return null;
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
