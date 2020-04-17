using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour {
    int order;
    Tile tile;
    BoardManager boardManager;
    float movementSpeed = 2f;
    Animator animator;
    Card card;
    Color color;
    SpriteRenderer spriteRenderer;
    Boss boss;

    // coordinates how to move?

    private void Awake() {
        tile = GetComponentInParent<Tile>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boardManager = FindObjectOfType<BoardManager>();
        boss = FindObjectOfType<Boss>();
    }

    private void Start() {
        color = spriteRenderer.color;
    }

    public int getOrder() {
        return order;
    }

    public void setOrder(int value) {
        order = value;
    }

    public void InitSummon(Card card, int order) {
        this.card = card;
        setOrder(order);
    }

    public IEnumerator ExecuteAction() {
        yield break;
    }

    public IEnumerator Move() {
        yield return StartCoroutine(tile.MoveSummon(this));
    }

    public IEnumerator WalkToTile(Tile destination) {
        Vector3 currentPos = transform.position;
        Vector3 endPos = currentPos;
        endPos.x += (float)CONSTANTS.summonSpacing;

        animator.SetBool("isWalking", true);
        for (float t = 0; t < movementSpeed; t += Time.deltaTime) {
            Vector3 lerpedPos = Vector3.Lerp(currentPos, endPos, Mathf.Min(1, t / movementSpeed));
            transform.position = lerpedPos;
            yield return null;
        }
        animator.SetBool("isWalking", false);
        SetParent(destination);
        yield break;
    }

    public IEnumerator Attack() {
        if (boardManager.CheckCanHitBoss(tile, card.range)) {
            boardManager.IncrementAttacksToWaitFor();
            Debug.Log("Attack boss");
            animator.SetTrigger("isAttacking");
        } else {
            Debug.Log("Out of range");
        }
        yield break;
    }

    public void HandleAttackAnimationEvent() {
        StartCoroutine(boss.TakeDamage(card.attack));
        boardManager.IncrementAttackCounter();
    }

    public IEnumerator Die() {
        yield return StartCoroutine(FadeOut());
        Destroy(transform.gameObject);
    }

    void SetParent(Tile parent) {
        transform.SetParent(parent.transform);
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
