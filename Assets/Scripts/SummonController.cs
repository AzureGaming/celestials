using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class SummonController : MonoBehaviour {
    public bool attackRoutineRunning = false;
    public bool movementRoutineRunning = false;
    Animator animator;
    BoardManager boardManager;
    SpriteRenderer spriteRenderer;
    Color color;
    float movementSpeed = 2f;
    Entity entity;
    GameManager gameManager;
    Summon summon;

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boardManager = FindObjectOfType<BoardManager>();
        entity = GetComponent<Entity>();
        gameManager = FindObjectOfType<GameManager>();
        summon = GetComponent<Summon>();
    }

    private void Start() {
        color = spriteRenderer.color;
    }

    public void Walk() {
        StartCoroutine(WalkRoutine(entity.movementSpeed));
    }

    public void Attack() {
        StartCoroutine(AttackRoutine(entity.range));
    }

    public void Die() {
        StartCoroutine(DieRoutine());
    }

    public void ExecuteAction() {

    }

    public void OnAttackAnimationEnd() {
        attackRoutineRunning = false;
    }

    bool CheckWithinRange(int range) {
        if (boardManager.GetDestination(summon, range) == null) {
            return true;
        }
        return false;
    }

    void SetParent(Tile parent) {
        transform.SetParent(parent.transform);
    }

    IEnumerator WalkRoutine(int tiles) {
        Tile destination = boardManager.GetDestination(summon, tiles);
        if (destination == null) {
            yield break;
        }
        movementRoutineRunning = true;
        Vector3 currentPos = transform.position;
        Vector3 endPos = destination.transform.position;
        animator.SetBool("isWalking", true);
        yield return StartCoroutine(UpdatePositionRoutine(currentPos, endPos));
        animator.SetBool("isWalking", false);
        SetParent(destination);
        movementRoutineRunning = false;
        yield break;
    }

    IEnumerator AttackRoutine(int range) {
        if (CheckWithinRange(range)) {
            Debug.Log("within range: " + entity.name);
            attackRoutineRunning = true;
            animator.SetTrigger("isAttacking");
            yield return new WaitUntil(() => !attackRoutineRunning);
        } else {
            Debug.Log("Not in range: " + entity.name);
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
