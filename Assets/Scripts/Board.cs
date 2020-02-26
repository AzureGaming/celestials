using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    Summon[] summons;
    GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    public Summon[] GetSummons() {
        return GetComponentsInChildren<Summon>();
    }

    private IEnumerator OnMouseUp() {
        Debug.Log("mouse up on board");
        if (gameManager.GetIsHoldingCard()) {
            yield return StartCoroutine(Summon(gameManager.GetHeldCardId()));
        }
    }

    IEnumerator Summon(int cardId) {
        Debug.Log("Implement Summon method" + cardId);
        yield break;
    }
}
