using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour {
    int order;
    Tile tile;
    float movementSpeed = 2f;
    Animator animator;
    // coordinates how to move?

    private void Awake() {
        tile = GetComponentInParent<Tile>();
        animator = GetComponent<Animator>();
    }

    public int getOrder() {
        return order;
    }

    public void setOrder(int value) {
        order = value;
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
            Debug.Log("fadsf" + lerpedPos);
            yield return null;
        }
        animator.SetBool("isWalking", false);
        SetParent(destination);

    }

    void SetParent(Tile parent) {
        transform.SetParent(parent.transform);
    }
}
