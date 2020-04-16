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

    // coordinates how to move?

    private void Awake() {
        tile = GetComponentInParent<Tile>();
        animator = GetComponent<Animator>();
        boardManager = FindObjectOfType<BoardManager>();
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
        Debug.Log("Implement summon action execution" + order);
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
        if (boardManager.CheckCanHitBoss(tile, this.card.range)) {
            Debug.Log("Attack boss");
            animator.SetTrigger("isAttacking");
        } else {
            Debug.Log("Out of range");
        }
        yield break;
    }

    public void HandleAttackAnimationEvent() {

    }

    void SetParent(Tile parent) {
        transform.SetParent(parent.transform);
    }
}
