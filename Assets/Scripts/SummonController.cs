using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class SummonController : MonoBehaviour {
    Animator animator;
    BoardManager boardManager;
    SpriteRenderer spriteRenderer;
    Color color;
    float movementSpeed = 2f;

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    private void Start() {
        color = spriteRenderer.color;
    }

    public void Walk(int tiles) {
        StartCoroutine(WalkRoutine(tiles));
    }

    public void Attack(int damage) {
        // check if in range
        //if (boardManager.CheckAttackRange(cardPrefab.range, GetComponentInParent<Tile>().column, GetComponentInParent<Tile>().row)) {
        //    if (boardManager.CheckAttackRange(0, GetComponentInParent<Tile>().column, GetComponentInParent<Tile>().row)) {
        //        boardManager.IncrementAttacksToWaitFor();
        //        Debug.Log("Attack boss");
        //        animator.SetTrigger("isAttacking");
        //    }
    }

    public void Die() {
        StartCoroutine(DieRoutine());
    }

    public void ExecuteAction() {

    }

    void OnAttackAnimationEnd() {
        //StartCoroutine(boss.TakeDamage(card.attack));
        //    StartCoroutine(boss.TakeDamage(0));
        //    boardManager.IncrementAttackCounter();
    }

    void SetParent(Tile parent) {
        transform.SetParent(parent.transform);
    }

    IEnumerator WalkRoutine(int tiles) {
        Tile destination = boardManager.GetDestination(GetComponent<Summon>(), tiles);
        Vector3 currentPos = transform.position;
        Vector3 endPos = destination.transform.position;
        animator.SetBool("isWalking", true);
        yield return StartCoroutine(UpdatePositionRoutine(currentPos, endPos));
        animator.SetBool("isWalking", false);
        SetParent(destination);
        yield break;
    }

    IEnumerator DieRoutine() {
        yield return StartCoroutine(FlashRed());
        yield return StartCoroutine(FadeOut());
        Destroy(transform.gameObject);
    }

    IEnumerator UpdatePositionRoutine(Vector3 currentPos, Vector3 endPos) {
        Debug.Log("start" + transform.position);

        for (float t = 0; t < movementSpeed; t += Time.deltaTime) {
            Vector3 lerpedPos = Vector3.Lerp(currentPos, endPos, Mathf.Min(1, t / movementSpeed));
            transform.position = lerpedPos;
            yield return null;
        }
        Debug.Log("end" + transform.position);

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
