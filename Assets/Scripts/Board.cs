using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    Summon[] summons;
    GameManager gameManager;
    BoxCollider2D collider2d;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        collider2d = GetComponent<BoxCollider2D>();
    }

    public Summon[] GetSummons() {
        return GetComponentsInChildren<Summon>();
    }

    public void EnablePlay() {
        SetColliderTrigger(true);
    }

    public void DisablePlay() {
        SetColliderTrigger(false);
    }

    private IEnumerator OnMouseUp() {
        Debug.Log("mouse up on board");
        if (gameManager.GetIsHoldingCard()) {
            yield return StartCoroutine(Summon(gameManager.GetHeldCardId()));
        }
    }

    void SetColliderTrigger(bool value) {
        collider2d.isTrigger = value;
    }

    IEnumerator Summon(int cardId) {
        Debug.Log("Implement Summon method" + cardId);
        yield break;
    }
}
