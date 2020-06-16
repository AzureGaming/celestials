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

    void SetColliderTrigger(bool value) {
        collider2d.isTrigger = value;
    }

    public IEnumerator Summon(Card card) {
        Debug.Log("Implement Summon method" + card);
        yield break;
    }
}
